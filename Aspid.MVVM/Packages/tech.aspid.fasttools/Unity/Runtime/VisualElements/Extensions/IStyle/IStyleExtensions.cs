using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static partial class IStyleExtensions
    {
        #region Flex
        /// <summary>
        /// Sets <see cref="IStyle.flexBasis"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Initial main size of a flex item, on the main flex axis. The final layout might be smaller or larger, according to the flex shrinking and growing determined by the other flex properties.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The flex basis to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetFlexBasis<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            style.flexBasis = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.flexGrow"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies how the item will grow relative to the rest of the flexible items inside the same container.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The flex grow factor to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetFlexGrow<T>(
            this T style,
            StyleFloat value)
            where T : IStyle
        {
            style.flexGrow = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.flexShrink"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies how the item will shrink relative to the rest of the flexible items inside the same container.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The flex shrink factor to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetFlexShrink<T>(
            this T style,
            StyleFloat value)
            where T : IStyle
        {
            style.flexShrink = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.flexWrap"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Placement of children over multiple lines if not enough space is available in this container.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The flex wrap mode to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetFlexWrap<T>(
            this T style,
            StyleEnum<Wrap> value)
            where T : IStyle
        {
            style.flexWrap = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.flexWrap"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Placement of children over multiple lines if not enough space is available in this container.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The flex wrap mode to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetFlexWrap<T>(
            this T style,
            Wrap value)
            where T : IStyle
        {
            style.flexWrap = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.flexDirection"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Direction of the main axis to layout children in a container.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The flex direction to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetFlexDirection<T>(
            this T style,
            StyleEnum<FlexDirection> value)
            where T : IStyle
        {
            style.flexDirection = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.flexDirection"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Direction of the main axis to layout children in a container.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The flex direction to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetFlexDirection<T>(
            this T style,
            FlexDirection value)
            where T : IStyle
        {
            style.flexDirection = value;
            return style;
        }
        #endregion

        #region Size
        /// <summary>
        /// Sets <see cref="IStyle.width"/>, <see cref="IStyle.height"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>width</c> –– Fixed width of an element for the layout.</para>
        /// <para><c>height</c> –– Fixed height of an element for the layout.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The size to apply to both width and height.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetSize<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetSize(
                width: value,
                height: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.width"/>, <see cref="IStyle.height"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>width</c> –– Fixed width of an element for the layout.</para>
        /// <para><c>height</c> –– Fixed height of an element for the layout.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="width">The width to set, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="height">The height to set, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetSize<T>(
            this T style,
            StyleLength? width = null,
            StyleLength? height = null)
            where T : IStyle
        {
            if (width.HasValue) style.width = width.Value;
            if (height.HasValue) style.height = height.Value;

            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.minWidth"/>, <see cref="IStyle.minHeight"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>minWidth</c> –– Minimum width for an element, when it is flexible or measures its own size.</para>
        /// <para><c>minHeight</c> –– Minimum height for an element, when it is flexible or measures its own size.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The minimum size to apply to both width and height.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMinSize<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetMinSize(
                minWidth: value,
                minHeight: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.minWidth"/>, <see cref="IStyle.minHeight"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>minWidth</c> –– Minimum width for an element, when it is flexible or measures its own size.</para>
        /// <para><c>minHeight</c> –– Minimum height for an element, when it is flexible or measures its own size.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="minWidth">The minimum width to set, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="minHeight">The minimum height to set, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMinSize<T>(
            this T style,
            StyleLength? minWidth = null,
            StyleLength? minHeight = null)
            where T : IStyle
        {
            if (minWidth.HasValue) style.minWidth = minWidth.Value;
            if (minHeight.HasValue) style.minHeight = minHeight.Value;

            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.maxWidth"/>, <see cref="IStyle.maxHeight"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>maxWidth</c> –– Maximum width for an element, when it is flexible or measures its own size.</para>
        /// <para><c>maxHeight</c> –– Maximum height for an element, when it is flexible or measures its own size.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The maximum size to apply to both width and height.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMaxSize<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetMaxSize(
                maxWidth: value,
                maxHeight: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.maxWidth"/>, <see cref="IStyle.maxHeight"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>maxWidth</c> –– Maximum width for an element, when it is flexible or measures its own size.</para>
        /// <para><c>maxHeight</c> –– Maximum height for an element, when it is flexible or measures its own size.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="maxWidth">The maximum width to set, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="maxHeight">The maximum height to set, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMaxSize<T>(
            this T style,
            StyleLength? maxWidth = null,
            StyleLength? maxHeight = null)
            where T : IStyle
        {
            if (maxWidth.HasValue) style.maxWidth = maxWidth.Value;
            if (maxHeight.HasValue) style.maxHeight = maxHeight.Value;

            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.width"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>width</c> –– Fixed width of an element for the layout.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The width to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetWidth<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetSize(width: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.minWidth"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>minWidth</c> –– Minimum width for an element, when it is flexible or measures its own size.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The minimum width to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMinWidth<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetMinSize(minWidth: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.maxWidth"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>maxWidth</c> –– Maximum width for an element, when it is flexible or measures its own size.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The maximum width to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMaxWidth<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetMaxSize(maxWidth: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.height"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>height</c> –– Fixed height of an element for the layout.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The height to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetHeight<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetSize(height: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.minHeight"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>minHeight</c> –– Minimum height for an element, when it is flexible or measures its own size.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The minimum height to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMinHeight<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetMinSize(minHeight: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.maxHeight"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>maxHeight</c> –– Maximum height for an element, when it is flexible or measures its own size.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The maximum height to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMaxHeight<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetMaxSize(maxHeight: value);
        }
        #endregion

        #region Font
        /// <summary>
        /// Sets <see cref="IStyle.unityFont"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Font to draw the element's text, defined as a Font object.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The font to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityFont<T>(
            this T style,
            StyleFont value)
            where T : IStyle
        {
            style.unityFont = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.fontSize"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Font size to draw the element's text, specified in point size.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The font size to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetFontSize<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            style.fontSize = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityFontDefinition"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Font to draw the element's text, defined as a FontDefinition structure. It takes precedence over -unity-font.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The font definition to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityFontDefinition<T>(
            this T style,
            StyleFontDefinition value)
            where T : IStyle
        {
            style.unityFontDefinition = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityFontStyleAndWeight"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Font style and weight (normal, bold, italic) to draw the element's text.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The font style and weight to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityFontStyleAndWeight<T>(
            this T style,
            StyleEnum<FontStyle> value)
            where T : IStyle
        {
            style.unityFontStyleAndWeight = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityFontStyleAndWeight"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Font style and weight (normal, bold, italic) to draw the element's text.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The font style and weight to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityFontStyleAndWeight<T>(
            this T style,
            FontStyle value)
            where T : IStyle
        {
            style.unityFontStyleAndWeight = value;
            return style;
        }
        #endregion

        #region Text
        /// <summary>
        /// Sets <see cref="IStyle.wordSpacing"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Increases or decreases the space between words.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The word spacing to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetWordSpacing<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            style.wordSpacing = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.letterSpacing"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Increases or decreases the space between characters.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The letter spacing to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetLetterSpacing<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            style.letterSpacing = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityTextAlign"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Horizontal and vertical text alignment in the element's box.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The text alignment to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityTextAlign<T>(
            this T style,
            StyleEnum<TextAnchor> value)
            where T : IStyle
        {
            style.unityTextAlign = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityTextAlign"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Horizontal and vertical text alignment in the element's box.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The text alignment to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityTextAlign<T>(
            this T style,
            TextAnchor value)
            where T : IStyle
        {
            style.unityTextAlign = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.textShadow"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Drop shadow of the text.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The text shadow to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetTextShadow<T>(
            this T style,
            StyleTextShadow value)
            where T : IStyle
        {
            style.textShadow = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityTextOutlineColor"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Outline color of the text.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The text outline color to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityTextOutlineColor<T>(
            this T style,
            StyleColor value)
            where T : IStyle
        {
            style.unityTextOutlineColor = value;
            return style;
        }

        /// <summary>
        /// Sets the text outline color by parsing an HTML color string via <see cref="ColorUtility.TryParseHtmlString"/>.
        /// </summary>
        /// <typeparam name="T">The style type.</typeparam>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The HTML color string (e.g. "#FF0000", "red").</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityTextOutlineColor<T>(
            this T style,
            string value)
            where T : IStyle
        {
            if (!ColorUtility.TryParseHtmlString(value, out var color))
            {
                Debug.LogWarning($"Failed to parse color string: '{value}'");
                return style;
            }
            return style.SetUnityTextOutlineColor(color);
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityTextOutlineWidth"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Outline width of the text.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The text outline width to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityTextOutlineWidth<T>(
            this T style,
            StyleFloat value)
            where T : IStyle
        {
            style.unityTextOutlineWidth = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityParagraphSpacing"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Increases or decreases the space between paragraphs.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The paragraph spacing to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityParagraphSpacing<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            style.unityParagraphSpacing = value;
            return style;
        }

