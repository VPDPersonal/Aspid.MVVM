using System;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.Types.Editors;
using Aspid.FastTools.UIElements.Editors.Internal;
using static Aspid.FastTools.SerializeReferences.Editors.SerializeReferenceAuditUI;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    // The individual cards a document's body is made of, and the parts they share. Four shapes, one look: a resolved
    // or missing reference, an unassigned slot, a required field the graph has no node for, and a back-edge leaf that
    // terminates a cycle. Which band a card gets — a YAML dropdown, a live-property dropdown, or a static line — is
    // decided here; what the band's pick does belongs to the .Picker partial.
    internal sealed partial class SerializeReferenceGraphView
    {
        private const string NodeClass = RootClass + "__node";
        private const string NodeBackEdgeClass = NodeClass + "--back-edge";
        private const string NodeEmptyClass = NodeClass + "--empty";
        private const string NodeMigrateCardClass = NodeClass + "--migrate";
        private const string NodeHeaderHoverClass = NodeClass + "--header-hover";
        private const string NodeBandClass = RootClass + "__node-band";
        private const string NodeBandMissingClass = NodeBandClass + "--missing";
        private const string NodeBandMigrateClass = NodeBandClass + "--migrate";
        private const string NodeBandRowClass = RootClass + "__node-band-row";
        private const string NodeDividerClass = RootClass + "__node-divider";
        private const string NodeSweepClass = RootClass + "__node-sweep";
        private const string NodeSweepMissingClass = NodeSweepClass + "--missing";
        private const string NodeSweepMigrateClass = NodeSweepClass + "--migrate";
        private const string NodeActionClass = RootClass + "__node-action";
        private const string NodeActionInfoClass = NodeActionClass + "--info";
        private const string NodeHeaderClass = RootClass + "__node-header";
        private const string NodeFooterClass = RootClass + "__node-footer";
        private const string NodeRootLabelClass = RootClass + "__node-root-label";
        private const string NodeTypeClass = RootClass + "__node-type";
        private const string NodeRidClass = RootClass + "__node-rid";
        private const string NodeBadgesClass = RootClass + "__node-badges";

        private const string BadgeClass = RootClass + "__badge";
        private const string BadgeSharedClass = BadgeClass + "--shared";

        private const string ChipClass = RootClass + "__chip";
        private const string ClearOrphanClass = RootClass + "__clear-orphan";

        // Band verb + collapse chevron; the picker host swaps the chevron glyph alone, never the label.
        private const string FixCollapsedText = "Fix Missing  ▼";
        private const string ChangeCollapsedText = "Change  ▼";
        private const string AssignCollapsedText = "Assign  ▼";

        // A required slot's band verb names what the amber is about: the field must be assigned, not merely can be.
        private const string AssignRequiredCollapsedText = "Assign Required  ▼";

        // A pending-migration card is not missing (Unity migrates it in memory; only the file is stale), so no "Missing".
        private const string MigrateFixCollapsedText = "Fix  ▼";

        // Single-sourced from the picker's "<None>" option so an empty slot reads like a cleared field in the Inspector.
        private const string EmptySlotText = TypeSelectorHelpers.NoneOption;

        // A node card whose band is an inline dropdown: a missing card edits through the YAML, a healthy one through
        // the live serialization API, an orphan keeps a static band plus a footer Clear. Cards are not indented —
        // the field path alone carries the nesting.
        private VisualElement BuildNodeCard(string assetPath, ReferenceGraphDocument document, ReferenceGraphNode? node, long rid, string pathLabel, bool isOrphan)
        {
            var missing = node is { Resolves: false, StoredType: { IsEmpty: false } };

            // An authoritative [MovedFrom] rename is a pending migration, not a breakage: Unity loads the reference
            // fine — only this file still stores the old name. Never for an orphan — nothing loads an orphan, so the
            // in-memory migration argument does not hold.
            Type migrationTarget = null;
            var isMigration = missing && !isOrphan &&
                SerializeReferenceGraphAnalysis.IsPendingMigration(assetPath, document.FileId, rid,
                    node.Value.StoredType, _constraints, out migrationTarget);

            var card = new AspidBox(AspidBoxPreset.Default.SetTheme(ThemeStyle.Type.Darkness))
                .AddClass(NodeClass);
            // Card-level modifier so card-wide states (the --picking accent frame, the picker's accent-follow rules)
            // read the calm info tone on a migration card instead of the broken-card amber.
            if (isMigration) card.AddClass(NodeMigrateCardClass);

            var typePreset = AspidLabelPreset.Default
                .SetLabelSize(AspidLabelSizeStyle.Type.H5)
                .SetLineSize(AspidDividingLineSizeStyle.Type.None);
            typePreset = isMigration
                ? typePreset.SetLabelStatus(StatusStyle.Type.Info)
                : missing || isOrphan
                    ? typePreset.SetLabelStatus(StatusStyle.Type.Warning)
                    : typePreset.SetLabelTheme(ThemeStyle.Type.Lightness);

            var typeLabel = new AspidLabel(node?.ShortName ?? $"rid {rid}", typePreset)
                .AddClass(NodeTypeClass)
                .SetPickingMode(PickingMode.Ignore);
            if (node is not null && !node.Value.StoredType.IsEmpty)
                typeLabel.tooltip = node.Value.FullName;

            var bandRow = BuildBandRow(typeLabel, BuildBadges(document, rid));

            // The captured file id targets every edit at exactly this document's rid (rids collide across documents).
            var fileId = document.FileId;

            if (missing)
            {
                // A missing reference cannot be reassigned through the serialization API, so its edit goes through the
                // YAML (keeping the orphaned payload).
                AspidGradientButton band = null;
                band = new AspidGradientButton(isMigration ? MigrateFixCollapsedText : FixCollapsedText,
                        _ => OpenMissingPicker(assetPath, fileId, rid, band))
                    .AddClass(NodeBandClass)
                    .AddClass(isMigration ? NodeBandMigrateClass : NodeBandMissingClass);
                band.AddLeadingContent(bandRow);
                card.AddChild(band);
                RegisterNavBand(band, card, () => OpenMissingPicker(assetPath, fileId, rid, band));
                AddBandDivider(card, band, isMigration ? NodeSweepMigrateClass : NodeSweepMissingClass);

                var action = BuildQuickFixRow(assetPath, fileId, rid, node.Value.StoredType, isMigration, migrationTarget);
                if (action is not null) card.AddChild(action);
            }
            else if (!isOrphan)
            {
                // A healthy reference edits through the live serialization API (keyed by the field path), so Unity
                // rewrites — or, on <None>, removes — the RefIds entry exactly as the Inspector would.
                var graphPath = pathLabel;
                AspidGradientButton band = null;
                band = new AspidGradientButton(ChangeCollapsedText, _ => OpenLivePicker(assetPath, fileId, graphPath, band))
                    .AddClass(NodeBandClass);
                band.AddLeadingContent(bandRow);
                card.AddChild(band);
                RegisterNavBand(band, card, () => OpenLivePicker(assetPath, fileId, graphPath, band));
                AddBandDivider(card, band, sweepModifier: null);
            }
            else
            {
                // An orphan has no field pointing at it, so there is no live property to edit — its band stays static
                // and the footer Clear (below) drops the dangling entry. The divider still splits band from footer,
                // but with no hover source there is no sweep.
                card.AddChild(bandRow);
                AddBandDivider(card, band: null, sweepModifier: null);
            }

            // Healthy and empty slots are cleared through their band's picker (<None>), so no separate button here.
            var meta = BuildFooter(pathLabel, $"rid {rid}");

            if (isOrphan)
            {
                // Drop a dangling RefIds entry no field points at. File edit, so it is confirmed and not undoable.
                var clear = new AspidGradientButton("Clear", _ => ClearOrphan(assetPath, fileId, rid))
                    .AddClass(ClearOrphanClass);
                RegisterNavTarget(clear, () => ClearOrphan(assetPath, fileId, rid));
                meta.AddChild(clear);
            }

            card.AddChild(meta);

            return card;
        }

        // An unassigned [SerializeReference] slot — a field whose pointer is the null sentinel (rid -2). Its band is
        // still a dropdown assigning a type through the live serialization API; a slot whose field path could not be
        // recovered stays static (nothing to target). A required slot wears the missing card's clothes — amber
        // "<None>" header and amber band accent, no badge — so every "fix this" card in the graph reads the same.
        private VisualElement BuildEmptySlotCard(string assetPath, long fileId, string pathLabel)
        {
            var isRequired = IsFieldRequiredUnset(fileId, pathLabel);

            var card = new AspidBox(AspidBoxPreset.Default.SetTheme(ThemeStyle.Type.Darkness))
                .AddClass(NodeClass);
            if (!isRequired) card.AddClass(NodeEmptyClass);

            // A plain Label on an ordinary empty slot so the --empty USS rule tints it; a required slot paints its
            // own amber status via AspidLabel, exactly like a missing card's type header.
            var typeLabel = isRequired
                ? (VisualElement)BuildRequiredNoneLabel("Required reference is not set")
                : new Label(EmptySlotText).AddClass(NodeTypeClass).SetPickingMode(PickingMode.Ignore);

            var bandRow = BuildBandRow(typeLabel, badges: null);

            if (string.IsNullOrEmpty(pathLabel))
            {
                // No recoverable field path to target — leave the slot a static "<None>" leaf.
                card.AddChild(bandRow);
                AddBandDivider(card, band: null, sweepModifier: null);
            }
            else
            {
                // <None> is a no-op here — the slot is already unset.
                var graphPath = pathLabel;
                AspidGradientButton band = null;
                band = new AspidGradientButton(isRequired ? AssignRequiredCollapsedText : AssignCollapsedText,
                        _ => OpenLivePicker(assetPath, fileId, graphPath, band))
                    .AddClass(NodeBandClass);
                if (isRequired) band.AddClass(NodeBandMissingClass);
                band.AddLeadingContent(bandRow);
                card.AddChild(band);
                RegisterNavBand(band, card, () => OpenLivePicker(assetPath, fileId, graphPath, band));
                AddBandDivider(card, band, isRequired ? NodeSweepMissingClass : null);
            }

            card.AddChild(BuildFooter(pathLabel, "unassigned"));

            return card;
        }

        // Trailing cards for required violations the graph has no node for — a string / SerializableType required
        // field is never threaded into RefIds, so SerializeReferenceGraphScanner never emits a document for a
        // component whose only serialized-reference-worthy fields are these.
        // Mirrors a required BuildEmptySlotCard line for line — amber "<None>" header, amber "Assign ▼" band,
        // "Component.field:" + "unassigned" on the footer — so a required string / SerializableType field reads
        // exactly like a required managed-reference slot (and both echo the missing card's clothes). The pick writes
        // the type's assembly-qualified name into the backing string; a scene asset cannot be object-loaded (see
        // SerializeReferenceGraphEditor.TryResolveRequiredStringProperty), so its band stays a static line edited
        // through the normal Inspector.
        private VisualElement BuildRequiredOnlyCard(GateViolation violation, ViolationFieldLabels labels)
        {
            var card = new AspidBox(AspidBoxPreset.Default.SetTheme(ThemeStyle.Type.Darkness))
                .AddClass(NodeClass);

            var bandRow = BuildBandRow(BuildRequiredNoneLabel("Required type is not set"), badges: null);

            if (SerializeReferenceHelpers.IsScene(violation.AssetPath))
            {
                // Not reachable through the live serialization API — leave the band a static "<None>" line.
                card.AddChild(bandRow);
                AddBandDivider(card, band: null, sweepModifier: null);
            }
            else
            {
                AspidGradientButton band = null;
                band = new AspidGradientButton(AssignRequiredCollapsedText, _ => OpenRequiredStringPicker(violation, band))
                    .AddClass(NodeBandClass)
                    .AddClass(NodeBandMissingClass);
                band.AddLeadingContent(bandRow);
                card.AddChild(band);
                RegisterNavBand(band, card, () => OpenRequiredStringPicker(violation, band));
                AddBandDivider(card, band, NodeSweepMissingClass);
            }

            card.AddChild(BuildFooter(labels.Describe(violation), "unassigned"));

            return card;
        }

        // A back-edge to a rid already on the current render path — a single dim, italic line (no footer) so cycles
        // terminate visibly.
        private static VisualElement BuildBackEdgeCard(long rid)
        {
            var card = new AspidBox(AspidBoxPreset.Default.SetTheme(ThemeStyle.Type.Darkness))
                .AddClass(NodeClass)
                .AddClass(NodeBackEdgeClass);

            card.AddChild(new VisualElement()
                .AddClass(NodeHeaderClass)
                .AddChild(new Label($"↩ rid {rid}")
                    .AddClass(NodeTypeClass)
                    .SetPickingMode(PickingMode.Ignore)));

            return card;
        }

        // ---------------------------------------------------------------------------------------------------------
        // Shared card parts
        // ---------------------------------------------------------------------------------------------------------

        // The band's content, docked into the band button (or standing alone on a static card). Ignored for picking
        // so clicks fall through to the band's own handler.
        private static VisualElement BuildBandRow(VisualElement typeLabel, VisualElement badges)
        {
            var row = new VisualElement()
                .AddClass(NodeBandRowClass)
                .AddChild(typeLabel);
            if (badges is not null) row.AddChild(badges);
            row.pickingMode = PickingMode.Ignore;
            return row;
        }

        // The amber "<None>" header a required card wears — the same clothes as a missing card's type header.
        private static AspidLabel BuildRequiredNoneLabel(string tooltip)
        {
            var label = new AspidLabel(EmptySlotText, AspidLabelPreset.Default
                    .SetLabelStatus(StatusStyle.Type.Warning)
                    .SetLabelSize(AspidLabelSizeStyle.Type.H5)
                    .SetLineSize(AspidDividingLineSizeStyle.Type.None))
                .AddClass(NodeTypeClass)
                .SetPickingMode(PickingMode.Ignore);
            label.tooltip = tooltip;
            return label;
        }

        // No MISSING badge — the band action and amber type pill already carry it; only SHARED remains, dotted with
        // the rid's own colour so an aliased pair is recognisable across cards.
        private static VisualElement BuildBadges(ReferenceGraphDocument document, long rid)
        {
            var badges = new VisualElement()
                .AddClass(NodeBadgesClass)
                .SetPickingMode(PickingMode.Ignore);

            if (!document.Shared.Contains(rid)) return badges;

            var shared = new Label("SHARED").AddClass(BadgeClass).AddClass(BadgeSharedClass);
            var chip = new VisualElement().AddClass(ChipClass);
            chip.style.backgroundColor = SerializeReferenceRidColor.ForRid(rid);
            shared.AddChild(chip);

            return badges.AddChild(shared);
        }

        // The one-click row under a broken card's band: a pending [MovedFrom] migration if the stored type resolves to
        // one, otherwise the ranked Smart Fix guess — or nothing when neither applies.
        private VisualElement BuildQuickFixRow(string assetPath, long fileId, long rid, ManagedTypeName storedType,
            bool isMigration, Type migrationTarget)
        {
            if (isMigration)
            {
                // The same YAML rewrite a picker pick performs — no confirm, matching the picker's own apply.
                return BuildNodeActionRow(
                    $"Migrate → {migrationTarget.Name}",
                    $"This entry resolves to {migrationTarget.FullName} via its declared [MovedFrom] — Unity " +
                    "already migrates it in memory when the asset loads. Migrating rewrites the stored type " +
                    "name in the file so it matches the code.",
                    info: true,
                    () => ApplyFix(assetPath, fileId, rid, migrationTarget.AssemblyQualifiedName));
            }

            if (!SerializeReferenceGraphAnalysis.TryGetSuggestion(assetPath, fileId, rid, storedType, _constraints, out var suggestion))
                return null;

            // Safe to hand straight to ApplyFix: Rank's pool is constraint-filtered, so the suggestion is always a
            // type the picker itself would offer.
            return BuildNodeActionRow(
                $"Smart Fix {SerializeReferenceHelpers.GetSuggestionLabel(suggestion)}",
                SerializeReferenceHelpers.GetSuggestionDetail(suggestion),
                info: false,
                () => ApplyFix(assetPath, fileId, rid, suggestion.Type.AssemblyQualifiedName));
        }

        // A one-click action (Smart Fix / Migrate) as a flat accent verb over the same hover fill the Project
        // References action rows use, instead of a filled gradient pill floating over the glass card. Each card
        // keeps one accent: warning amber for a Smart Fix guess on a broken card, info for a pending migration.
        private VisualElement BuildNodeActionRow(string text, string tooltipText, bool info, Action onClick)
        {
            var row = new Label(text).AddClass(NodeActionClass);
            if (info) row.AddClass(NodeActionInfoClass);
            row.tooltip = tooltipText;
            row.RegisterCallback<ClickEvent>(_ => onClick());
            RegisterNavTarget(row, onClick);
            return row;
        }

        // The dim hairline between a card's band and its body, plus — when the band is interactive — the accent
        // underline sweep that scales in while the band is hovered (the Project References group cards' idiom).
        // The sweep is the band's sibling, so USS :hover can't reach it; it rides the card's --header-hover modifier
        // instead, which the ring lights (see RegisterNavBand). Both hide while the picker is docked (see the
        // --picking USS rules).
        private static void AddBandDivider(VisualElement card, AspidGradientButton band, string sweepModifier)
        {
            card.AddChild(new AspidDividingLine(AspidDividingLinePreset.Default
                    .SetTheme(ThemeStyle.Type.Light)
                    .SetSize(AspidDividingLineSizeStyle.Type.Thin))
                .AddClass(NodeDividerClass));

            if (band is null) return;

            var sweep = new VisualElement()
                .AddClass(NodeSweepClass)
                .SetPickingMode(PickingMode.Ignore);
            if (sweepModifier is not null) sweep.AddClass(sweepModifier);
            card.AddChild(sweep);
        }

        // Every card's footer: the field path it sits at (when one was recovered) plus its rid / status word, both
        // selectable so they can be copied out.
        private static VisualElement BuildFooter(string pathLabel, string trailingText)
        {
            var meta = new VisualElement().AddClass(NodeFooterClass);

            if (!string.IsNullOrEmpty(pathLabel))
            {
                meta.AddChild(MakeSelectable(new Label($"{pathLabel}:")
                    .AddClass(NodeRootLabelClass)));
            }

            meta.AddChild(MakeSelectable(new Label(trailingText)
                .AddClass(NodeRidClass)));

            return meta;
        }

        // Matches an empty slot's graph field path (list indices as "[i]") against a GateViolation's FieldPath (Unity's
        // native SerializedProperty form, "Array.data[i]") for the same document — the same normalization
        // TryResolveLiveProperty already applies to reach the live property at this path. Best-effort: a slot whose
        // path could not be recovered by the YAML walk (SerializeReferenceGraphScanner's "reference" fallback) never
        // matches a real property path, so its badge is silently skipped rather than false-positiving — the same
        // violation still shows correctly in the Project References tab.
        private bool IsFieldRequiredUnset(long fileId, string pathLabel)
        {
            if (string.IsNullOrEmpty(pathLabel) || _requiredViolations.Count == 0) return false;

            var propertyPath = SerializeReferenceGraphEditor.ToSerializedPropertyPath(pathLabel);
            foreach (var violation in _requiredViolations)
            {
                if (violation.FileId == fileId && violation.FieldPath == propertyPath) return true;
            }

            return false;
        }
    }
}
