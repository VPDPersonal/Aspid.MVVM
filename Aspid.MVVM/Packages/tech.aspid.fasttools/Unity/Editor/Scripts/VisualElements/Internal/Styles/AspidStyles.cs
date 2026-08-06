// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Single source of truth for the shared Aspid editor USS class names and the default
    /// stylesheet path. Internal components reference these constants instead of hard-coding
    /// string literals so class/path names stay consistent across the package.
    /// </summary>
    internal static class AspidStyles
    {
        /// <summary>
        /// Resource path of the default dark USS stylesheet.
        /// </summary>
        public const string DefaultStyleSheet = "UI/Aspid-FastTools-Default-Dark";
        
        /// <summary>
        /// USS class applied to elements that use the Aspid background style.
        /// </summary>
        public const string BackgroundStyle = "aspid-fasttools-background";

        /// <summary>
        /// USS modifier applied alongside <see cref="BackgroundStyle"/> to give the element rounded corners and padding.
        /// </summary>
        public const string BackgroundRoundedState = BackgroundStyle + "--rounded";

        /// <summary>
        /// USS class applied to inspector container elements styled with Aspid editor theme.
        /// </summary>
        public const string InspectorStyleClass = "aspid-fasttools-inspector-container";
    }
}