#if UNITY_6000_2_OR_NEWER
        /// <summary>
        /// Sets <see cref="IStyle.unityTextAutoSize"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Overrides any explicit font-size to scale text within the defined minimum and maximum bounds, recalculating as needed to fit its container.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The text auto size settings to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityTextAutoSize<T>(
            this T style,
            StyleTextAutoSize value)
            where T : IStyle
        {
            style.unityTextAutoSize = value;
            return style;
        }
#endif

#if UNITY_6000_0_OR_NEWER
        /// <summary>
        /// Sets <see cref="IStyle.unityTextGenerator"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Switches between Unity's standard and advanced text generator.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The text generator type to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityTextGenerator<T>(
            this T style,
            StyleEnum<TextGeneratorType> value)
            where T : IStyle
        {
            style.unityTextGenerator = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityTextGenerator"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Switches between Unity's standard and advanced text generator.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The text generator type to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityTextGenerator<T>(
            this T style,
            TextGeneratorType value)
            where T : IStyle
        {
            style.unityTextGenerator = value;
            return style;
        }
        
        /// <summary>
        /// Sets <see cref="IStyle.unityEditorTextRenderingMode"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// TextElement editor rendering mode.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The editor text rendering mode to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityEditorTextRenderingMode<T>(
            this T style,
            StyleEnum<EditorTextRenderingMode> value)
            where T : IStyle
        {
            style.unityEditorTextRenderingMode = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityEditorTextRenderingMode"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// TextElement editor rendering mode.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The editor text rendering mode to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityEditorTextRenderingMode<T>(
            this T style,
            EditorTextRenderingMode value)
            where T : IStyle
        {
            style.unityEditorTextRenderingMode = value;
            return style;
        }
#endif

        /// <summary>
        /// Sets <see cref="IStyle.textOverflow"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// The element's text overflow mode.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The text overflow mode to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetTextOverflow<T>(
            this T style,
            StyleEnum<TextOverflow> value)
            where T : IStyle
        {
            style.textOverflow = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.textOverflow"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// The element's text overflow mode.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The text overflow mode to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetTextOverflow<T>(
            this T style,
            TextOverflow value)
            where T : IStyle
        {
            style.textOverflow = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityTextOverflowPosition"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// The element's text overflow position.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The text overflow position to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityTextOverflowPosition<T>(
            this T style,
            StyleEnum<TextOverflowPosition> value)
            where T : IStyle
        {
            style.unityTextOverflowPosition = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityTextOverflowPosition"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// The element's text overflow position.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The text overflow position to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityTextOverflowPosition<T>(
            this T style,
            TextOverflowPosition value)
            where T : IStyle
        {
            style.unityTextOverflowPosition = value;
            return style;
        }
        #endregion

        #region Color
        /// <summary>
        /// Sets <see cref="IStyle.color"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Color to use when drawing the text of an element.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The text color to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetColor<T>(
            this T style,
            StyleColor value)
            where T : IStyle
        {
            style.color = value;
            return style;
        }

        /// <summary>
        /// Parses an HTML color string and sets <see cref="IStyle.color"/>, returning the style for chaining.
        /// </summary>
        /// <remarks>
        /// Color to use when drawing the text of an element.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The HTML color string to parse (e.g. "#RRGGBB" or a named color).</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetColor<T>(
            this T style,
            string value)
            where T : IStyle
        {
            if (!ColorUtility.TryParseHtmlString(value, out var color))
            {
                Debug.LogWarning($"Failed to parse color string: '{value}'");
                return style;
            }

            style.SetColor(color);
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.opacity"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies the transparency of an element and of its children.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The opacity to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetOpacity<T>(
            this T style,
            StyleFloat value)
            where T : IStyle
        {
            style.opacity = value;
            return style;
        }
        #endregion

        #region Align
        /// <summary>
        /// Sets <see cref="IStyle.alignSelf"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Similar to align-items, but only for this specific element.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The alignment to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetAlignSelf<T>(
            this T style,
            StyleEnum<Align> value)
            where T : IStyle
        {
            style.alignSelf = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.alignSelf"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Similar to align-items, but only for this specific element.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The alignment to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetAlignSelf<T>(
            this T style,
            Align value)
            where T : IStyle
        {
            style.alignSelf = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.alignItems"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Alignment of children on the cross axis of this container.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The children alignment to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetAlignItems<T>(
            this T style,
            StyleEnum<Align> value)
            where T : IStyle
        {
            style.alignItems = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.alignItems"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Alignment of children on the cross axis of this container.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The children alignment to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetAlignItems<T>(
            this T style,
            Align value)
            where T : IStyle
        {
            style.alignItems = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.alignContent"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Alignment of the whole area of children on the cross axis if they span over multiple lines in this container.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The content alignment to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetAlignContent<T>(
            this T style,
            StyleEnum<Align> value)
            where T : IStyle
        {
            style.alignContent = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.alignContent"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Alignment of the whole area of children on the cross axis if they span over multiple lines in this container.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The content alignment to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetAlignContent<T>(
            this T style,
            Align value)
            where T : IStyle
        {
            style.alignContent = value;
            return style;
        }
        #endregion

        #region Aspect
