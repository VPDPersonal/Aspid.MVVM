// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal readonly struct NextIdWarning
    {
        public readonly bool Show;
        public readonly string Tooltip;

        public NextIdWarning(bool show, string tooltip)
        {
            Show = show;
            Tooltip = tooltip;
        }

        public static NextIdWarning Hidden(string tooltip = null) =>
            new(show: false, tooltip: tooltip ?? string.Empty);

        public static NextIdWarning Visible(string tooltip) =>
            new(show: true, tooltip: tooltip);
    }
}
