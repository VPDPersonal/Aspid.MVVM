using System;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using Aspid.FastTools.UIElements.Editors.Internal;
using static Aspid.FastTools.SerializeReferences.Editors.SerializeReferenceAuditUI;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    // Card building: one card per broken stored type, whose whole header is the bulk picker, plus the read-only
    // Required violations card. Entry rows are deliberately not individually fixable here — the per-row Fix affordance
    // is reserved for the Asset References graph, and a row's only affordance is jumping to its asset.
    internal sealed partial class SerializeReferenceProjectView
    {
        private const string GroupClass = RootClass + "__group";
        private const string GroupMigrateClass = GroupClass + "--migrate";
        private const string GroupHeaderHoverClass = GroupClass + "--header-hover";
        private const string GroupDividerClass = RootClass + "__group-divider";
        private const string GroupSweepClass = RootClass + "__group-sweep";
        private const string GroupSweepMigrateClass = GroupSweepClass + "--migrate";
        private const string GroupHeaderRowClass = RootClass + "__group-header-row";
        private const string GroupHeaderRowStaticClass = GroupHeaderRowClass + "--static";
        private const string GroupHeaderClass = RootClass + "__group-header";
        private const string GroupCountClass = RootClass + "__group-count";
        private const string GroupFixAllClass = RootClass + "__group-fix-all";
        private const string GroupFixAllMigrateClass = GroupFixAllClass + "--migrate";
        private const string GroupActionClass = RootClass + "__group-action";
        private const string GroupActionInfoClass = GroupActionClass + "--info";
        private const string GroupEntryClass = RootClass + "__group-entry";
        private const string GroupEntryPathClass = RootClass + "__group-entry-path";
        private const string GroupEntryRidClass = RootClass + "__group-entry-rid";
        private const string GroupEntryFieldClass = RootClass + "__group-entry-field";

        // A broken-type group card: the whole header is one clickable row that toggles the type picker, with the bulk
        // "Fix all (N) ▼" action on the right.
        private VisualElement BuildGroupCard(MissingReferenceGroup group, MissingReferenceMigration migration)
        {
            var card = new AspidBox(AspidBoxPreset.Default.SetTheme(ThemeStyle.Type.Darkness))
                .AddClass(GroupClass);

            // Constraint + migration were resolved once in RenderGroups (see MissingReferenceMigration) and are reused
            // here so the card can never disagree with the partition. A migration is an authoritative [MovedFrom]
            // rename: Unity already migrates these in memory at load — only the files still store the old name.
            var constraint = migration.Constraint;
            var isMigration = migration.IsMigration;

            // Card-level modifier so card-wide states (the --picking accent frame) can follow the card's own
            // accent — a migration card is info-toned end to end, never the broken-card amber.
            if (isMigration) card.AddClass(GroupMigrateClass);

            // Built first so the type name + count can be docked into its body; the captured local is assigned before use.
            AspidGradientButton fixAll = null;
            fixAll = new AspidGradientButton(SerializeReferenceProjectSummary.BuildFixAllLabel(group, isMigration),
                    _ => ToggleGroupPicker(group, constraint, fixAll))
                .AddClass(GroupFixAllClass);
            // A migration card keeps its calm info tone end to end — the amber Fix all accent is the "broken" alarm.
            if (isMigration) fixAll.AddClass(GroupFixAllMigrateClass);
            // Registered as the card's header, so the ring drives the underline sweep added below from both the
            // keyboard highlight and the mouse hover.
            _ring.RegisterHeader(fixAll, card, GroupHeaderHoverClass, () => ToggleGroupPicker(group, constraint, fixAll));
            fixAll.tooltip = constraint == typeof(object)
                ? $"{group.DisplayName}\nMixed or unresolvable field types — the picker is unconstrained (any managed-reference type)."
                : $"{group.DisplayName}\nConstrained to {constraint.FullName}.";

            fixAll.AddLeadingContent(BuildGroupHeaderRow(
                group.StoredType.Class,
                SerializeReferenceProjectSummary.BuildGroupCountText(group),
                isMigration ? StatusStyle.Type.Info : StatusStyle.Type.Warning,
                isStatic: false));
            card.AddChild(fixAll);

            AddGroupDivider(card, withSweep: true, isMigration ? GroupSweepMigrateClass : null);

            var action = BuildBulkActionRow(group, migration);
            if (action is not null) card.AddChild(action);

            foreach (var entry in group.Entries)
                card.AddChild(BuildGroupEntryRow(entry));

            return card;
        }

        // The one-click row under a card's header: a pending [MovedFrom] migration if the group is one, otherwise the
        // ranked Smart Fix guess — or nothing when neither applies.
        private VisualElement BuildBulkActionRow(MissingReferenceGroup group, MissingReferenceMigration migration)
        {
            if (migration.IsMigration)
            {
                var target = migration.Target;

                // Not a guess, so it replaces the Smart Fix row: same confirm + diff preview + Undo flow as a picked fix.
                return BuildGroupActionRow(
                    $"Migrate all ({group.Entries.Count}) → {target.Name}",
                    $"Every entry resolves to {target.FullName} via its declared [MovedFrom] — Unity already " +
                    "migrates them in memory when the asset loads. Migrating rewrites the stored type name in the " +
                    "files so they match the code; the attribute can be removed once no file stores the old name.",
                    info: true,
                    () => ApplyGroupFix(group, target));
            }

            if (!group.TryGetSuggestion(migration.Constraint, out var suggestion)) return null;

            // Reuse the shared label/detail builders so the Smart Fix copy never drifts from the inspector notice.
            return BuildGroupActionRow(
                $"Smart Fix {SerializeReferenceHelpers.GetSuggestionLabel(suggestion)}",
                SerializeReferenceHelpers.GetSuggestionDetail(suggestion),
                info: false,
                () => ApplyGroupFix(group, suggestion.Type));
        }

        // A one-click bulk action (Smart Fix / Migrate all) as a member of the entry-row family: a left-aligned
        // accent verb over the same flat hover fill as the ping rows below it, instead of a filled gradient pill
        // floating over the glass card. Each card keeps one accent: warning amber for a Smart Fix guess on a
        // broken card, info for a pending migration.
        private VisualElement BuildGroupActionRow(string text, string tooltipText, bool info, Action onClick)
        {
            var row = new Label(text).AddClass(GroupActionClass);
            if (info) row.AddClass(GroupActionInfoClass);
            row.tooltip = tooltipText;
            row.RegisterCallback<ClickEvent>(_ => onClick());
            RegisterNavTarget(row, onClick);
            return row;
        }

        // Flat read-only list of every unset [TypeSelector(Required = true)] field, fed by the same headless scanner
        // as the build/CI gate. No bulk fix here — unlike a broken type, an empty required field has nothing sensible
        // to auto-assign, so the row's only affordance is jumping to the offending asset (where the graph's inline
        // Assign Required picker lives).
        private VisualElement BuildRequiredGroupCard(IReadOnlyList<GateViolation> violations)
        {
            var card = new AspidBox(AspidBoxPreset.Default.SetTheme(ThemeStyle.Type.Darkness))
                .AddClass(GroupClass);

            var files = violations.Select(violation => violation.AssetPath).Distinct(StringComparer.Ordinal).Count();

            card.AddChild(BuildGroupHeaderRow(
                "Required violations",
                $"{BuildCountText(violations.Count, "entry")} · {(files == 1 ? "1 file" : $"{files} files")}",
                StatusStyle.Type.Warning,
                isStatic: true));

            // Same header divider as the Fix-all cards, keeping every card's header/body split on one line — but no
            // sweep: this header row is static, there is nothing to hover.
            AddGroupDivider(card, withSweep: false);

            // One resolver for the whole card: several violations commonly share one asset (e.g. a prefab with
            // multiple unset required fields), and it memoises the object loads per asset path.
            var labels = new ViolationFieldLabels();
            foreach (var violation in violations)
                card.AddChild(BuildRequiredViolationRow(violation, labels));

            return card;
        }

        // Every card's header body: the title on the left, the count line on the right, ignored for picking so clicks
        // fall through to the hosting button's own handler (a static row has none).
        private static VisualElement BuildGroupHeaderRow(string title, string countText, StatusStyle.Type status, bool isStatic)
        {
            var header = new AspidLabel(title, AspidLabelPreset.Default
                    .SetLabelStatus(status)
                    .SetLabelSize(AspidLabelSizeStyle.Type.H5)
                    .SetLineSize(AspidDividingLineSizeStyle.Type.None))
                .AddClass(GroupHeaderClass)
                .SetPickingMode(PickingMode.Ignore);

            var count = new Label(countText)
                .AddClass(GroupCountClass)
                .SetPickingMode(PickingMode.Ignore);

            var row = new VisualElement()
                .AddClass(GroupHeaderRowClass)
                .AddChild(header)
                .AddChild(count);
            if (isStatic) row.AddClass(GroupHeaderRowStaticClass);
            row.pickingMode = PickingMode.Ignore;

            return row;
        }

        // Header divider plus, for an interactive header, its underline sweep (the Welcome cards' idiom). The sweep is
        // the header's sibling, so USS :hover can't reach it; it rides the card's --header-hover modifier instead,
        // which the ring lights. Both hide while the picker is docked — the dropdown is inserted right after the
        // header, and they would land under it.
        private static void AddGroupDivider(VisualElement card, bool withSweep, string sweepModifier = null)
        {
            card.AddChild(new AspidDividingLine(AspidDividingLinePreset.Default
                    .SetTheme(ThemeStyle.Type.Light)
                    .SetSize(AspidDividingLineSizeStyle.Type.Thin))
                .AddClass(GroupDividerClass));

            if (!withSweep) return;

            var sweep = new VisualElement()
                .AddClass(GroupSweepClass)
                .SetPickingMode(PickingMode.Ignore);
            if (sweepModifier is not null) sweep.AddClass(sweepModifier);
            card.AddChild(sweep);
        }

        // Read-only entry row: clicking jumps to the asset — the bulk Fix above is the only mutation in project mode.
        private VisualElement BuildGroupEntryRow(MissingReferenceLocation entry)
        {
            var path = MakeSelectable(new Label(entry.AssetPath).AddClass(GroupEntryPathClass));
            path.tooltip = entry.AssetPath;

            var rid = MakeSelectable(new Label($"rid {entry.Entry.Rid}").AddClass(GroupEntryRidClass));

            return BuildEntryRow(entry.AssetPath, path, rid);
        }

        // Read-only entry row: asset path on the left, "Component.field" on the right; the whole row jumps to the
        // asset — same cross-link as a broken-reference row.
        private VisualElement BuildRequiredViolationRow(GateViolation violation, ViolationFieldLabels labels)
        {
            var path = MakeSelectable(new Label(violation.AssetPath).AddClass(GroupEntryPathClass));
            path.tooltip = violation.AssetPath;

            var field = MakeSelectable(new Label(labels.Describe(violation)).AddClass(GroupEntryFieldClass));

            return BuildEntryRow(violation.AssetPath, path, field);
        }

        // The shared row shape behind both audit lists: two selectable columns over one click target.
        private VisualElement BuildEntryRow(string assetPath, Label left, Label right)
        {
            var row = new VisualElement().AddClass(GroupEntryClass);
            row.AddChild(left).AddChild(right);

            row.RegisterCallback<ClickEvent>(evt =>
            {
                // A drag-select ends in a click too — don't navigate away from text the user is copying.
                if (evt.target is TextElement text && text.selection.HasSelection()) return;
                JumpToAsset(assetPath);
            });

            RegisterNavTarget(row, () => JumpToAsset(assetPath));
            row.AddManipulator(new ContextualMenuManipulator(evt => PopulateEntryContextMenu(evt, assetPath)));

            return row;
        }

        // Right-click alternatives to the row's default left-click jump. Runs after the selectable labels populate
        // their own items (bubble-up), so the menu is wiped first to drop their Copy entry — Cmd+C on a selection
        // still copies, and the menu stays the same three items wherever the click lands.
        private void PopulateEntryContextMenu(ContextualMenuPopulateEvent evt, string assetPath)
        {
            for (var i = evt.menu.MenuItems().Count - 1; i >= 0; i--)
                evt.menu.RemoveItemAt(i);

            evt.menu.AppendAction("Open in Asset References", _ => JumpToAsset(assetPath));

            evt.menu.AppendAction(
                "Open in Prefab Mode",
                _ => PrefabStageUtility.OpenPrefab(assetPath),
                assetPath.EndsWith(".prefab", StringComparison.OrdinalIgnoreCase)
                    ? DropdownMenuAction.Status.Normal
                    : DropdownMenuAction.Status.Disabled);

            evt.menu.AppendAction("Select in Project", _ =>
            {
                var asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
                if (asset is null) return;

                Selection.activeObject = asset;
                EditorGUIUtility.PingObject(asset);
            });
        }

        // Cross-link shared by every read-only audit row: jump to the asset's full Inspect graph; ping as a
        // fallback when hosted standalone.
        private void JumpToAsset(string assetPath)
        {
            var asset = AssetDatabase.LoadMainAssetAtPath(assetPath);
            if (asset is null) return;

            if (OnInspectAsset is not null) OnInspectAsset(asset);
            else EditorGUIUtility.PingObject(asset);
        }
    }
}
