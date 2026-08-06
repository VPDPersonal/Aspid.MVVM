using System;
using System.Linq;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.UIElements.Editors.Internal;
using static Aspid.FastTools.SerializeReferences.Editors.SerializeReferenceAuditUI;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Project-wide repair tool for missing <c>[SerializeReference]</c> types. <c>Scan Project</c> sweeps every text
    /// asset under <c>Assets/</c>, groups the broken references by their stored (now unloadable) type, and offers a
    /// single bulk <c>Fix all</c> per group: one type pick + one confirmation rewrites every entry across every
    /// affected file. Unset <c>[TypeSelector(Required = true)]</c> fields are audited alongside them as a read-only
    /// card that cross-links into the per-asset graph.
    /// </summary>
    /// <remarks>
    /// The implementation is split across partial files by concern: this file owns the chrome, the scan pass and the
    /// results states; <c>.Cards</c> builds the group cards and their rows; <c>.Actions</c> runs the bulk fix, clear
    /// and undo behind the inline picker. The copy lives in the pure
    /// <see cref="SerializeReferenceProjectSummary"/>, the scan model in <see cref="MissingReferenceGroup"/>, and the
    /// file edits in <see cref="SerializeReferenceBatchEditor"/>.
    /// </remarks>
    internal sealed partial class SerializeReferenceProjectView : VisualElement
    {
        private const string StyleSheetPath = "UI/SerializeReferences/Aspid-FastTools-SerializeReference";

        private const string RootClass = "aspid-fasttools-repair-references";
        private const string ContentClass = RootClass + "__content";
        private const string PanelClass = RootClass + "__panel";
        private const string PanelTitleClass = RootClass + "__panel-title";
        private const string PanelDescriptionClass = RootClass + "__panel-description";
        private const string ScanProjectClass = RootClass + "__scan-project";
        private const string EmptyClass = RootClass + "__empty";
        private const string EmptyHiddenClass = EmptyClass + "--hidden";
        private const string EmptyIconClass = RootClass + "__empty-icon";
        private const string EmptyIconInfoClass = EmptyIconClass + "--info";
        private const string EmptyIconSuccessClass = EmptyIconClass + "--success";
        private const string EmptyTitleClass = RootClass + "__empty-title";
        private const string EmptyMessageClass = RootClass + "__empty-message";
        private const string ResultsClass = RootClass + "__results";
        private const string ResultsHiddenClass = ResultsClass + "--hidden";
        private const string ResultsHeaderClass = RootClass + "__results-header";
        private const string ResultsHintClass = RootClass + "__results-hint";
        private const string LegendClass = RootClass + "__legend";
        private const string LegendHiddenClass = LegendClass + "--hidden";
        private const string LegendItemClass = RootClass + "__legend-item";
        private const string LegendDotClass = RootClass + "__legend-dot";
        private const string LegendDotInfoClass = LegendDotClass + "--info";
        private const string LegendTextClass = RootClass + "__legend-text";
        private const string SummaryListClass = RootClass + "__summary-list";
        private const string SummaryClass = RootClass + "__summary";
        private const string SummaryUndoClass = RootClass + "__summary-undo";
        private const string ScrollClass = RootClass + "__scroll";
        private const string NavTargetClass = RootClass + "__nav-target";
        private const string NavTargetFocusedClass = NavTargetClass + "--focused";

        // Scan button label: cold call-to-action before the first scan, quiet refresh once the index is warm.
        private const string ScanLabel = "Scan Project";
        private const string RescanLabel = "Rescan";

        private readonly VisualElement _empty;
        private readonly VisualElement _results;
        private readonly AspidLabel _resultsHeader;
        private readonly VisualElement _summaries;
        private readonly Label _resultsHint;
        private readonly VisualElement _legend;
        private readonly VisualElement _list;
        private readonly AspidGradientButton _scanButton;
        private readonly ScrollView _scroll;

        // Keyboard navigation: one flat focus ring over every actionable element in visual order — Rescan first,
        // then each card's Fix all / action row / entry rows — shared with the other window tabs.
        private readonly NavRing _ring;

        // The one inline picker, docked under whichever Fix all button opened it (see the .Actions partial).
        private readonly AuditPickerHost _picker;

        // The legend's block-specific USS class names for the shared item builder.
        private static readonly LegendClasses _legendClassSet = new(LegendItemClass, LegendDotClass, LegendDotInfoClass, LegendTextClass);

        // Required-violations audit has no incrementally-maintained index like SerializeReferenceTypeUsageIndex, so it
        // is only (re)scanned on an explicit Scan/Rescan click, not on every Initialize() (tab switch would otherwise
        // pay for a full project sweep). Static so the result survives the view being rebuilt on a tab switch.
        private static bool _requiredIsWarm;
        private static IReadOnlyList<GateViolation> _requiredViolationsCache = Array.Empty<GateViolation>();

        private static IReadOnlyList<GateViolation> RequiredViolationsForRender =>
            _requiredIsWarm ? _requiredViolationsCache : Array.Empty<GateViolation>();

        /// <summary>
        /// Jump from a project-audit result row to that asset's Inspect graph. Wired by the host window.
        /// </summary>
        public Action<Object> OnInspectAsset;

        /// <summary>
        /// Reports this view's state to the host window, which owns the shared dotted canvas and washes it with the
        /// matching status. Wired by the window.
        /// </summary>
        public Action<StatusStyle.Type> OnCanvasStatus;

        public SerializeReferenceProjectView()
        {
            var root = this;
            style.flexGrow = 1;
            root.AddAspidThemeStyleSheets()
                .AddStyleSheetsFromResource(StyleSheetPath)
                .AddClass(RootClass);

            var panelTitle = new AspidLabel("Find missing references", AspidLabelPreset.Default
                    .SetLabelTheme(ThemeStyle.Type.Lightness)
                    .SetLabelSize(AspidLabelSizeStyle.Type.H5)
                    .SetLineSize(AspidDividingLineSizeStyle.Type.None))
                .AddClass(PanelTitleClass);

            var panelDescription = new Label(
                    "Sweep every asset under Assets/ for broken [SerializeReference] types and bulk-fix them by type.")
                .AddClass(PanelDescriptionClass);

            // Label flips between ScanLabel and RescanLabel as the index warms.
            _scanButton = new AspidGradientButton(ScanLabel, _ => ScanProject())
                .AddClass(ScanProjectClass);

            var panel = new VisualElement()
                .AddClass(PanelClass)
                .AddChild(panelTitle)
                .AddChild(panelDescription)
                .AddChild(_scanButton);

            _empty = new VisualElement().AddClass(EmptyClass);

            _resultsHeader = new AspidLabel(string.Empty, AspidLabelPreset.Default
                    .SetLabelStatus(StatusStyle.Type.Warning)
                    .SetLabelSize(AspidLabelSizeStyle.Type.H4)
                    .SetLineTheme(ThemeStyle.Type.Dark)
                    .SetLineStatus(StatusStyle.Type.Warning))
                .AddClass(ResultsHeaderClass);

            _resultsHint = new Label(string.Empty).AddClass(ResultsHintClass);

            // Color key for the two card accents; only shown when both are actually on screen (see RenderGroups).
            _legend = new VisualElement()
                .AddClass(LegendClass)
                .AddClass(LegendHiddenClass)
                .AddChild(BuildLegendItem("Broken — pick a replacement", info: false, _legendClassSet))
                .AddChild(BuildLegendItem("Renamed — one-click migrate", info: true, _legendClassSet));

            // Receipt stack: one help-box per bulk Fix all, kept across chained fixes and cleared only on a fresh scan.
            _summaries = new VisualElement().AddClass(SummaryListClass);

            _list = new VisualElement();

            _results = new VisualElement()
                .AddClass(ResultsClass)
                .AddChild(_resultsHeader)
                .AddChild(_resultsHint)
                .AddChild(_legend)
                .AddChild(_summaries)
                .AddChild(_list);

            // One scroll spans the whole view, so the panel scrolls away with the group list instead of staying pinned.
            var content = new VisualElement()
                .AddClass(ContentClass)
                .AddChild(panel)
                .AddChild(_empty)
                .AddChild(_results);

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

            ResetNavTargets();
        }

        // ---------------------------------------------------------------------------------------------------------
        // Keyboard navigation
        // ---------------------------------------------------------------------------------------------------------

        // Every render pass rebuilds the ring from scratch (the old elements are gone with _list.Clear()). The Scan
        // button outlives the render and re-registers here, so a highlight sitting on it comes back with it and
        // Enter-on-Scan keeps its highlight.
        private void ResetNavTargets()
        {
            _ring.Clear(keepFocusedElement: true);
            RegisterNavTarget(_scanButton, ScanProject);
        }

        private void RegisterNavTarget(VisualElement element, Action activate) => _ring.Register(element, activate);

        // ---------------------------------------------------------------------------------------------------------
        // Scan pass
        // ---------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Restores whatever the warm index can already show, or opens idle when nothing has been scanned yet.
        /// </summary>
        /// <remarks>
        /// Cold index: wait for a deliberate Scan click — the cold sweep parses every asset's YAML behind a blocking
        /// bar, so it must never run unasked. Warm index: re-deriving groups is a cheap in-memory filter, so results
        /// survive a tab switch. The breakage-notification deep-link bypasses this and calls
        /// <see cref="ScanProject"/> directly.
        /// </remarks>
        public void Initialize()
        {
            if (SerializeReferenceTypeUsageIndex.IsWarm || _requiredIsWarm) RenderWarmGroups();
            else ShowIdle();
        }

        /// <summary>Sweeps the project for missing references and groups them by stored broken type.</summary>
        /// <remarks>Slow when the index is cold — this is the one deliberate moment the audit pays for a full sweep.</remarks>
        public void ScanProject()
        {
            if (_list is null) return;

            _picker.Close();
            ClearSummaries();

            // Unlike the missing-type index, the required-field scan has nothing incremental behind it (see
            // RequiredViolationsForRender).
            _requiredViolationsCache = CollectRequiredViolations();
            _requiredIsWarm = true;

            RenderWarmGroups();
        }

        // Collects the unresolved set from the warm index and paints it; shared by Scan/Rescan and Initialize's warm restore.
        private void RenderWarmGroups()
        {
            if (_list is null) return;
            if (_scanButton is not null) _scanButton.Text = RescanLabel;

            RenderGroups(MissingReferenceGroup.CollectFromIndex(), RequiredViolationsForRender);
        }

        // Full project sweep for unset [TypeSelector(Required = true)] fields, reusing the same headless scanner the
        // build/CI gate uses. Skipped entirely when the gate is switched Off — a required audit nobody wants to fail
        // or warn on shouldn't cost a full-project YAML sweep on every Scan click either.
        private static IReadOnlyList<GateViolation> CollectRequiredViolations() =>
            SerializeReferenceSettings.BuildSeverity == GateSeverity.Off
                ? Array.Empty<GateViolation>()
                : SerializeReferenceGateScanner.Scan(GateOptions.RequiredOnly);

        // Paints a collected group set: count header + hint + one card per broken-type group plus one Required
        // violations card, or the terminal hero when both are empty. The bulk actions special-case the came-back-clean
        // case so their summary HelpBox survives (see ShowMissingReferencesClean).
        private void RenderGroups(List<MissingReferenceGroup> groups, IReadOnlyList<GateViolation> requiredViolations)
        {
            _list.Clear();
            ResetNavTargets();

            var missingCount = groups.Sum(group => group.Entries.Count);
            var requiredCount = requiredViolations.Count;

            if (missingCount == 0 && requiredCount == 0)
            {
                ShowEmptyState(
                    success: true,
                    title: "Project clean",
                    message: "No missing managed references or unset required fields found anywhere under Assets/.");
                return;
            }

            // Pending migrations sink to the very bottom, below the Required violations card too: the whole amber
            // band (broken groups, then required fields) stacks first and the calm blue one-click cards close the
            // list. Each band keeps the scanner's order.
            var migrations = new List<(MissingReferenceGroup Group, MissingReferenceMigration Migration)>();
            foreach (var group in groups)
            {
                // Resolve constraint + migration ONCE per group and reuse it for the card and picker label below, so
                // the partition and the card can never disagree on whether a group is a migration.
                var migration = new MissingReferenceMigration(group);
                if (migration.IsMigration) migrations.Add((group, migration));
                else _list.AddChild(BuildGroupCard(group, migration));
            }

            // The header splits the migration entries out of the missing count — a [MovedFrom] rename with a
            // one-click fix shouldn't inflate the alarm number.
            var migrationCount = migrations.Sum(entry => entry.Group.Entries.Count);
            ShowResults(
                SerializeReferenceProjectSummary.BuildResultsHeaderText(missingCount - migrationCount, migrationCount, requiredCount),
                StatusStyle.Type.Warning);
            _resultsHint.text = SerializeReferenceProjectSummary.BuildResultsHintText(requiredCount > 0);

            // The amber/blue key only earns its row when both accents are on screen at once.
            var hasAmber = groups.Count > migrations.Count || requiredCount > 0;
            _legend.EnableInClassList(LegendHiddenClass, migrations.Count == 0 || !hasAmber);

            if (requiredCount > 0)
                _list.AddChild(BuildRequiredGroupCard(requiredViolations));

            foreach (var (group, migration) in migrations)
                _list.AddChild(BuildGroupCard(group, migration));
        }

        // Re-derives the groups after a bulk edit and repaints. A group set that came back empty stays in the results
        // region rather than the "Project clean" hero, which would hide the fix's summary receipt — the hero is
        // reserved for an explicit Rescan.
        private void RerenderAfterBulkEdit()
        {
            if (_scanButton is not null) _scanButton.Text = RescanLabel;

            var groups = MissingReferenceGroup.CollectFromIndex();
            if (groups.Count == 0) ShowMissingReferencesClean();
            else RenderGroups(groups, RequiredViolationsForRender);
        }

        // ---------------------------------------------------------------------------------------------------------
        // View states
        // ---------------------------------------------------------------------------------------------------------

        // Shared "no missing references left" branch for the bulk actions: stays in the results region (not the
        // clean-state hero) so the fix's summary receipt survives, while still surfacing whatever Required violations
        // card RequiredViolationsForRender currently reports (empty right after a clear-to-null, which invalidates the
        // cache instead of risking a stale under-report — see ClearGroupToNull).
        private void ShowMissingReferencesClean()
        {
            _list.Clear();
            ResetNavTargets();
            var requiredViolations = RequiredViolationsForRender;

            ShowResults(
                requiredViolations.Count == 0
                    ? "No missing references"
                    : $"No missing references, {BuildCountText(requiredViolations.Count, "required violation")}",
                StatusStyle.Type.Success);
            _resultsHint.text = "Nothing left to repair. Rescan to sweep the project again and confirm it's clean.";
            _legend.AddClass(LegendHiddenClass);

            if (requiredViolations.Count > 0)
                _list.AddChild(BuildRequiredGroupCard(requiredViolations));
        }

        private void ShowEmptyState(bool success, string title, string message)
        {
            ResetNavTargets();
            _results.AddClass(ResultsHiddenClass);
            _empty.RemoveClass(EmptyHiddenClass);
            _empty.Clear();
            OnCanvasStatus?.Invoke(success ? StatusStyle.Type.Success : StatusStyle.Type.Info);

            var icon = new VisualElement()
                .AddClass(EmptyIconClass)
                .AddClass(success ? EmptyIconSuccessClass : EmptyIconInfoClass);

            var titlePreset = AspidLabelPreset.Default
                .SetLabelTheme(success ? ThemeStyle.Type.Light : ThemeStyle.Type.Lightness)
                .SetLabelSize(AspidLabelSizeStyle.Type.H3)
                .SetLineSize(AspidDividingLineSizeStyle.Type.None);

            if (success) titlePreset = titlePreset.SetLabelStatus(StatusStyle.Type.Success);

            _empty.AddChild(icon)
                .AddChild(new AspidLabel(title, titlePreset).AddClass(EmptyTitleClass))
                .AddChild(new Label(message).AddClass(EmptyMessageClass));
        }

        // Cold-index idle state until the first scan. No results list yet — the project is unscanned, so "clean"
        // cannot be claimed.
        private void ShowIdle() => ShowEmptyState(
            success: false,
            title: "Project not scanned",
            message: "Run Scan Project to map every broken [SerializeReference] type across your assets — then repair each missing type in bulk.");

        // The status is explicit per call site: the missing-references sweep washes Warning, while the came-back-clean
        // receipt washes Success rather than leaving a clean state on an amber backdrop.
        private void ShowResults(string headerText, StatusStyle.Type status)
        {
            _empty.AddClass(EmptyHiddenClass);
            _results.RemoveClass(ResultsHiddenClass);
            _resultsHeader.Text = headerText;
            OnCanvasStatus?.Invoke(status);
        }

        // Appends one receipt to the running stack (newest at the bottom) rather than overwriting the previous; only
        // ClearSummaries resets it on the next fresh scan. The Undo button reverts exactly this fix.
        private void ShowSummary(string title, string message, Action<VisualElement> onUndo)
        {
            var summary = new AspidHelpBox(AspidHelpBoxPreset.Default.SetMessageType(HelpBoxMessageType.Warning))
                .AddClass(SummaryClass);
            summary.Title = title;
            summary.Message = message;

            if (onUndo is not null)
                summary.AddChild(new AspidGradientButton("Undo", _ => onUndo(summary)).AddClass(SummaryUndoClass));

            _summaries.AddChild(summary);
        }

        private void ClearSummaries() => _summaries?.Clear();
    }
}
