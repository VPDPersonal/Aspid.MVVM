using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// A <see cref="VisualElement"/> container with an Aspid background and theme support.
    /// The theme can be driven by USS via the <c>--aspid-fasttools-theme</c> custom property
    /// or set explicitly in code.
    /// </summary>
    public class AspidBox : VisualElement
    {
        private StyleOverride<ThemeStyle> _theme;

        /// <summary>
        /// Gets or sets the visual theme of this box.
        /// </summary>
        public ThemeStyle Theme
        {
            get => _theme;
            set => _theme.Set(value);
        }

        /// <summary>
        /// Creates an <see cref="AspidBox"/> with the specified initial theme.
        /// </summary>
        /// <param name="theme">The initial theme. Defaults to <see cref="ThemeStyle.Light"/>.</param>
        public AspidBox(ThemeStyle theme = ThemeStyle.Light)
        {
            this.AddClass(StyleClasses.Background);
            
            _theme = new StyleOverride<ThemeStyle>(theme, (oldValue, newValue) =>
            {
                this.RemoveClass(oldValue.ToUss())
                    .AddClass(newValue.ToUss());
            });
            
            RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
        }
        
        private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
        {
            if (evt.customStyle.TryGetByEnum<ThemeStyle>(StyleClasses.Theme.Property, out var themeValue))
                _theme.SetDefault(themeValue);
        }
    }
}
