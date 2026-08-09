using System;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using System.Collections.Generic;
using System.Linq;
using Aspid.FastTools.UIElements.Editors.Internal;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Asset-level visualiser for <c>[SerializeReference]</c> managed-reference graphs. For each serialized object
    /// document in the asset it draws the reference tree — field-pointer roots, their nested children, shared
    /// (aliased) references and orphaned payloads — straight from the YAML, so it surfaces references at any nesting
    /// depth and the orphans the Inspector cannot navigate to. Every reference card is an inline type dropdown: the
    /// same embedded picker the Repair window uses, anchored under the clicked card, where picking a type assigns /
    /// re-points the reference and <c>&lt;None&gt;</c> clears it. Orphaned payloads no field reaches carry a
    /// <c>Clear</c> action.
    /// </summary>
    /// <remarks>
    /// The implementation is split across partial files by concern: this file owns the chrome, the scan pass and the
    /// overview; <c>.Cards</c> lays out each document and walks its tree; <c>.Nodes</c> builds the individual cards;
    /// <c>.Picker</c> opens the inline type pickers and applies what they pick. The counting and copy live in the pure
    /// <see cref="SerializeReferenceGraphAnalysis"/> / <see cref="SerializeReferenceGraphSummary"/>, and every edit is
    /// performed by <see cref="SerializeReferenceGraphEditor"/> — this view only decides when to re-render.
    /// </remarks>
    internal sealed partial class SerializeReferenceGraphView : VisualElement
    {
        private const string StyleSheetPath = "UI/SerializeReferences/Aspid-FastTools-ReferenceGraph";

        private const string RootClass = "aspid-fasttools-reference-graph";
        private const string ContentClass = RootClass + "__content";
        private const string CardClass = RootClass + "__card";
        private const string CardTitleClass = RootClass + "__card-title";
        private const string CardDescriptionClass = RootClass + "__card-description";
        private const string AssetClass = RootClass + "__asset";
        private const string RescanClass = RootClass + "__rescan";
        private const string EmptyClass = RootClass + "__empty";
        private const string EmptyHiddenClass = EmptyClass + "--hidden";
        private const string EmptyIconClass = RootClass + "__empty-icon";
        private const string EmptyIconInfoClass = EmptyIconClass + "--info";
        private const string EmptyTitleClass = RootClass + "__empty-title";
        private const string EmptyMessageClass = RootClass + "__empty-message";
        private const string ScrollClass = RootClass + "__scroll";
        private const string ListClass = RootClass + "__list";
        private const string ListHiddenClass = ListClass + "--hidden";

        private const string OverviewClass = RootClass + "__overview";
        private const string OverviewHiddenClass = OverviewClass + "--hidden";
        private const string OverviewTitleClass = RootClass + "__overview-title";
        private const string OverviewHintClass = RootClass + "__overview-hint";

        private const string LegendClass = RootClass + "__legend";
        private const string LegendHiddenClass = LegendClass + "--hidden";
        private const string LegendItemClass = RootClass + "__legend-item";
        private const string LegendDotClass = RootClass + "__legend-dot";
        private const string LegendDotInfoClass = LegendDotClass + "--info";
        private const string LegendTextClass = RootClass + "__legend-text";

        private const string NavTargetClass = RootClass + "__nav-target";
        private const string NavTargetFocusedClass = NavTargetClass + "--focused";

        // Reports this view's state to the host window, which owns the shared dotted canvas behind every mode and
        // washes it with the matching status.
        private readonly Action<StatusStyle.Type> _onCanvasStatus;

        // Reports a target change to the host window: it rebuilds this view from its cached target on every tab switch,
        // so without this an in-view pick would be dropped on the next return to this tab.
        private readonly Action<Object> _onTargetChanged;

        private Object _target;
        private readonly ObjectField _assetField;
        private readonly AspidGradientButton _rescanButton;
        private readonly VisualElement _empty;
        private readonly VisualElement _overview;
        private readonly AspidLabel _overviewTitle;
        private readonly Label _overviewHint;
        private readonly VisualElement _legend;
        private readonly VisualElement _list;
        private readonly ScrollView _scroll;

        // Keyboard navigation: one flat focus ring over every actionable element in visual order — Rescan first, then
        // each document header, node band, action row and orphan Clear — shared with the other window tabs, so a
        // member hidden inside a collapsed document band drops out of the ring (see NavRing).
        private readonly NavRing _ring;

        // The one inline picker, docked under whichever band opened it (see the .Picker partial).
        private readonly AuditPickerHost _picker;

        // Per-asset declared-field-type map, shared by every constraint question one render pass asks. Cleared on
        // every Rescan so a rewritten file is re-read rather than answered from the pre-edit map.
        private readonly SerializeReferenceConstraintCache _constraints = new();

        // The legend's block-specific USS class names for the shared item builder.
        private static readonly SerializeReferenceAuditUI.LegendClasses _legendClassSet =
            new(LegendItemClass, LegendDotClass, LegendDotInfoClass, LegendTextClass);

        // Unset [TypeSelector(Required = true)] fields for the current asset, refreshed on every Rescan. Populated
        // straight from SerializeReferenceGateScanner — the same required-field check the Project References audit
        // and the build/CI gate use — so the amber required styling here always agrees with them. A required
        // string/SerializableType field has no rid and so no graph node; it gets its own trailing card instead
        // (BuildRequiredOnlyCard).
        private IReadOnlyList<GateViolation> _requiredViolations = Array.Empty<GateViolation>();

        public SerializeReferenceGraphView(Object target, Action<StatusStyle.Type> onCanvasStatus, Action<Object> onTargetChanged = null)
        {
            _target = target;
            _onCanvasStatus = onCanvasStatus;
            _onTargetChanged = onTargetChanged;

            var root = this;
            style.flexGrow = 1;
            root.AddAspidThemeStyleSheets()
                .AddStyleSheetsFromResource(StyleSheetPath)
                .AddClass(RootClass);

            var cardTitle = new AspidLabel("Inspect asset", AspidLabelPreset.Default
                    .SetLabelTheme(ThemeStyle.Type.Lightness)
                    .SetLabelSize(AspidLabelSizeStyle.Type.H5)
                    .SetLineSize(AspidDividingLineSizeStyle.Type.None))
                .AddClass(CardTitleClass);

            var cardDescription = new Label(
                    "Map a saved asset's [SerializeReference] graph and repair missing types inline.")
                .AddClass(CardDescriptionClass);

            _assetField = new ObjectField
            {
                objectType = typeof(Object),
                allowSceneObjects = false,
                value = _target,
            };
            _assetField.AddClass(AssetClass);
            _assetField.RegisterValueChangedCallback(evt => SetTarget(evt.newValue));

            // The field is hosted inside the Rescan button: swallow its presses so opening the object picker or
            // dragging an asset in doesn't bubble to the button's Clickable and re-run Rescan.
            _assetField.RegisterCallback<PointerDownEvent>(evt => evt.StopPropagation());

            _rescanButton = new AspidGradientButton("Rescan", _ => Rescan())
                .AddClass(RescanClass);
            _rescanButton.AddTrailingContent(_assetField);
            _rescanButton.FillWithTrailingContent();

            var card = new AspidBox(AspidBoxPreset.Default.SetTheme(ThemeStyle.Type.Darkness))
                .AddClass(CardClass)
                .AddChild(cardTitle)
                .AddChild(cardDescription)
                .AddChild(_rescanButton);

            _empty = new VisualElement().AddClass(EmptyClass);

            _overviewTitle = new AspidLabel(string.Empty, AspidLabelPreset.Default
                    .SetLabelStatus(StatusStyle.Type.Warning)
                    .SetLabelSize(AspidLabelSizeStyle.Type.H4)
                    .SetLineTheme(ThemeStyle.Type.Dark)
                    .SetLineStatus(StatusStyle.Type.Warning))
                .AddClass(OverviewTitleClass);

            _overviewHint = new Label(string.Empty).AddClass(OverviewHintClass);

            // Color key for the two card accents; only shown when both are actually on screen (see ShowOverview) —
            // the same amber/blue legend the Project References audit renders under its hint.
            _legend = new VisualElement()
                .AddClass(LegendClass)
                .AddClass(LegendHiddenClass)
                .AddChild(SerializeReferenceAuditUI.BuildLegendItem("Broken — pick a replacement", info: false, _legendClassSet))
                .AddChild(SerializeReferenceAuditUI.BuildLegendItem("Renamed — one-click migrate", info: true, _legendClassSet));

            _overview = new VisualElement()
                .AddClass(OverviewClass)
                .AddClass(OverviewHiddenClass)
                .AddChild(_overviewTitle)
                .AddChild(_overviewHint)
                .AddChild(_legend);

            _list = new VisualElement().AddClass(ListClass);

            // One scroll spans the whole view, so the card and overview scroll away with the document list rather than
            // staying pinned above a separately-scrolling list.
            var content = new VisualElement()
                .AddClass(ContentClass)
                .AddChild(card)
                .AddChild(_empty)
                .AddChild(_overview)
                .AddChild(_list);

            _scroll = new ScrollView().AddClass(ScrollClass);
            _scroll.AddChild(content);

            root.AddChild(_scroll);

            _picker = new AuditPickerHost(this, _list, _pickerClassSet);

            // The shared keyboard ring: the root holds focus (grabbed on attach, re-grabbed when the picker closes)
            // so keys reach it before anything is highlighted. Suspended while a type picker owns the keyboard.
            _ring = new NavRing(
                host: this,
                navTargetClass: NavTargetClass,
                focusedClass: NavTargetFocusedClass,
                scrollTo: element => _scroll.ScrollTo(element),
                isSuspended: () => _picker.IsOpen);

            Rescan();
        }

        // ---------------------------------------------------------------------------------------------------------
        // Keyboard navigation (mirrors SerializeReferenceProjectView)
        // ---------------------------------------------------------------------------------------------------------

        // Every render pass rebuilds the ring from scratch (the old elements are gone with _list.Clear()). The Rescan
        // button outlives the render and re-registers here, so a highlight sitting on it comes back with it and
        // Enter-on-Rescan keeps its highlight.
        private void ResetNavTargets()
        {
            _ring.Clear(keepFocusedElement: true);
            RegisterNavTarget(_rescanButton, () => Rescan());
        }

        private void RegisterNavTarget(VisualElement element, Action activate) => _ring.Register(element, activate);

        // A node band is its card's header: the ring drives the underline sweep AddBandDivider hangs under it, so the
        // sweep reads the same whether the band is hovered or holds the keyboard highlight.
        private void RegisterNavBand(AspidGradientButton band, VisualElement card, Action activate) =>
            _ring.RegisterHeader(band, card, NodeHeaderHoverClass, activate);

        // ---------------------------------------------------------------------------------------------------------
        // Scan pass
        // ---------------------------------------------------------------------------------------------------------

        private void SetTarget(Object target)
        {
            _target = target;
            // Mirror the pick back to the host so its cached target follows; the host just stores it (no rebuild),
            // so this never re-enters.
            _onTargetChanged?.Invoke(target);
            // Open() retargets an already-open window, so the field must follow the new target — without notifying,
            // or the change callback would trigger a second scan.
            _assetField?.SetValueWithoutNotify(target);
            if (_list is not null) Rescan();
        }

        private void Rescan(List<ReferenceGraphDocument> prebuilt = null)
        {
            if (_list is null) return;

            _picker.Close();
            // Drop the constraint maps so a rescan after a fix / clear re-reads the rewritten YAML, not a stale map.
            _constraints.Clear();
            _list.Clear();
            ResetNavTargets();
            _requiredViolations = Array.Empty<GateViolation>();

            var assetPath = _target ? AssetDatabase.GetAssetPath(_target) : null;
            if (string.IsNullOrEmpty(assetPath))
            {
                if (!TryOfferSourcePrefab())
                {
                    ShowEmpty(
                        "No asset selected",
                        "Select a saved asset (a prefab or ScriptableObject) to map its managed-reference graph.");
                }

                return;
            }

            var documents = prebuilt ?? SerializeReferenceGraphScanner.Build(assetPath);

            // Same headless scanner as the Project References audit and the build/CI gate, scoped to this one asset.
            // Read before the empty-graph bail: a string / SerializableType required field has no rid and so never
            // produces a document (SerializeReferenceGraphScanner only emits one for an object with a RefIds block),
            // so it can be the ONLY thing this asset has to show even when the managed-reference graph is empty.
            _requiredViolations = SerializeReferenceGateScanner.ScanAssetRequiredFields(assetPath);

            if (documents.Count == 0 && _requiredViolations.Count == 0)
            {
                ShowEmpty(
                    "No managed references",
                    "This asset has no [SerializeReference] managed references to map.");
                return;
            }

            ShowResults();
            RenderDocuments(assetPath, documents);
        }

        // A nested prefab instance keeps its managed-reference data in the source prefab, not the host, so offer to
        // retarget the graph onto that source where the RefIds actually live.
        private bool TryOfferSourcePrefab()
        {
            if (!SerializeReferenceHelpers.TryGetSourcePrefabPath(_target, out var sourcePath)) return false;

            ShowResults();
            _onCanvasStatus?.Invoke(StatusStyle.Type.Info);

            var info = new AspidHelpBox(AspidHelpBoxPreset.Default.SetMessageType(HelpBoxMessageType.Info))
                .SetMessage("This is a prefab instance — its managed references live in the source prefab.");
            _list.AddChild(info);

            void OpenSource() => SetTarget(AssetDatabase.LoadAssetAtPath<Object>(sourcePath));

            var openSource = new AspidGradientButton("Open Source Prefab", _ => OpenSource());
            RegisterNavTarget(openSource, OpenSource);
            _list.AddChild(openSource);
            return true;
        }

        // Paints every document card, tallies what they hold and reports one verdict to both the overview and the
        // window canvas — so the headline's tint and the wash behind it can never disagree.
        private void RenderDocuments(string assetPath, List<ReferenceGraphDocument> documents)
        {
            // Empty (unassigned) slots are tallied separately: they are not broken, so they never tip the
            // headline / canvas to amber — they only surface in the dim hint.
            var total = 0;
            var missing = 0;
            var orphans = 0;
            var empties = 0;
            var migrations = 0;

            // Every empty managed-reference slot's normalized path, gathered up front so the required-only cards
            // below can tell "already badged on a graph card" apart from "no graph node exists for this field at
            // all" (a string / SerializableType required field, or an empty slot under a document the scanner
            // failed to reach) without re-walking the tree per violation.
            var emptySlotPaths = SerializeReferenceGraphAnalysis.CollectEmptySlotPaths(documents);

            var showHeaders = documents.Count > 1;
            foreach (var document in documents)
            {
                _list.AddChild(BuildDocument(assetPath, document, showHeaders));

                total += document.Nodes.Count;
                var (broken, documentMigrations) = SerializeReferenceGraphAnalysis.CountUnresolved(assetPath, document, _constraints);
                missing += broken + documentMigrations;
                migrations += documentMigrations;
                orphans += document.Orphans.Count;
                empties += SerializeReferenceGraphAnalysis.CountEmptySlots(document);
            }

            // Fields the graph has no node for at all: string / SerializableType required fields (never threaded
            // into RefIds) plus, defensively, any managed-reference violation the graph walk could not place.
            var ungraphedRequired = _requiredViolations
                .Where(violation => !emptySlotPaths.Contains((violation.FileId, violation.FieldPath)))
                .ToList();

            // The headline's "unassigned fields" note counts only slots that are allowed to stay empty — a required
            // empty slot is reported through the required count instead, never twice.
            var required = _requiredViolations.Count;
            var graphedRequired = required - ungraphedRequired.Count;

            // Pending migrations are not breakages — a graph whose only annotations are migrations reads info-blue,
            // matching the Project References group card.
            var status = SerializeReferenceAuditUI.ResolveStatus(missing - migrations, orphans, required, migrations);
            ShowOverview(status, total, missing, orphans, Math.Max(0, empties - graphedRequired), migrations, required);

            if (ungraphedRequired.Count > 0)
            {
                // One resolver for the whole batch: several violations commonly share a component, and it memoises
                // the object loads per asset path.
                var labels = new ViolationFieldLabels();
                foreach (var violation in ungraphedRequired)
                    _list.AddChild(BuildRequiredOnlyCard(violation, labels));
            }

            _onCanvasStatus?.Invoke(status);
        }

        // ---------------------------------------------------------------------------------------------------------
        // View states
        // ---------------------------------------------------------------------------------------------------------

        private void ShowEmpty(string title, string message)
        {
            HideOverview();
            _list.AddClass(ListHiddenClass);
            _empty.RemoveClass(EmptyHiddenClass);
            _empty.Clear();
            _onCanvasStatus?.Invoke(StatusStyle.Type.Info);

            var icon = new VisualElement()
                .AddClass(EmptyIconClass)
                .AddClass(EmptyIconInfoClass);

            _empty.AddChild(icon)
                .AddChild(new AspidLabel(title, AspidLabelPreset.Default
                        .SetLabelTheme(ThemeStyle.Type.Lightness)
                        .SetLabelSize(AspidLabelSizeStyle.Type.H3)
                        .SetLineSize(AspidDividingLineSizeStyle.Type.None))
                    .AddClass(EmptyTitleClass))
                .AddChild(new Label(message).AddClass(EmptyMessageClass));
        }

        private void ShowResults()
        {
            // The overview stays hidden here; only the document-graph path (RenderDocuments) re-shows it, so the
            // prefab-instance branch that reuses ShowResults keeps the missing-reference headline suppressed.
            HideOverview();
            _empty.AddClass(EmptyHiddenClass);
            _list.RemoveClass(ListHiddenClass);
        }

        private void ShowOverview(StatusStyle.Type status, int total, int missing, int orphans, int empties, int migrations, int required)
        {
            var broken = missing - migrations;

            _overviewTitle.Text = SerializeReferenceGraphSummary.BuildOverviewTitle(broken, orphans, migrations, required);
            _overviewTitle.LabelStatus = status;
            _overviewTitle.LineStatus = status;

            _overviewHint.text = SerializeReferenceGraphSummary.BuildOverviewHint(total, missing, orphans, empties, migrations, required);

            // The amber/blue key only earns its row when both accents are on screen at once (see the Project
            // References legend).
            var hasAmber = broken > 0 || orphans > 0 || required > 0;
            _legend.EnableInClassList(LegendHiddenClass, migrations == 0 || !hasAmber);

            _overview.RemoveClass(OverviewHiddenClass);
        }

        private void HideOverview() => _overview?.AddClass(OverviewHiddenClass);

        // Future work: a "Make unique" action on a SHARED node — cloning the aliased reference so the two fields no
        // longer affect each other (mirrors SerializeReferenceHelpers.MakeReferenceUnique).
    }
}
