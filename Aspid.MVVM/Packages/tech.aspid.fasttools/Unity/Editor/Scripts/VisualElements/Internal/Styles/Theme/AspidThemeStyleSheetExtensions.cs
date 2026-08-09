using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Extensions that apply the Aspid editor theme (default palette plus the optional user override)
    /// to a <see cref="VisualElement"/>.
    /// </summary>
    internal static class AspidThemeStyleSheetExtensions
    {
        /// <summary>
        /// Adds <see cref="AspidStyles.DefaultStyleSheet"/> to the element and, when the user has
        /// configured one, layers <see cref="AspidThemeSettings.OverrideStyleSheet"/> on top of it.
        /// The override is added to the same element as the base palette so its <c>:root</c> tokens
        /// take precedence. The element subscribes to <see cref="AspidThemeSettings.Changed"/> while
        /// attached to a panel (re-applying the current override on attach) and unsubscribes when it
        /// leaves the panel, so live updates survive detach/reattach and never leak when never attached.
        /// </summary>
        /// <param name="element">The element that receives the theme style sheets.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddAspidThemeStyleSheets<T>(this T element)
            where T : VisualElement
        {
            element.AddStyleSheetsFromResource(AspidStyles.DefaultStyleSheet);

            var applied = AspidThemeSettings.OverrideStyleSheet;
            if (applied != null) element.AddStyleSheets(applied);

            element.RegisterCallback<AttachToPanelEvent>(_ =>
            {
                OnThemeChanged();
                AspidThemeSettings.Changed += OnThemeChanged;
            });
            element.RegisterCallback<DetachFromPanelEvent>(_ => AspidThemeSettings.Changed -= OnThemeChanged);

            return element;

            void OnThemeChanged()
            {
                if (applied != null) element.RemoveStyleSheets(applied);

                applied = AspidThemeSettings.OverrideStyleSheet;
                if (applied != null) element.AddStyleSheets(applied);
            }
        }
    }
}
