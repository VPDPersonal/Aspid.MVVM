using System;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.Types.Editors;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    // The three bulk mutations a group card offers — rewrite every entry to one picked type, clear them all to null,
    // and undo a rewrite from its receipt — plus the inline picker they hang off. Each one confirms with a preview
    // computed by the very scan the write applies, leaves a receipt in the summary stack, and hands the file work to
    // SerializeReferenceBatchEditor; only the confirmation copy and the re-render live here.
    internal sealed partial class SerializeReferenceProjectView
    {
        private const string PickerClass = RootClass + "__picker";
        private const string PickerAttachedClass = PickerClass + "--attached";
        private const string GroupPickingClass = GroupClass + "--picking";

        private static readonly AuditPickerHost.PickerClasses _pickerClassSet =
            new(PickerClass, PickerAttachedClass, GroupPickingClass);

        // The group's bulk picker, inline below the Fix all button, constrained to the group's intersected field type.
        private void ToggleGroupPicker(MissingReferenceGroup group, Type constraint, AspidGradientButton button)
        {
            if (_picker.ToggleClosed(button)) return;

            _picker.Open(button, new TypeSelectorView(
                filter: ManagedReferenceFilter.For(constraint),
                currentAqn: null, // the bulk group picker has no current value — nothing (not even <None>) wears the check
                onSelected: assemblyQualifiedName =>
                {
                    // <None> emits an empty name: clear the group to null instead of treating it as a no-op.
                    if (string.IsNullOrEmpty(assemblyQualifiedName))
                    {
                        ClearGroupToNull(group);
                        return;
                    }

                    var type = Type.GetType(assemblyQualifiedName, throwOnError: false);
                    if (type is not null) ApplyGroupFix(group, type);
                },
                onDismiss: _picker.Close));
        }

        // Rewrites every entry in the group to newType after a mandatory confirmation.
        private void ApplyGroupFix(MissingReferenceGroup group, Type newType)
        {
            if (newType is null) return;
            _picker.Close();

            var entries = SerializeReferenceBatchEditor.FilterWritable(group.Entries, out var skipped);

            if (entries.Count == 0)
            {
                EditorUtility.DisplayDialog(
                    "Repair Missing References",
                    "All references in this group live in open scene(s) or Prefab Mode. Close them and rescan, " +
                    "or repair the fields directly in the Inspector.",
                    "OK");
                return;
            }

            var files = SerializeReferenceBatchEditor.CountFiles(entries);
            var skippedNote = skipped > 0
                ? $"\n\n{skipped} reference(s) in open scene(s) or Prefab Mode will be skipped."
                : string.Empty;

            // When the group's picker fell back to an unconstrained list because its entries' declared field types
            // disagree, the single chosen type cannot fit every entry — warn that the mismatched ones null on reimport.
            group.ResolveConstraint(out var mixedFieldTypes);
            var mixedNote = mixedFieldTypes
                ? "\n\nField types in this group differ — the chosen type may not fit every entry; incompatible ones " +
                  "will become null on reimport."
                : string.Empty;

            var managedType = ManagedTypeName.FromType(newType);

            // The preview is computed by the same scan the rewrite applies, so it shows exactly what gets written.
            var diff = SerializeReferenceProjectSummary.BuildDiffPreview(entries, managedType);

            if (!EditorUtility.DisplayDialog(
                    "Repair Missing References",
                    $"Rewrite {entries.Count} reference(s) in {files} file(s) to '{newType.FullName}'?\n\n" +
                    diff +
                    "This edits the asset files directly; an Undo button on the summary can revert it." + skippedNote + mixedNote,
                    "Rewrite",
                    "Cancel"))
                return;

            var rewritten = SerializeReferenceBatchEditor.Rewrite(entries, managedType, "Repairing References");

            SerializeReferenceRepairSuggestions.ClearCache();

            var summaryTitle = rewritten == 1 ? "Rewrote 1 reference" : $"Rewrote {rewritten} references";
            var summaryBody = $"Replaced missing '{group.DisplayName}' with '{newType.FullName}'.";
            if (skipped > 0)
                summaryBody += $" Skipped {skipped} in open scene(s) or Prefab Mode.";

            // Undo re-points the entries back to the original (now-missing) stored type. Only the type line moved —
            // the data blocks were never touched on disk — so flipping it back is a faithful revert.
            var originalType = group.StoredType;
            var missingName = group.DisplayName;
            var appliedName = newType.FullName;
            void Undo(VisualElement receipt) => UndoGroupFix(entries, originalType, managedType, missingName, appliedName, receipt);

            RerenderAfterBulkEdit();
            ShowSummary(summaryTitle, summaryBody, Undo);
        }

        // Clears every entry in the group to null. Closed assets are nulled in the YAML directly; assets open in
        // Prefab Mode / a loaded scene cannot be rewritten on disk (the open copy would clobber it on save), so those
        // are nulled on the live object and stay in the audit until saved. NOT undoable: the broken payload is discarded.
        private void ClearGroupToNull(MissingReferenceGroup group)
        {
            _picker.Close();

            SerializeReferenceBatchEditor.SplitWritable(group.Entries, out var onDisk, out var inMemory);
            if (onDisk.Count == 0 && inMemory.Count == 0) return;

            var fileCount = SerializeReferenceBatchEditor.CountFiles(onDisk);
            var total = onDisk.Count + inMemory.Count;

            var openNote = inMemory.Count > 0
                ? $"\n\n{inMemory.Count} reference(s) are open in Prefab Mode or a scene — those are nulled on the live " +
                  "object and saved with the asset (the audit keeps listing them until you save)."
                : string.Empty;
            var diskNote = onDisk.Count > 0
                ? $" {onDisk.Count} on disk in {fileCount} file(s) are edited directly."
                : string.Empty;

            if (!EditorUtility.DisplayDialog(
                    "Clear Missing References",
                    $"Clear {total} reference(s) to null?\n\n" +
                    SerializeReferenceProjectSummary.BuildClearPreview(group.Entries) +
                    $"This nulls every field holding the broken '{group.DisplayName}' and discards its payload." +
                    diskNote + " It cannot be undone." + openNote,
                    "Clear",
                    "Cancel"))
                return;

            var clearedOnDisk = SerializeReferenceBatchEditor.Null(onDisk, "Clearing References");
            var clearedInMemory = SerializeReferenceBatchEditor.ClearOpenInMemory(inMemory, group.StoredType);
            var cleared = clearedOnDisk + clearedInMemory;

            SerializeReferenceRepairSuggestions.ClearCache();

            // Nothing actually changed (every edit failed) — skip the receipt rather than claim a cleared count of 0.
            if (cleared == 0)
            {
                if (_scanButton is not null) _scanButton.Text = RescanLabel;
                RenderGroups(MissingReferenceGroup.CollectFromIndex(), RequiredViolationsForRender);
                return;
            }

            var summaryTitle = cleared == 1 ? "Cleared 1 reference" : $"Cleared {cleared} references";
            var summaryBody = $"Set missing '{group.DisplayName}' to null.";
            if (clearedInMemory > 0)
            {
                summaryBody += clearedInMemory == 1
                    ? " 1 was nulled in memory — save the asset to persist it (still listed until saved)."
                    : $" {clearedInMemory} were nulled in memory — save the assets to persist them (still listed until saved).";
            }

            // Unlike Fix all (which only swaps a stored type name, never nulls anything), Clear to null CAN turn a
            // required field that held a broken-but-non-null reference into a genuine unset-required violation — drop
            // the stale cache so the Required violations card doesn't under-report until the user rescans.
            _requiredIsWarm = false;

            RerenderAfterBulkEdit();

            // No Undo: clearing discards the broken payload (see above). The receipt is a plain record.
            ShowSummary(summaryTitle, summaryBody, onUndo: null);
        }

        // Reverts one bulk fix by re-pointing its entries back to the original (now-missing) stored type. Only this
        // fix's own receipt is dropped — receipts for other still-applied fixes survive, unlike a full Rescan.
        private void UndoGroupFix(IReadOnlyList<MissingReferenceLocation> entries, ManagedTypeName originalType,
            ManagedTypeName appliedType, string missingName, string appliedName, VisualElement receipt)
        {
            // The asset may have opened in a scene / Prefab Mode since the fix; apply the same guard as the forward fix.
            var writable = SerializeReferenceBatchEditor.FilterWritable(entries, out var skipped);

            // Only entries that STILL hold the type this receipt applied may be re-pointed — the group can have been
            // re-broken and fixed to a DIFFERENT type since, and blindly rewriting would destroy that newer fix.
            var revertible = SerializeReferenceBatchEditor.FilterStillHolding(writable, appliedType, out var diverged);

            if (revertible.Count == 0)
            {
                EditorUtility.DisplayDialog(
                    "Undo Repair",
                    diverged > 0
                        ? "These references no longer hold the type this fix applied (they were re-pointed or removed " +
                          "since), so there is nothing this undo can safely revert."
                        : "These references now live in open scene(s) or Prefab Mode. Close them and try the undo again.",
                    "OK");
                return;
            }

            var files = SerializeReferenceBatchEditor.CountFiles(revertible);
            var skippedNote = skipped > 0
                ? $"\n\n{skipped} reference(s) in open scene(s) or Prefab Mode will be skipped."
                : string.Empty;
            var divergedNote = diverged > 0
                ? $"\n\n{diverged} reference(s) no longer hold '{appliedName}' (changed since this fix) and will be left alone."
                : string.Empty;

            if (!EditorUtility.DisplayDialog(
                    "Undo Repair",
                    $"Re-point {revertible.Count} reference(s) in {files} file(s) back to the missing '{missingName}'?\n\n" +
                    $"This restores the broken state you had before replacing it with '{appliedName}', and edits the " +
                    "asset files directly." + skippedNote + divergedNote,
                    "Undo",
                    "Cancel"))
                return;

            var reverted = SerializeReferenceBatchEditor.Rewrite(revertible, originalType, "Undoing Repair");

            SerializeReferenceRepairSuggestions.ClearCache();

            // Drop only this receipt — the others describe fixes still applied. RenderGroups rebuilds only _list,
            // never _summaries, so the surviving receipts stay put.
            receipt?.RemoveFromHierarchy();
            RenderGroups(MissingReferenceGroup.CollectFromIndex(), RequiredViolationsForRender);

            // The rewrite can come up short if a file changed between the check and the write — report the real count.
            var undoTitle = reverted == 1 ? "Reverted 1 reference" : $"Reverted {reverted} references";
            var undoBody = $"Re-pointed back to the missing '{missingName}'.";
            if (diverged > 0) undoBody += $" Left {diverged} alone (no longer '{appliedName}').";
            if (reverted < revertible.Count) undoBody += $" {revertible.Count - reverted} could not be rewritten.";
            ShowSummary(undoTitle, undoBody, null);
        }
    }
}
