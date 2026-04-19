using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Centralised registry of USS class names and <see cref="CustomStyleProperty{T}"/> definitions
    /// used by Aspid UI elements.
    /// </summary>
    public static class StyleClasses
    {
        /// <summary>
        /// Resource path of the default dark USS stylesheet.
        /// </summary>
        public const string DefaultStyleSheet = "Styles/Aspid-FastTools-Default-Dark";

        public const string Adapter = "aspid-fasttools-draw-adapter";
        public const string AdapterMargin = "aspid-fasttools-draw-adapter-margin";
        
        /// <summary>
        /// USS class applied to elements that use the Aspid background style.
        /// </summary>
        public const string Background = "aspid-fasttools-background";

        /// <summary>
        /// USS class names and custom property for the theme style.
        /// </summary>
        public static class Theme
        {
            /// <summary>
            /// Custom USS property used to propagate the theme to child elements.
            /// </summary>
            public static readonly CustomStyleProperty<string> Property = new("--aspid-fasttools-theme");

            /// <summary>x
            /// USS class for the <see cref="ThemeStyle.Darkness"/> variant.
            /// </summary>
            public const string Darkness = "aspid-fasttools-darkness";

            /// <summary>
            /// USS class for the <see cref="ThemeStyle.Dark"/> variant.
            /// </summary>
            public const string Dark = "aspid-fasttools-dark";

            /// <summary>
            /// USS class for the <see cref="ThemeStyle.Light"/> variant.
            /// </summary>
            public const string Light = "aspid-fasttools-light";

            /// <summary>
            /// USS class for the <see cref="ThemeStyle.Lightness"/> variant.
            /// </summary>
            public const string Lightness = "aspid-fasttools-lightness";
        }

        /// <summary>
        /// USS class names and custom property for the status style.
        /// </summary>
        public static class Status
        {
            /// <summary>
            /// Custom USS property used to propagate the status to child elements.
            /// </summary>
            public static readonly CustomStyleProperty<string> Property = new("--aspid-fasttools-status");

            /// <summary>
            /// USS class for the <see cref="StatusStyle.Success"/> status.
            /// </summary>
            public const string Success = "aspid-fasttools-status-success";

            /// <summary>
            /// USS class for the <see cref="StatusStyle.Warning"/> status.
            /// </summary>
            public const string Warning = "aspid-fasttools-status-warning";

            /// <summary>
            /// USS class for the <see cref="StatusStyle.Error"/> status.
            /// </summary>
            public const string Error = "aspid-fasttools-status-error";

            /// <summary>
            /// USS class for the <see cref="StatusStyle.Info"/> status.
            /// </summary>
            public const string Info = "aspid-fasttools-status-info";
        }

        /// <summary>
        /// Custom USS properties for <see cref="AspidLabel"/> styling.
        /// </summary>
        public static class Label
        {
            /// <summary>
            /// Custom USS property for overriding the label font size via USS.
            /// </summary>
            public static readonly CustomStyleProperty<string> SizeProperty = new("--aspid-fasttools-label-size");

            /// <summary>
            /// Custom USS property for overriding the label font style via USS.
            /// </summary>
            public static readonly CustomStyleProperty<string> FontStyleProperty = new("--aspid-fasttools-label-font-style");
        }

        /// <summary>
        /// USS class names and custom property for <see cref="AspidDividingLine"/> styling.
        /// </summary>
        public static class DividingLine
        {
            /// <summary>
            /// Custom USS property for overriding the line size via USS.
            /// </summary>
            public static readonly CustomStyleProperty<string> SizeProperty = new("--aspid-fasttools-line-size");

            /// <summary>
            /// USS class for the <see cref="DividingLineSize.Thin"/> variant.
            /// </summary>
            public const string Thin = "aspid-fasttools-thin-line";

            /// <summary>
            /// USS class for the <see cref="DividingLineSize.Medium"/> variant.
            /// </summary>
            public const string Medium = "aspid-fasttools-medium-line";

            /// <summary>
            /// USS class for the <see cref="DividingLineSize.Bold"/> variant.
            /// </summary>
            public const string Bold = "aspid-fasttools-bold-line";

            /// <summary>
            /// USS class for the <see cref="DividingLineDirection.Horizontal"/> orientation.
            /// </summary>
            public const string Horizontal = "aspid-fasttools-horizontal-line";

            /// <summary>
            /// USS class for the <see cref="DividingLineDirection.Vertical"/> orientation.
            /// </summary>
            public const string Vertical = "aspid-fasttools-vertical-line";
        }

        /// <summary>
        /// USS class names and custom properties for <see cref="AspidInspectorHeader"/> styling.
        /// </summary>
        public static class InspectorHeader
        {
            /// <summary>
            /// Custom USS property for the hover gradient accent color.
            /// </summary>
            public static readonly CustomStyleProperty<Color> GradientColorProperty = new("--aspid-gradient-color");
        }
    }
}
