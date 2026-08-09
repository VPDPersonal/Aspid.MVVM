using System;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Editable list of scan-excluded project folders, replacing the free-text "one path per line" field with a proper
    /// add/remove list. A flat member of its settings section card — no panel frame of its own: a header row pairs the
    /// "Excluded scan folders" caption with a dim status hint and a "+" pinned at the right edge (the whole header is
    /// the add target and washes green on hover), then one indented flat row per excluded folder (path on the left, an
    /// ✕ remove on the right). Clicking the header opens a folder picker and stores the project-relative path;
    /// clicking a folder row re-opens the picker to re-point that entry in place. Reads and writes
    /// <see cref="SerializeReferenceSettings.ExcludedFolders"/> and rebuilds on
    /// <see cref="SerializeReferenceSettings.ExcludedFoldersChanged"/>, so the in-window Settings tab and the Project
    /// Settings page stay mirrored. Self-contained styling (palette + own USS) so it renders on both surfaces.
    /// </summary>
    internal sealed class SerializeReferenceExcludedFoldersField : VisualElement
    {
        private const string StyleSheetPath =
            "UI/SerializeReferences/Aspid-FastTools-SerializeReference-ExcludedFolders";

        private const string RootClass = "aspid-fasttools-excluded-folders";
        private const string HeaderClass = "aspid-fasttools-excluded-folders__header";
        private const string HeaderCaptionClass = "aspid-fasttools-excluded-folders__header-caption";
        private const string HeaderAddClass = "aspid-fasttools-excluded-folders__header--add";
        private const string ListClass = "aspid-fasttools-excluded-folders__list";
        private const string EntryClass = "aspid-fasttools-excluded-folders__entry";
        private const string EntryHoverClass = "aspid-fasttools-excluded-folders__entry--hover";
        private const string EntryDangerClass = "aspid-fasttools-excluded-folders__entry--danger";
        private const string HintClass = "aspid-fasttools-excluded-folders__hint";
        private const string PathClass = "aspid-fasttools-excluded-folders__path";
        private const string RemoveClass = "aspid-fasttools-excluded-folders__remove";
        private const string AddButtonClass = "aspid-fasttools-excluded-folders__add";

        // The two mutually-exclusive full-row hover tints on folder rows; SetTint is their single writer.
        private static readonly string[] HoverTints = { EntryHoverClass, EntryDangerClass };

        private readonly VisualElement _list;
        private readonly VisualElement _header;
        private readonly Label _hint;

        // The current folder rows with their paths, refreshed by Rebuild — the source for GetNavTargets, so the
        // keyboard ring can walk the rows exactly as the pointer can.
        private readonly List<(VisualElement Row, string Path)> _rows = new();

        /// <summary>
        /// Raised after the folder rows are rebuilt (add / remove / external change). The hosting keyboard ring
        /// listens to re-collect its targets, since every rebuild replaces the row elements.
        /// </summary>
        internal event Action RowsRebuilt;

        public SerializeReferenceExcludedFoldersField()
        {
            this.AddClass(RootClass)
                .AddStyleSheetsFromResource(StyleSheetPath)
                .AddAspidThemeStyleSheets();

            // The whole header row is the add target (the flat action-row idiom); the "+" and the dim status hint are
            // passive Labels so their clicks bubble up to the row instead of firing a second add. The green add-intent
            // wash is code-toggled (--add) so the keyboard ring can mirror it via the same class family.
            _hint = new Label { tooltip = "Add folder" }.AddClass(HintClass);
            var caption = new Label("Excluded scan folders").AddClass(HeaderCaptionClass);
            var addGlyph = new Label("+") { tooltip = "Add folder" }.AddClass(AddButtonClass);

            _header = new VisualElement().AddClass(HeaderClass)
                .AddChild(caption)
                .AddChild(_hint)
                .AddChild(addGlyph);
            _header.RegisterCallback<ClickEvent>(_ => AddFolder());
            _header.RegisterCallback<PointerEnterEvent>(_ => _header.AddToClassList(HeaderAddClass));
            _header.RegisterCallback<PointerLeaveEvent>(_ => _header.RemoveFromClassList(HeaderAddClass));

            _list = new VisualElement().AddClass(ListClass);

            Add(_header);
            Add(_list);

            Rebuild();

            // ExcludedFoldersChanged (not the broad Changed) fires only when the set really moves. Armed from build
            // time, then follows the panel lifecycle: docking re-parents the tree (a detach then an attach WITHOUT a
            // rebuild), which would kill a build-time-only subscription (see AspidSettingsUI.SyncFromSettings).
            var subscribed = false;

            void Arm()
            {
                if (subscribed) return;
                subscribed = true;
                SerializeReferenceSettings.ExcludedFoldersChanged += Rebuild;
                Rebuild();
            }

            void Disarm()
            {
                if (!subscribed) return;
                subscribed = false;
                SerializeReferenceSettings.ExcludedFoldersChanged -= Rebuild;
            }

            RegisterCallback<AttachToPanelEvent>(_ => Arm());
            RegisterCallback<DetachFromPanelEvent>(_ => Disarm());
            Arm();
        }

        private void Rebuild()
        {
            _list.Clear();
            _rows.Clear();

            var folders = SerializeReferenceSettings.ExcludedFolders;
            _hint.text = folders.Length == 0 ? "No excluded folders" : string.Empty;

            foreach (var path in folders)
            {
                // The path label fills the row left of the ✕ (full height), so clicking it anywhere edits.
                var label = new Label(path) { tooltip = path }.AddClass(PathClass);
                label.RegisterCallback<ClickEvent>(_ => Edit(path));

                var remove = new Button(() => Remove(path)) { text = "✕", tooltip = "Remove" }.AddClass(RemoveClass);

                var row = new VisualElement().AddClass(EntryClass)
                    .AddChild(label)
                    .AddChild(remove);

                TintWhileOver(row, label, EntryHoverClass, null);
                TintWhileOver(row, remove, EntryDangerClass, null);

                _list.Add(row);
                _rows.Add((row, path));
            }

            RowsRebuilt?.Invoke();
        }

        // Tints the whole entry while the pointer is over the zone (leaving falls back to fallback; null clears) —
        // USS can't tint a row from a child's hover state.
        private static void TintWhileOver(VisualElement entry, VisualElement zone, string tint, string fallback)
        {
            zone.RegisterCallback<PointerEnterEvent>(_ => SetTint(entry, tint));
            zone.RegisterCallback<PointerLeaveEvent>(_ => SetTint(entry, fallback));
        }

        // Sets exactly one full-row hover tint (null clears all); single-writer keeps the tints mutually exclusive.
        private static void SetTint(VisualElement entry, string tint)
        {
            foreach (var cls in HoverTints) entry.EnableInClassList(cls, cls == tint);
        }

        /// <summary>
        /// The control's keyboard-ring members in visual order, mirroring the pointer affordances exactly: the header
        /// row first (activate = the add-folder picker), then one member per folder row (activate = the edit picker —
        /// what clicking the row does; remove = what its ✕ does, for the ring's Delete/Backspace). Re-collect on
        /// <see cref="RowsRebuilt"/> — a rebuild replaces every row element.
        /// </summary>
        internal IEnumerable<(VisualElement Element, Action Activate, Action Remove)> GetNavTargets()
        {
            yield return (_header, AddFolder, null);

            foreach (var (row, path) in _rows)
                yield return (row, () => Edit(path), () => Remove(path));
        }

        private void AddFolder()
        {
            var relative = PickProjectFolder("Exclude folder from scan", "Assets");
            if (relative == null) return;

            var current = SerializeReferenceSettings.ExcludedFolders;
            if (current.Contains(relative)) return;

            SerializeReferenceSettings.ExcludedFolders = current.Append(relative).ToArray();
        }

        // Re-points a folder row in place; a pick that lands on an existing entry collapses onto it (Distinct).
        private void Edit(string folder)
        {
            var relative = PickProjectFolder("Edit excluded folder", folder);
            if (relative == null || string.Equals(relative, folder, StringComparison.Ordinal)) return;

            SerializeReferenceSettings.ExcludedFolders = SerializeReferenceSettings.ExcludedFolders
                .Select(f => string.Equals(f, folder, StringComparison.Ordinal) ? relative : f)
                .Distinct()
                .ToArray();
        }

        // Returns the picked folder as a project-relative path, or null on cancel or an outside-project pick
        // (the latter explains itself via a dialog).
        private static string PickProjectFolder(string title, string startFolder)
        {
            var absolute = EditorUtility.OpenFolderPanel(title, startFolder, string.Empty);
            if (string.IsNullOrEmpty(absolute)) return null;

            var relative = FileUtil.GetProjectRelativePath(absolute);
            if (!string.IsNullOrEmpty(relative)) return relative;

            EditorUtility.DisplayDialog(
                "Folder outside project",
                "Pick a folder inside the project (under Assets/ or Packages/).",
                "OK");
            return null;
        }

        private void Remove(string folder) =>
            SerializeReferenceSettings.ExcludedFolders = SerializeReferenceSettings.ExcludedFolders
                .Where(f => !string.Equals(f, folder, StringComparison.Ordinal))
                .ToArray();
    }
}