#if UNITY_6000_3_OR_NEWER
        /// <summary>
        /// Sets <see cref="IStyle.aspectRatio"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Sets a preferred aspect ratio for the box, which will be used in the calculation of auto sizes and some other layout functions.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The aspect ratio to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetAspectRatio<T>(
            this T style,
            StyleRatio value)
            where T : IStyle
        {
            style.aspectRatio = value;
            return style;
        }
#endif
        #endregion

        #region Filter
#if UNITY_6000_3_OR_NEWER
        /// <summary>
        /// Sets <see cref="IStyle.filter"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Filter effects to apply to the element.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The filter effects to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetFilter<T>(
            this T style,
            StyleList<FilterFunction> value)
            where T : IStyle
        {
            style.filter = value;
            return style;
        }
#endif
        #endregion

        #region Border
        /// <summary>
        /// Sets <see cref="IStyle.borderTopColor"/>, <see cref="IStyle.borderRightColor"/>,
        /// <see cref="IStyle.borderBottomColor"/>, <see cref="IStyle.borderLeftColor"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderTopColor</c> –– Color of the element's top border.</para>
        /// <para><c>borderRightColor</c> –– Color of the element's right border.</para>
        /// <para><c>borderBottomColor</c> –– Color of the element's bottom border.</para>
        /// <para><c>borderLeftColor</c> –– Color of the element's left border.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The border color to apply to all sides.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderColor<T>(
            this T style,
            StyleColor value)
            where T : IStyle
        {
            return style.SetBorderColor(
                top: value,
                right: value,
                bottom: value,
                left: value);
        }

        /// <summary>
        /// Sets the border color on all sides by parsing an HTML color string via <see cref="ColorUtility.TryParseHtmlString"/>.
        /// </summary>
        /// <typeparam name="T">The style type.</typeparam>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The HTML color string (e.g. "#FF0000", "red").</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderColor<T>(
            this T style,
            string value)
            where T : IStyle
        {
            if (!ColorUtility.TryParseHtmlString(value, out var color))
            {
                Debug.LogWarning($"Failed to parse color string: '{value}'");
                return style;
            }

            return style.SetBorderColor(color);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopColor"/>, <see cref="IStyle.borderRightColor"/>,
        /// <see cref="IStyle.borderBottomColor"/>, <see cref="IStyle.borderLeftColor"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderTopColor</c> –– Color of the element's top border.</para>
        /// <para><c>borderRightColor</c> –– Color of the element's right border.</para>
        /// <para><c>borderBottomColor</c> –– Color of the element's bottom border.</para>
        /// <para><c>borderLeftColor</c> –– Color of the element's left border.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="top">The top border color, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="right">The right border color, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottom">The bottom border color, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="left">The left border color, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderColor<T>(
            this T style,
            StyleColor? top = null,
            StyleColor? right = null,
            StyleColor? bottom = null,
            StyleColor? left = null)
            where T : IStyle
        {
            if (top.HasValue) style.borderTopColor = top.Value;
            if (right.HasValue) style.borderRightColor = right.Value;
            if (bottom.HasValue) style.borderBottomColor = bottom.Value;
            if (left.HasValue) style.borderLeftColor = left.Value;

            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderRightColor"/>, <see cref="IStyle.borderLeftColor"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderRightColor</c> –– Color of the element's right border.</para>
        /// <para><c>borderLeftColor</c> –– Color of the element's left border.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The border color to apply to the left and right sides.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderColorX<T>(
            this T style,
            StyleColor value)
            where T : IStyle
        {
            return style.SetBorderColor(
                right: value,
                left: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopColor"/> and <see cref="IStyle.borderBottomColor"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderTopColor</c> –– Color of the element's top border.</para>
        /// <para><c>borderBottomColor</c> –– Color of the element's bottom border.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The border color to apply to the top and bottom sides.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderColorY<T>(
            this T style,
            StyleColor value)
            where T : IStyle
        {
            return style.SetBorderColor(
                top: value,
                bottom: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopColor"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderTopColor</c> –– Color of the element's top border.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The top border color to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderColorTop<T>(
            this T style,
            StyleColor value)
            where T : IStyle
        {
            return style.SetBorderColor(top: value);
        }

        /// <summary>
        /// Sets the top border color by parsing an HTML color string via <see cref="ColorUtility.TryParseHtmlString"/>.
        /// </summary>
        /// <typeparam name="T">The style type.</typeparam>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The HTML color string (e.g. "#FF0000", "red").</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderColorTop<T>(
            this T style,
            string value)
            where T : IStyle
        {
            if (!ColorUtility.TryParseHtmlString(value, out var color))
            {
                Debug.LogWarning($"Failed to parse color string: '{value}'");
                return style;
            }
            
            return style.SetBorderColorTop(color);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderRightColor"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderRightColor</c> –– Color of the element's right border.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The right border color to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderColorRight<T>(
            this T style,
            StyleColor value)
            where T : IStyle
        {
            return style.SetBorderColor(right: value);
        }

        /// <summary>
        /// Sets the right border color by parsing an HTML color string via <see cref="ColorUtility.TryParseHtmlString"/>.
        /// </summary>
        /// <typeparam name="T">The style type.</typeparam>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The HTML color string (e.g. "#FF0000", "red").</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderColorRight<T>(
            this T style,
            string value)
            where T : IStyle
        {
            if (!ColorUtility.TryParseHtmlString(value, out var color))
            {
                Debug.LogWarning($"Failed to parse color string: '{value}'");
                return style;
            }
            
            return style.SetBorderColorRight(color);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderBottomColor"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderBottomColor</c> –– Color of the element's bottom border.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The bottom border color to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderColorBottom<T>(
            this T style,
            StyleColor value)
            where T : IStyle
        {
            return style.SetBorderColor(bottom: value);
        }

        /// <summary>
        /// Sets the bottom border color by parsing an HTML color string via <see cref="ColorUtility.TryParseHtmlString"/>.
        /// </summary>
        /// <typeparam name="T">The style type.</typeparam>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The HTML color string (e.g. "#FF0000", "red").</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderColorBottom<T>(
            this T style,
            string value)
            where T : IStyle
        {
            if (!ColorUtility.TryParseHtmlString(value, out var color))
            {
                Debug.LogWarning($"Failed to parse color string: '{value}'");
                return style;
            }
            
            return style.SetBorderColorBottom(color);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderLeftColor"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderLeftColor</c> –– Color of the element's left border.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The left border color to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderColorLeft<T>(
            this T style,
            StyleColor value)
            where T : IStyle
        {
            return style.SetBorderColor(left: value);
        }

        /// <summary>
        /// Sets the left border color by parsing an HTML color string via <see cref="ColorUtility.TryParseHtmlString"/>.
        /// </summary>
        /// <typeparam name="T">The style type.</typeparam>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The HTML color string (e.g. "#FF0000", "red").</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderColorLeft<T>(
            this T style,
            string value)
            where T : IStyle
        {
            if (!ColorUtility.TryParseHtmlString(value, out var color))
            {
                Debug.LogWarning($"Failed to parse color string: '{value}'");
                return style;
            }
            
            return style.SetBorderColorLeft(color);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopLeftRadius"/>, <see cref="IStyle.borderTopRightRadius"/>,
        /// <see cref="IStyle.borderBottomRightRadius"/>, <see cref="IStyle.borderBottomLeftRadius"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderTopLeftRadius</c> –– The radius of the top-left corner when a rounded rectangle is drawn in the element's box.</para>
        /// <para><c>borderTopRightRadius</c> –– The radius of the top-right corner when a rounded rectangle is drawn in the element's box.</para>
        /// <para><c>borderBottomRightRadius</c> –– The radius of the bottom-right corner when a rounded rectangle is drawn in the element's box.</para>
        /// <para><c>borderBottomLeftRadius</c> –– The radius of the bottom-left corner when a rounded rectangle is drawn in the element's box.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The border radius to apply to all corners.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderRadius<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetBorderRadius(
                topLeft: value,
                topRight: value,
                bottomRight: value,
                bottomLeft: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopLeftRadius"/>, <see cref="IStyle.borderTopRightRadius"/>,
        /// <see cref="IStyle.borderBottomRightRadius"/>, <see cref="IStyle.borderBottomLeftRadius"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderTopLeftRadius</c> –– The radius of the top-left corner when a rounded rectangle is drawn in the element's box.</para>
        /// <para><c>borderTopRightRadius</c> –– The radius of the top-right corner when a rounded rectangle is drawn in the element's box.</para>
        /// <para><c>borderBottomRightRadius</c> –– The radius of the bottom-right corner when a rounded rectangle is drawn in the element's box.</para>
        /// <para><c>borderBottomLeftRadius</c> –– The radius of the bottom-left corner when a rounded rectangle is drawn in the element's box.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="topLeft">The top-left radius, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="topRight">The top-right radius, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottomRight">The bottom-right radius, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottomLeft">The bottom-left radius, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderRadius<T>(
            this T style,
            StyleLength? topLeft = null,
            StyleLength? topRight = null,
            StyleLength? bottomRight = null,
            StyleLength? bottomLeft = null)
            where T : IStyle
        {
            if (topLeft.HasValue) style.borderTopLeftRadius = topLeft.Value;
            if (topRight.HasValue) style.borderTopRightRadius = topRight.Value;
            if (bottomRight.HasValue) style.borderBottomRightRadius = bottomRight.Value;
            if (bottomLeft.HasValue) style.borderBottomLeftRadius = bottomLeft.Value;

            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopLeftRadius"/>, <see cref="IStyle.borderTopRightRadius"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderTopLeftRadius</c> –– The radius of the top-left corner when a rounded rectangle is drawn in the element's box.</para>
        /// <para><c>borderTopRightRadius</c> –– The radius of the top-right corner when a rounded rectangle is drawn in the element's box.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The radius to apply to both top corners.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderRadiusTop<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetBorderRadius(
                topLeft: value,
                topRight: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderBottomRightRadius"/>, <see cref="IStyle.borderBottomLeftRadius"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderBottomRightRadius</c> –– The radius of the bottom-right corner when a rounded rectangle is drawn in the element's box.</para>
        /// <para><c>borderBottomLeftRadius</c> –– The radius of the bottom-left corner when a rounded rectangle is drawn in the element's box.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The radius to apply to both bottom corners.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderRadiusBottom<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetBorderRadius(
                bottomRight: value,
                bottomLeft: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopLeftRadius"/>, <see cref="IStyle.borderBottomLeftRadius"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderTopLeftRadius</c> –– The radius of the top-left corner when a rounded rectangle is drawn in the element's box.</para>
        /// <para><c>borderBottomLeftRadius</c> –– The radius of the bottom-left corner when a rounded rectangle is drawn in the element's box.</para>
        /// </remarks>
        /// <typeparam name="T">The style type.</typeparam>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The radius to apply to both left corners.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderRadiusLeft<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetBorderRadius(
                topLeft: value,
                bottomLeft: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopRightRadius"/>, <see cref="IStyle.borderBottomRightRadius"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderTopRightRadius</c> –– The radius of the top-right corner when a rounded rectangle is drawn in the element's box.</para>
        /// <para><c>borderBottomRightRadius</c> –– The radius of the bottom-right corner when a rounded rectangle is drawn in the element's box.</para>
        /// </remarks>
        /// <typeparam name="T">The style type.</typeparam>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The radius to apply to both right corners.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderRadiusRight<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetBorderRadius(
                topRight: value,
                bottomRight: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopLeftRadius"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderTopLeftRadius</c> –– The radius of the top-left corner when a rounded rectangle is drawn in the element's box.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The top-left corner radius to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderRadiusTopLeft<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetBorderRadius(topLeft: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopRightRadius"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderTopRightRadius</c> –– The radius of the top-right corner when a rounded rectangle is drawn in the element's box.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The top-right corner radius to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderRadiusTopRight<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetBorderRadius(topRight: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderBottomRightRadius"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderBottomRightRadius</c> –– The radius of the bottom-right corner when a rounded rectangle is drawn in the element's box.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The bottom-right corner radius to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderRadiusBottomRight<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetBorderRadius(bottomRight: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderBottomLeftRadius"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderBottomLeftRadius</c> –– The radius of the bottom-left corner when a rounded rectangle is drawn in the element's box.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The bottom-left corner radius to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderRadiusBottomLeft<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetBorderRadius(bottomLeft: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopWidth"/>, <see cref="IStyle.borderRightWidth"/>,
        /// <see cref="IStyle.borderBottomWidth"/>, <see cref="IStyle.borderLeftWidth"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderTopWidth</c> –– Space reserved for the top edge of the border during the layout phase.</para>
        /// <para><c>borderRightWidth</c> –– Space reserved for the right edge of the border during the layout phase.</para>
        /// <para><c>borderBottomWidth</c> –– Space reserved for the bottom edge of the border during the layout phase.</para>
        /// <para><c>borderLeftWidth</c> –– Space reserved for the left edge of the border during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The border width to apply to all sides.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderWidth<T>(
            this T style,
            StyleFloat value)
            where T : IStyle
        {
            return style.SetBorderWidth(
                top: value,
                right: value,
                bottom: value,
                left: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopWidth"/>, <see cref="IStyle.borderRightWidth"/>,
        /// <see cref="IStyle.borderBottomWidth"/>, <see cref="IStyle.borderLeftWidth"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderTopWidth</c> –– Space reserved for the top edge of the border during the layout phase.</para>
        /// <para><c>borderRightWidth</c> –– Space reserved for the right edge of the border during the layout phase.</para>
        /// <para><c>borderBottomWidth</c> –– Space reserved for the bottom edge of the border during the layout phase.</para>
        /// <para><c>borderLeftWidth</c> –– Space reserved for the left edge of the border during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="top">The top border width, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="right">The right border width, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottom">The bottom border width, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="left">The left border width, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderWidth<T>(
            this T style,
            StyleFloat? top = null,
            StyleFloat? right = null,
            StyleFloat? bottom = null,
            StyleFloat? left = null)
            where T : IStyle
        {
            if (top.HasValue) style.borderTopWidth = top.Value;
            if (right.HasValue) style.borderRightWidth = right.Value;
            if (bottom.HasValue) style.borderBottomWidth = bottom.Value;
            if (left.HasValue) style.borderLeftWidth = left.Value;

            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderLeftWidth"/> and <see cref="IStyle.borderRightWidth"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderRightWidth</c> –– Space reserved for the right edge of the border during the layout phase.</para>
        /// <para><c>borderLeftWidth</c> –– Space reserved for the left edge of the border during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The border width to apply to the left and right sides.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderWidthX<T>(
            this T style,
            StyleFloat value)
            where T : IStyle
        {
            return style.SetBorderWidth(right: value, left: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopWidth"/> and <see cref="IStyle.borderBottomWidth"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderTopWidth</c> –– Space reserved for the top edge of the border during the layout phase.</para>
        /// <para><c>borderBottomWidth</c> –– Space reserved for the bottom edge of the border during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The border width to apply to the top and bottom sides.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderWidthY<T>(
            this T style,
            StyleFloat value)
            where T : IStyle
        {
            return style.SetBorderWidth(top: value, bottom: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopWidth"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderTopWidth</c> –– Space reserved for the top edge of the border during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The top border width to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderWidthTop<T>(
            this T style,
            StyleFloat value)
            where T : IStyle
        {
            return style.SetBorderWidth(top: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderRightWidth"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderRightWidth</c> –– Space reserved for the right edge of the border during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The right border width to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderWidthRight<T>(
            this T style,
            StyleFloat value)
            where T : IStyle
        {
            return style.SetBorderWidth(right: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderBottomWidth"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderBottomWidth</c> –– Space reserved for the bottom edge of the border during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The bottom border width to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderWidthBottom<T>(
            this T style,
            StyleFloat value)
            where T : IStyle
        {
            return style.SetBorderWidth(bottom: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderLeftWidth"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>borderLeftWidth</c> –– Space reserved for the left edge of the border during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The left border width to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBorderWidthLeft<T>(
            this T style,
            StyleFloat value)
            where T : IStyle
        {
            return style.SetBorderWidth(left: value);
        }
        #endregion

        #region Cursor
        /// <summary>
        /// Sets <see cref="IStyle.cursor"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Mouse cursor to display when the mouse pointer is over an element.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The cursor style to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetCursor<T>(
            this T style,
            StyleCursor value)
            where T : IStyle
        {
            style.cursor = value;
            return style;
        }
        #endregion

        #region Margin
        /// <summary>
        /// Sets <see cref="IStyle.marginTop"/>, <see cref="IStyle.marginRight"/>,
        /// <see cref="IStyle.marginBottom"/>, <see cref="IStyle.marginLeft"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>marginTop</c> –– Space reserved for the top edge of the margin during the layout phase.</para>
        /// <para><c>marginRight</c> –– Space reserved for the right edge of the margin during the layout phase.</para>
        /// <para><c>marginBottom</c> –– Space reserved for the bottom edge of the margin during the layout phase.</para>
        /// <para><c>marginLeft</c> –– Space reserved for the left edge of the margin during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The margin to apply to all sides.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMargin<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetMargin(
                top: value,
                right: value,
                bottom: value,
                left: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.marginTop"/>, <see cref="IStyle.marginRight"/>,
        /// <see cref="IStyle.marginBottom"/>, <see cref="IStyle.marginLeft"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>marginTop</c> –– Space reserved for the top edge of the margin during the layout phase.</para>
        /// <para><c>marginRight</c> –– Space reserved for the right edge of the margin during the layout phase.</para>
        /// <para><c>marginBottom</c> –– Space reserved for the bottom edge of the margin during the layout phase.</para>
        /// <para><c>marginLeft</c> –– Space reserved for the left edge of the margin during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="top">The top margin, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="right">The right margin, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottom">The bottom margin, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="left">The left margin, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMargin<T>(
            this T style,
            StyleLength? top = null,
            StyleLength? right = null,
            StyleLength? bottom = null,
            StyleLength? left = null)
            where T : IStyle
        {
            if (top.HasValue) style.marginTop = top.Value;
            if (right.HasValue) style.marginRight = right.Value;
            if (bottom.HasValue) style.marginBottom = bottom.Value;
            if (left.HasValue) style.marginLeft = left.Value;

            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.marginRight"/>, <see cref="IStyle.marginLeft"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>marginRight</c> –– Space reserved for the right edge of the margin during the layout phase.</para>
        /// <para><c>marginLeft</c> –– Space reserved for the left edge of the margin during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The horizontal margin to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMarginX<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetMargin(
                right: value,
                left: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.marginTop"/>, <see cref="IStyle.marginBottom"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>marginTop</c> –– Space reserved for the top edge of the margin during the layout phase.</para>
        /// <para><c>marginBottom</c> –– Space reserved for the bottom edge of the margin during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The vertical margin to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMarginY<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetMargin(
                top: value,
                bottom: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.marginTop"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>marginTop</c> –– Space reserved for the top edge of the margin during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The top margin to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMarginTop<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetMargin(top: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.marginRight"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>marginRight</c> –– Space reserved for the right edge of the margin during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The right margin to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMarginRight<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetMargin(right: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.marginBottom"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>marginBottom</c> –– Space reserved for the bottom edge of the margin during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The bottom margin to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMarginBottom<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetMargin(bottom: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.marginLeft"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>marginLeft</c> –– Space reserved for the left edge of the margin during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The left margin to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetMarginLeft<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetMargin(left: value);
        }
        #endregion

        #region Padding
        /// <summary>
        /// Sets <see cref="IStyle.paddingTop"/>, <see cref="IStyle.paddingRight"/>,
        /// <see cref="IStyle.paddingBottom"/>, <see cref="IStyle.paddingLeft"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>paddingTop</c> –– Space reserved for the top edge of the padding during the layout phase.</para>
        /// <para><c>paddingRight</c> –– Space reserved for the right edge of the padding during the layout phase.</para>
        /// <para><c>paddingBottom</c> –– Space reserved for the bottom edge of the padding during the layout phase.</para>
        /// <para><c>paddingLeft</c> –– Space reserved for the left edge of the padding during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The padding to apply to all sides.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetPadding<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetPadding(
                top: value,
                right: value,
                bottom: value,
                left: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.paddingTop"/>, <see cref="IStyle.paddingRight"/>,
        /// <see cref="IStyle.paddingBottom"/>, <see cref="IStyle.paddingLeft"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>paddingTop</c> –– Space reserved for the top edge of the padding during the layout phase.</para>
        /// <para><c>paddingRight</c> –– Space reserved for the right edge of the padding during the layout phase.</para>
        /// <para><c>paddingBottom</c> –– Space reserved for the bottom edge of the padding during the layout phase.</para>
        /// <para><c>paddingLeft</c> –– Space reserved for the left edge of the padding during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="top">The top padding, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="right">The right padding, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottom">The bottom padding, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="left">The left padding, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetPadding<T>(
            this T style,
            StyleLength? top = null,
            StyleLength? right = null,
            StyleLength? bottom = null,
            StyleLength? left = null)
            where T : IStyle
        {
            if (top.HasValue) style.paddingTop = top.Value;
            if (right.HasValue) style.paddingRight = right.Value;
            if (bottom.HasValue) style.paddingBottom = bottom.Value;
            if (left.HasValue) style.paddingLeft = left.Value;

            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.paddingRight"/>, <see cref="IStyle.paddingLeft"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>paddingRight</c> –– Space reserved for the right edge of the padding during the layout phase.</para>
        /// <para><c>paddingLeft</c> –– Space reserved for the left edge of the padding during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The horizontal padding to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetPaddingX<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetPadding(
                right: value,
                left: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.paddingTop"/>, <see cref="IStyle.paddingBottom"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>paddingTop</c> –– Space reserved for the top edge of the padding during the layout phase.</para>
        /// <para><c>paddingBottom</c> –– Space reserved for the bottom edge of the padding during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The vertical padding to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetPaddingY<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetPadding(
                top: value,
                bottom: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.paddingTop"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>paddingTop</c> –– Space reserved for the top edge of the padding during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The top padding to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetPaddingTop<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetPadding(top: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.paddingRight"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>paddingRight</c> –– Space reserved for the right edge of the padding during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The right padding to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetPaddingRight<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetPadding(right: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.paddingBottom"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>paddingBottom</c> –– Space reserved for the bottom edge of the padding during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The bottom padding to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetPaddingBottom<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetPadding(bottom: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.paddingLeft"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>paddingLeft</c> –– Space reserved for the left edge of the padding during the layout phase.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The left padding to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetPaddingLeft<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetPadding(left: value);
        }
        #endregion

        #region Display
        /// <summary>
        /// Sets <see cref="IStyle.display"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Defines how an element is displayed in the layout.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The display mode to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetDisplay<T>(
            this T style,
            StyleEnum<DisplayStyle> value)
            where T : IStyle
        {
            style.display = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.display"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Defines how an element is displayed in the layout.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The display mode to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetDisplay<T>(
            this T style,
            DisplayStyle value)
            where T : IStyle
        {
            style.display = value;
            return style;
        }
        #endregion

        #region Overflow
        /// <summary>
        /// Sets <see cref="IStyle.overflow"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// How a container behaves if its content overflows its own box.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The overflow behavior to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetOverflow<T>(
            this T style,
            StyleEnum<Overflow> value)
            where T : IStyle
        {
            style.overflow = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.overflow"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// How a container behaves if its content overflows its own box.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The overflow behavior to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetOverflow<T>(
            this T style,
            Overflow value)
            where T : IStyle
        {
            style.overflow = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityOverflowClipBox"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies which box the element content is clipped against.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The overflow clip box to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityOverflowClipBox<T>(
            this T style,
            StyleEnum<OverflowClipBox> value)
            where T : IStyle
        {
            style.unityOverflowClipBox = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityOverflowClipBox"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies which box the element content is clipped against.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The overflow clip box to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityOverflowClipBox<T>(
            this T style,
            OverflowClipBox value)
            where T : IStyle
        {
            style.unityOverflowClipBox = value;
            return style;
        }
        #endregion

        #region Distance
        /// <summary>
        /// Sets <see cref="IStyle.top"/>, <see cref="IStyle.right"/>,
        /// <see cref="IStyle.bottom"/>, <see cref="IStyle.left"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>top</c> –– Top distance from the element's box during layout.</para>
        /// <para><c>right</c> –– Right distance from the element's box during layout.</para>
        /// <para><c>bottom</c> –– Bottom distance from the element's box during layout.</para>
        /// <para><c>left</c> –– Left distance from the element's box during layout.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The distance to apply to all sides.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetDistance<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetDistance(
                top: value,
                right: value,
                bottom: value,
                left: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.top"/>, <see cref="IStyle.right"/>,
        /// <see cref="IStyle.bottom"/>, <see cref="IStyle.left"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>top</c> –– Top distance from the element's box during layout.</para>
        /// <para><c>right</c> –– Right distance from the element's box during layout.</para>
        /// <para><c>bottom</c> –– Bottom distance from the element's box during layout.</para>
        /// <para><c>left</c> –– Left distance from the element's box during layout.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="top">The top offset, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="right">The right offset, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottom">The bottom offset, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="left">The left offset, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetDistance<T>(
            this T style,
            StyleLength? top = null,
            StyleLength? right = null,
            StyleLength? bottom = null,
            StyleLength? left = null)
            where T : IStyle
        {
            if (top.HasValue) style.top = top.Value;
            if (right.HasValue) style.right = right.Value;
            if (bottom.HasValue) style.bottom = bottom.Value;
            if (left.HasValue) style.left = left.Value;

            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.right"/>, <see cref="IStyle.left"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>right</c> –– Right distance from the element's box during layout.</para>
        /// <para><c>left</c> –– Left distance from the element's box during layout.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The horizontal offset to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetDistanceX<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetDistance(
                right: value,
                left: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.top"/>, <see cref="IStyle.bottom"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>top</c> –– Top distance from the element's box during layout.</para>
        /// <para><c>bottom</c> –– Bottom distance from the element's box during layout.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The vertical offset to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetDistanceY<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetDistance(
                top: value,
                bottom: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.top"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>top</c> –– Top distance from the element's box during layout.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The top offset to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetTop<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetDistance(top: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.right"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>right</c> –– Right distance from the element's box during layout.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The right offset to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetRight<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetDistance(right: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.bottom"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>bottom</c> –– Bottom distance from the element's box during layout.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The bottom offset to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBottom<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetDistance(bottom: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.left"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>left</c> –– Left distance from the element's box during layout.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The left offset to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetLeft<T>(
            this T style,
            StyleLength value)
            where T : IStyle
        {
            return style.SetDistance(left: value);
        }
        #endregion

        #region Material
#if UNITY_6000_3_OR_NEWER
        /// <summary>
        /// Sets <see cref="IStyle.unityMaterial"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Custom material to use on the element.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The material to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityMaterial<T>(
            this T style,
            StyleMaterialDefinition value)
            where T : IStyle
        {
            style.unityMaterial = value;
            return style;
        }
#endif
        #endregion

        #region Transform
        /// <summary>
        /// Sets <see cref="IStyle.scale"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// A scaling transformation.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The scale transformation to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetScale<T>(
            this T style,
            StyleScale value)
            where T : IStyle
        {
            style.scale = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.rotate"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// A rotation transformation.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The rotation to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetRotate<T>(
            this T style,
            StyleRotate value)
            where T : IStyle
        {
            style.rotate = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.translate"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// A translate transformation.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The translation to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetTranslate<T>(
            this T style,
            StyleTranslate value)
            where T : IStyle
        {
            style.translate = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.position"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Element's positioning in its parent container.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The position type to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetPosition<T>(
            this T style,
            StyleEnum<Position> value)
            where T : IStyle
        {
            style.position = value;
            return style;
        }



        /// <summary>
        /// Sets <see cref="IStyle.position"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Element's positioning in its parent container.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The position type to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetPosition<T>(
            this T style,
            Position value)
            where T : IStyle
        {
            style.position = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.transformOrigin"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// The transformation origin is the point around which a transformation is applied.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The transform origin to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetTransformOrigin<T>(
            this T style,
            StyleTransformOrigin value)
            where T : IStyle
        {
            style.transformOrigin = value;
            return style;
        }
        #endregion

        #region Background
        /// <summary>
        /// Sets <see cref="IStyle.backgroundColor"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Background color to paint in the element's box.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The background color to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBackgroundColor<T>(
            this T style,
            StyleColor value)
            where T : IStyle
        {
            style.backgroundColor = value;
            return style;
        }

        /// <summary>
        /// Parses an HTML color string and sets <see cref="IStyle.backgroundColor"/>, returning the style for chaining.
        /// </summary>
        /// <remarks>
        /// Background color to paint in the element's box.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The HTML color string to parse (e.g. "#RRGGBB" or a named color).</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBackgroundColor<T>(
            this T style,
            string value)
            where T : IStyle
        {
            if (!ColorUtility.TryParseHtmlString(value, out var color))
            {
                Debug.LogWarning($"Failed to parse color string: '{value}'");
                return style;
            }
            
            style.SetBackgroundColor(color);
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.backgroundImage"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Background image to paint in the element's box.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The background image to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBackgroundImage<T>(
            this T style,
            StyleBackground value)
            where T : IStyle
        {
            style.backgroundImage = value;
            return style;
        }

        /// <summary>
        /// Loads a <see cref="Texture2D"/> from Resources and sets the <see cref="IStyle.backgroundImage"/> property.
        /// </summary>
        /// <param name="style">The style to modify.</param>
        /// <param name="path">The Resources path of the texture to load.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBackgroundImageFromResource<T>(
            this T style,
            string path)
            where T : IStyle
        {
            var texture = Resources.Load<Texture2D>(path);
            if (texture == null)
            {
                Debug.LogWarning($"Failed to load Texture2D from Resources path: '{path}'");
                return style;
            }
            
            style.backgroundImage = texture;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.backgroundSize"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Background image size value. Transitions are fully supported only when using size in pixels or percentages, such as pixel-to-pixel or percentage-to-percentage transitions.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The background size to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBackgroundSize<T>(
            this T style,
            StyleBackgroundSize value)
            where T : IStyle
        {
            style.backgroundSize = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.backgroundRepeat"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Background image repeat value.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The background repeat mode to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBackgroundRepeat<T>(
            this T style,
            StyleBackgroundRepeat value)
            where T : IStyle
        {
            style.backgroundRepeat = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityBackgroundImageTintColor"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Tinting color for the element's backgroundImage.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The background image tint color to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityBackgroundImageTintColor<T>(
            this T style,
            StyleColor value)
            where T : IStyle
        {
            style.unityBackgroundImageTintColor = value;
            return style;
        }

        /// <summary>
        /// Sets the background image tint color by parsing an HTML color string via <see cref="ColorUtility.TryParseHtmlString"/>.
        /// </summary>
        /// <typeparam name="T">The style type.</typeparam>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The HTML color string (e.g. "#FF0000", "red").</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnityBackgroundImageTintColor<T>(
            this T style,
            string value)
            where T : IStyle
        {
            if (!ColorUtility.TryParseHtmlString(value, out var color))
            {
                Debug.LogWarning($"Failed to parse color string: '{value}'");
                return style;
            }
            
            return style.SetUnityBackgroundImageTintColor(color);
        }

        /// <summary>
        /// Sets <see cref="IStyle.backgroundPositionX"/>, <see cref="IStyle.backgroundPositionY"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>backgroundPositionX</c> –– Background image x position value.</para>
        /// <para><c>backgroundPositionY</c> –– Background image y position value.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The background position to apply to both axes.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBackgroundPosition<T>(
            this T style,
            StyleBackgroundPosition value)
            where T : IStyle
        {
            return style.SetBackgroundPosition(
                x: value,
                y: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.backgroundPositionX"/>, <see cref="IStyle.backgroundPositionY"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>backgroundPositionX</c> –– Background image x position value.</para>
        /// <para><c>backgroundPositionY</c> –– Background image y position value.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="x">The horizontal background position, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="y">The vertical background position, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBackgroundPosition<T>(
            this T style,
            StyleBackgroundPosition? x = null,
            StyleBackgroundPosition? y = null)
            where T : IStyle
        {
            if (x.HasValue) style.backgroundPositionX = x.Value;
            if (y.HasValue) style.backgroundPositionY = y.Value;

            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.backgroundPositionX"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>backgroundPositionX</c> –– Background image x position value.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The horizontal background position to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBackgroundPositionX<T>(
            this T style,
            StyleBackgroundPosition value)
            where T : IStyle
        {
            return style.SetBackgroundPosition(x: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.backgroundPositionY"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>backgroundPositionY</c> –– Background image y position value.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The vertical background position to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBackgroundPositionY<T>(
            this T style,
            StyleBackgroundPosition value)
            where T : IStyle
        {
            return style.SetBackgroundPosition(y: value);
        }
        #endregion

        #region Transition
        /// <summary>
        /// Sets <see cref="IStyle.transitionDelay"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Duration to wait before starting a property's transition effect when its value changes.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The transition delays to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetTransitionDelay<T>(
            this T style,
            StyleList<TimeValue> value)
            where T : IStyle
        {
            style.transitionDelay = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.transitionDuration"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Time a transition animation should take to complete.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The transition durations to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetTransitionDuration<T>(
            this T style,
            StyleList<TimeValue> value)
            where T : IStyle
        {
            style.transitionDuration = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.transitionProperty"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Properties to which a transition effect should be applied.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The transition properties to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetTransitionProperty<T>(
            this T style,
            StyleList<StylePropertyName> value)
            where T : IStyle
        {
            style.transitionProperty = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.transitionTimingFunction"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Determines how intermediate values are calculated for properties modified by a transition effect.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The transition timing functions to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetTransitionTimingFunction<T>(
            this T style,
            StyleList<EasingFunction> value)
            where T : IStyle
        {
            style.transitionTimingFunction = value;
            return style;
        }
        #endregion

        #region UnitySlice
        /// <summary>
        /// Sets <see cref="IStyle.unitySliceScale"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Scale applied to an element's slices.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The slice scale to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnitySliceScale<T>(
            this T style,
            StyleFloat value)
            where T : IStyle
        {
            style.unitySliceScale = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unitySliceTop"/>, <see cref="IStyle.unitySliceRight"/>,
        /// <see cref="IStyle.unitySliceBottom"/>, <see cref="IStyle.unitySliceLeft"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>unitySliceTop</c> –– Size of the 9-slice's top edge when painting an element's background image.</para>
        /// <para><c>unitySliceRight</c> –– Size of the 9-slice's right edge when painting an element's background image.</para>
        /// <para><c>unitySliceBottom</c> –– Size of the 9-slice's bottom edge when painting an element's background image.</para>
        /// <para><c>unitySliceLeft</c> –– Size of the 9-slice's left edge when painting an element's background image.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The slice width to apply to all sides.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnitySlice<T>(
            this T style,
            StyleInt value)
            where T : IStyle
        {
            return style.SetUnitySlice(
                top: value,
                right: value,
                bottom: value,
                left: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.unitySliceTop"/>, <see cref="IStyle.unitySliceRight"/>,
        /// <see cref="IStyle.unitySliceBottom"/>, <see cref="IStyle.unitySliceLeft"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>unitySliceTop</c> –– Size of the 9-slice's top edge when painting an element's background image.</para>
        /// <para><c>unitySliceRight</c> –– Size of the 9-slice's right edge when painting an element's background image.</para>
        /// <para><c>unitySliceBottom</c> –– Size of the 9-slice's bottom edge when painting an element's background image.</para>
        /// <para><c>unitySliceLeft</c> –– Size of the 9-slice's left edge when painting an element's background image.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="top">The top slice width, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="right">The right slice width, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottom">The bottom slice width, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="left">The left slice width, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnitySlice<T>(
            this T style,
            StyleInt? top = null,
            StyleInt? right = null,
            StyleInt? bottom = null,
            StyleInt? left = null)
            where T : IStyle
        {
            if (top.HasValue) style.unitySliceTop = top.Value;
            if (right.HasValue) style.unitySliceRight = right.Value;
            if (bottom.HasValue) style.unitySliceBottom = bottom.Value;
            if (left.HasValue) style.unitySliceLeft = left.Value;

            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unitySliceRight"/>, <see cref="IStyle.unitySliceLeft"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>unitySliceRight</c> –– Size of the 9-slice's right edge when painting an element's background image.</para>
        /// <para><c>unitySliceLeft</c> –– Size of the 9-slice's left edge when painting an element's background image.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The horizontal slice width to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnitySliceX<T>(
            this T style,
            StyleInt value)
            where T : IStyle
        {
            return style.SetUnitySlice(
                right: value,
                left: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.unitySliceTop"/>, <see cref="IStyle.unitySliceBottom"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>unitySliceTop</c> –– Size of the 9-slice's top edge when painting an element's background image.</para>
        /// <para><c>unitySliceBottom</c> –– Size of the 9-slice's bottom edge when painting an element's background image.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The vertical slice width to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnitySliceY<T>(
            this T style,
            StyleInt value)
            where T : IStyle
        {
            return style.SetUnitySlice(
                top: value,
                bottom: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.unitySliceTop"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>unitySliceTop</c> –– Size of the 9-slice's top edge when painting an element's background image.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The top slice width to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnitySliceTop<T>(
            this T style,
            StyleInt value)
            where T : IStyle
        {
            return style.SetUnitySlice(top: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.unitySliceRight"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>unitySliceRight</c> –– Size of the 9-slice's right edge when painting an element's background image.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The right slice width to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnitySliceRight<T>(
            this T style,
            StyleInt value)
            where T : IStyle
        {
            return style.SetUnitySlice(right: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.unitySliceBottom"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>unitySliceBottom</c> –– Size of the 9-slice's bottom edge when painting an element's background image.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The bottom slice width to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnitySliceBottom<T>(
            this T style,
            StyleInt value)
            where T : IStyle
        {
            return style.SetUnitySlice(bottom: value);
        }

        /// <summary>
        /// Sets <see cref="IStyle.unitySliceLeft"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// <para><c>unitySliceLeft</c> –– Size of the 9-slice's left edge when painting an element's background image.</para>
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The left slice width to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnitySliceLeft<T>(
            this T style,
            StyleInt value)
            where T : IStyle
        {
            return style.SetUnitySlice(left: value);
        }

#if UNITY_6000_0_OR_NEWER
        /// <summary>
        /// Sets <see cref="IStyle.unitySliceType"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies the type of slicing.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The slice type to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnitySliceType<T>(
            this T style,
            StyleEnum<SliceType> value)
            where T : IStyle
        {
            style.unitySliceType = value;
            return style;
        }
        
        /// <summary>
        /// Sets <see cref="IStyle.unitySliceType"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies the type of slicing.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The slice type to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetUnitySliceType<T>(
            this T style,
            SliceType value)
            where T : IStyle
        {
            style.unitySliceType = value;
            return style;
        }
#endif
        #endregion

        #region Visibility
        /// <summary>
        /// Sets <see cref="IStyle.visibility"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies whether an element is visible.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The visibility to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetVisibility<T>(
            this T style,
            StyleEnum<Visibility> value)
            where T : IStyle
        {
            style.visibility = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.visibility"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies whether an element is visible.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The visibility to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetVisibility<T>(
            this T style,
            Visibility value)
            where T : IStyle
        {
            style.visibility = value;
            return style;
        }
        #endregion

        #region WhiteSpace
        /// <summary>
        /// Sets <see cref="IStyle.whiteSpace"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Word wrap over multiple lines if not enough space is available to draw the text of an element.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The white-space mode to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetWhiteSpace<T>(
            this T style,
            StyleEnum<WhiteSpace> value)
            where T : IStyle
        {
            style.whiteSpace = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.whiteSpace"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Word wrap over multiple lines if not enough space is available to draw the text of an element.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The white-space mode to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetWhiteSpace<T>(
            this T style,
            WhiteSpace value)
            where T : IStyle
        {
            style.whiteSpace = value;
            return style;
        }
        #endregion

        #region JustifyContent
        /// <summary>
        /// Sets <see cref="IStyle.justifyContent"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Justification of children on the main axis of this container.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The justify content mode to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetJustifyContent<T>(
            this T style,
            StyleEnum<Justify> value)
            where T : IStyle
        {
            style.justifyContent = value;
            return style;
        }

        /// <summary>
        /// Sets <see cref="IStyle.justifyContent"/> and returns the style for chaining.
        /// </summary>
        /// <remarks>
        /// Justification of children on the main axis of this container.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <param name="value">The justify content mode to set.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetJustifyContent<T>(
            this T style,
            Justify value)
            where T : IStyle
        {
            style.justifyContent = value;
            return style;
        }
        #endregion
    }
}
