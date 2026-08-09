using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Shared building blocks for the two References audit tabs (Asset References / Project References), so their count
    /// wording, selectable-label setup and the amber/blue accent legend read identically and can't drift apart. Each
    /// tab keeps its own USS block, so the legend builder wears the block's class names passed as a
    /// <see cref="LegendClasses"/>.
    /// </summary>
    internal static class SerializeReferenceAuditUI
    {
        /// <summary>
        /// "1 entry" / "3 entries" — singular as-is, plural via a naive y→ies / +s rule (enough for the audit's fixed
        /// nouns: reference, migration, violation, entry, file).
        /// </summary>
        public static string BuildCountText(int count, string noun) =>
            count == 1 ? $"1 {noun}" : $"{count} {(noun.EndsWith("y") ? noun[..^1] + "ies" : noun + "s")}";

        /// <summary>
        /// The audit's shared severity verdict, driving both a headline's own tint and the window canvas wash behind
        /// it: anything broken, orphaned or required-unset is amber; a graph whose only findings are pending
        /// <c>[MovedFrom]</c> migrations is info-blue (a stale file is not a breakage); otherwise green.
        /// </summary>
        /// <param name="broken">Missing references, EXCLUDING the pending migrations counted separately.</param>
        public static StatusStyle.Type ResolveStatus(int broken, int orphans, int required, int migrations) =>
            broken > 0 || orphans > 0 || required > 0
                ? StatusStyle.Type.Warning
                : migrations > 0
                    ? StatusStyle.Type.Info
                    : StatusStyle.Type.Success;

        /// <summary>
        /// Makes an audit row's text (asset paths, rids, field paths) selectable so it can be copied out; callers that
        /// also carry a row click gate the click on an empty selection (a drag-select ends in a click too).
        /// </summary>
        public static Label MakeSelectable(Label label)
        {
            label.selection.isSelectable = true;
            label.selection.doubleClickSelectsWord = true;
            label.selection.tripleClickSelectsLine = true;
            return label;
        }

        /// <summary>
        /// One dot + caption a pair of the accent legend: amber (default) for the broken/orphaned/required band, info
        /// blue (<paramref name="info"/> true) for the pending-migration cards, wearing the block's own legend classes.
        /// </summary>
        public static VisualElement BuildLegendItem(string text, bool info, in LegendClasses classes)
        {
            var dot = new VisualElement().AddClass(classes.Dot);
            if (info) dot.AddClass(classes.DotInfo);

            return new VisualElement()
                .AddClass(classes.Item)
                .AddChild(dot)
                .AddChild(new Label(text).AddClass(classes.Text));
        }

        /// <summary>The block-specific USS class names the shared legend builder wears (the two audit tabs use different BEM blocks).</summary>
        internal readonly struct LegendClasses
        {
            public readonly string Item;
            public readonly string Dot;
            public readonly string DotInfo;
            public readonly string Text;

            public LegendClasses(string item, string dot, string dotInfo, string text)
            {
                Item = item;
                Dot = dot;
                DotInfo = dotInfo;
                Text = text;
            }
        }
    }
}
