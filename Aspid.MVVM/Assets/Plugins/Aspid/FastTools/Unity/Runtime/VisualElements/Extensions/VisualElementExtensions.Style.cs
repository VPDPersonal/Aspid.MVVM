using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static partial class VisualElementExtensions
    {
        #region Flex
        /// <summary>
        /// Sets <see cref="IStyle.flexBasis"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Initial main size of a flex item, on the main flex axis. The final layout might be smaller or larger, according to the flex shrinking and growing determined by the other flex properties.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The flex basis to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetFlexBasis<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetFlexBasis(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.flexGrow"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies how the item will grow relative to the rest of the flexible items inside the same container.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The flex grow factor to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetFlexGrow<T>(
            this T element,
            StyleFloat value)
            where T : VisualElement
        {
            element.style.SetFlexGrow(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.flexShrink"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies how the item will shrink relative to the rest of the flexible items inside the same container.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The flex shrink factor to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetFlexShrink<T>(
            this T element,
            StyleFloat value)
            where T : VisualElement
        {
            element.style.SetFlexShrink(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.flexWrap"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Placement of children over multiple lines if not enough space is available in this container.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The flex wrap mode to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetFlexWrap<T>(
            this T element,
            StyleEnum<Wrap> value)
            where T : VisualElement
        {
            element.style.SetFlexWrap(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.flexDirection"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Direction of the main axis to layout children in a container.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The flex direction to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetFlexDirection<T>(
            this T element,
            FlexDirection value)
            where T : VisualElement
        {
            element.style.SetFlexDirection(value);
            return element;
        }
        #endregion

        #region Size
        /// <summary>
        /// Sets <see cref="IStyle.width"/>, <see cref="IStyle.height"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>width</c> –– Fixed width of an element for the layout.
        /// </para>
        /// <c>height</c> –– Fixed height of an element for the layout.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The size to apply to both width and height.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetSize<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetSize(
                width: value,
                height: value);

            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.width"/>, <see cref="IStyle.height"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>width</c> –– Fixed width of an element for the layout.
        /// </para>
        /// <c>height</c> –– Fixed height of an element for the layout.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="width">The width to set, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="height">The height to set, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetSize<T>(
            this T element,
            StyleLength? width = null,
            StyleLength? height = null)
            where T : VisualElement
        {
            element.style.SetSize(
                width: width,
                height: height);

            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.minWidth"/>, <see cref="IStyle.minHeight"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>minWidth</c> –– Minimum width for an element, when it is flexible or measures its own size.
        /// </para>
        /// <c>minHeight</c> –– Minimum height for an element, when it is flexible or measures its own size.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The minimum size to apply to both width and height.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMinSize<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetMinSize(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.minWidth"/>, <see cref="IStyle.minHeight"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>minWidth</c> –– Minimum width for an element, when it is flexible or measures its own size.
        /// </para>
        /// <c>minHeight</c> –– Minimum height for an element, when it is flexible or measures its own size.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="width">The minimum width to set, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="height">The minimum height to set, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMinSize<T>(
            this T element,
            StyleLength? width = null,
            StyleLength? height = null)
            where T : VisualElement
        {
            element.style.SetMinSize(
                minWidth: width,
                minHeight: height);

            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.maxWidth"/>, <see cref="IStyle.maxHeight"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>maxWidth</c> –– Maximum width for an element, when it is flexible or measures its own size.
        /// </para>
        /// <c>maxHeight</c> –– Maximum height for an element, when it is flexible or measures its own size.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The maximum size to apply to both width and height.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMaxSize<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetMaxSize(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.maxWidth"/>, <see cref="IStyle.maxHeight"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>maxWidth</c> –– Maximum width for an element, when it is flexible or measures its own size.
        /// </para>
        /// <c>maxHeight</c> –– Maximum height for an element, when it is flexible or measures its own size.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="width">The maximum width to set, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="height">The maximum height to set, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMaxSize<T>(
            this T element,
            StyleLength? width = null,
            StyleLength? height = null)
            where T : VisualElement
        {
            element.style.SetMaxSize(
                maxWidth: width,
                maxHeight: height);

            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.width"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>width</c> –– Fixed width of an element for the layout.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The width to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetWidth<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetWidth(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.minWidth"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>minWidth</c> –– Minimum width for an element, when it is flexible or measures its own size.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The minimum width to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMinWidth<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetMinWidth(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.maxWidth"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>maxWidth</c> –– Maximum width for an element, when it is flexible or measures its own size.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The maximum width to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMaxWidth<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetMaxWidth(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.height"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>height</c> –– Fixed height of an element for the layout.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The height to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHeight<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetHeight(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.minHeight"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>minHeight</c> –– Minimum height for an element, when it is flexible or measures its own size.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The minimum height to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMinHeight<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetMinHeight(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.maxHeight"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>maxHeight</c> –– Maximum height for an element, when it is flexible or measures its own size.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The maximum height to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMaxHeight<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetMaxHeight(value);
            return element;
        }
        #endregion

        #region Font
        /// <summary>
        /// Sets <see cref="IStyle.unityFont"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Font to draw the element's text, defined as a Font object.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The font to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnityFont<T>(
            this T element,
            StyleFont value)
            where T : VisualElement
        {
            element.style.SetUnityFont(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.fontSize"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Font size to draw the element's text, specified in point size.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The font size to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetFontSize<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetFontSize(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityFontDefinition"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Font to draw the element's text, defined as a FontDefinition structure. It takes precedence over -unity-font.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The font definition to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnityFontDefinition<T>(
            this T element,
            StyleFontDefinition value)
            where T : VisualElement
        {
            element.style.SetUnityFontDefinition(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityFontStyleAndWeight"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Font style and weight (normal, bold, italic) to draw the element's text.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The font style and weight to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnityFontStyleAndWeight<T>(
            this T element,
            StyleEnum<FontStyle> value)
            where T : VisualElement
        {
            element.style.SetUnityFontStyleAndWeight(value);
            return element;
        }
        #endregion

        #region Text
        /// <summary>
        /// Sets <see cref="IStyle.wordSpacing"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Increases or decreases the space between words.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The word spacing to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetWorldSpacing<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetWorldSpacing(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.letterSpacing"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Increases or decreases the space between characters.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The letter spacing to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLetterSpacing<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetLetterSpacing(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityTextAlign"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Horizontal and vertical text alignment in the element's box.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The text alignment to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnityTextAlign<T>(
            this T element,
            TextAnchor value)
            where T : VisualElement
        {
            element.style.SetUnityTextAlign(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.textShadow"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Drop shadow of the text.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The text shadow to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetTextShadow<T>(
            this T element,
            StyleTextShadow value)
            where T : VisualElement
        {
            element.style.SetTextShadow(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityTextOutlineColor"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Outline color of the text.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The text outline color to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnityTextOutlineColor<T>(
            this T element,
            StyleColor value)
            where T : VisualElement
        {
            element.style.SetUnityTextOutlineColor(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityTextOutlineWidth"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Outline width of the text.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The text outline width to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnityTextOutlineWidth<T>(
            this T element,
            StyleFloat value)
            where T : VisualElement
        {
            element.style.SetUnityTextOutlineWidth(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityParagraphSpacing"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Increases or decreases the space between paragraphs.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The paragraph spacing to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnityParagraphSpacing<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetUnityParagraphSpacing(value);
            return element;
        }

#if UNITY_6000_2_OR_NEWER
        /// <summary>
        /// Sets <see cref="IStyle.unityTextAutoSize"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Overrides any explicit font-size to scale text within the defined minimum and maximum bounds, recalculating as needed to fit its container.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The text auto size settings to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnityTextAutoSize<T>(
            this T element,
            StyleTextAutoSize value)
            where T : VisualElement
        {
            element.style.SetUnityTextAutoSize(value);
            return element;
        }
#endif
#if UNITY_6000_0_OR_NEWER
        /// <summary>
        /// Sets <see cref="IStyle.unityTextGenerator"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Switches between Unity's standard and advanced text generator.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The text generator type to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnityTextGenerator<T>(
            this T element,
            TextGeneratorType value)
            where T : VisualElement
        {
            element.style.SetUnityTextGenerator(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityEditorTextRenderingMode"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// TextElement editor rendering mode.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The editor text rendering mode to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnityEditorTextRenderingMode<T>(
            this T element,
            EditorTextRenderingMode value)
            where T : VisualElement
        {
            element.style.SetUnityEditorTextRenderingMode(value);
            return element;
        }
#endif

        /// <summary>
        /// Sets <see cref="IStyle.textOverflow"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The element's text overflow mode.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The text overflow mode to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetTextOverflow<T>(
            this T element,
            StyleEnum<TextOverflow> value)
            where T : VisualElement
        {
            element.style.SetTextOverflow(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityTextOverflowPosition"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The element's text overflow position.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The text overflow position to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnityTextOverflowPosition<T>(
            this T element,
            TextOverflowPosition value)
            where T : VisualElement
        {
            element.style.SetUnityTextOverflowPosition(value);
            return element;
        }
        #endregion

        #region Color
        /// <summary>
        /// Sets <see cref="IStyle.color"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Color to use when drawing the text of an element.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The text color to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetColor<T>(
            this T element,
            StyleColor value)
            where T : VisualElement
        {
            element.style.SetColor(value);
            return element;
        }
        
        /// <summary>
        /// Parses an HTML color string and sets <see cref="IStyle.color"/>, returning the element for chaining.
        /// </summary>
        /// <remarks>
        /// Color to use when drawing the text of an element.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The HTML color string to parse (e.g. "#RRGGBB" or a named color).</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetColor<T>(
            this T element,
            string value)
            where T : VisualElement
        {
            element.style.SetColor(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.opacity"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies the transparency of an element and of its children.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The opacity to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetOpacity<T>(
            this T element,
            StyleFloat value)
            where T : VisualElement
        {
            element.style.SetOpacity(value);
            return element;
        }
        #endregion

        #region Align
        /// <summary>
        /// Sets <see cref="IStyle.alignSelf"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Similar to align-items, but only for this specific element.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The alignment to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetAlignSelf<T>(
            this T element,
            StyleEnum<Align> value)
            where T : VisualElement
        {
            element.style.SetAlignSelf(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.alignItems"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Alignment of children on the cross axis of this container.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The children alignment to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetAlignItems<T>(
            this T element,
            StyleEnum<Align> value)
            where T : VisualElement
        {
            element.style.SetAlignItems(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.alignContent"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Alignment of the whole area of children on the cross axis if they span over multiple lines in this container.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The content alignment to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetAlignContent<T>(
            this T element,
            StyleEnum<Align> value)
            where T : VisualElement
        {
            element.style.SetAlignContent(value);
            return element;
        }
        #endregion

        #region Aspect
#if UNITY_6000_3_OR_NEWER
        /// <summary>
        /// Sets <see cref="IStyle.aspectRatio"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Sets a preferred aspect ratio for the box, which will be used in the calculation of auto sizes and some other layout functions.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The aspect ratio to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetAspectRation<T>(
            this T element,
            StyleRatio value)
            where T : VisualElement
        {
            element.style.SetAspectRation(value);
            return element;
        }
#endif
        #endregion

        #region Filter
#if UNITY_6000_3_OR_NEWER
        /// <summary>
        /// Sets <see cref="IStyle.filter"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Filter effects to apply to the element.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The filter effects to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetFilter<T>(
            this T element,
            StyleList<FilterFunction> value)
            where T : VisualElement
        {
            element.style.SetFilter(value);
            return element;
        }
#endif
        #endregion

        #region Border
        /// <summary>
        /// Sets <see cref="IStyle.borderTopColor"/>, <see cref="IStyle.borderRightColor"/>,
        /// <see cref="IStyle.borderBottomColor"/>, <see cref="IStyle.borderLeftColor"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>borderTopColor</c> –– Color of the element's top border.
        /// </para>
        /// <c>borderRightColor</c> –– Color of the element's right border.
        /// <para>
        /// <c>borderBottomColor</c> –– Color of the element's bottom border.
        /// </para>
        /// <c>borderLeftColor</c> –– Color of the element's left border.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The border color to apply to all sides.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderColor<T>(
            this T element,
            StyleColor value)
            where T : VisualElement
        {
            element.style.SetBorderColor(
                top: value,
                right: value,
                bottom: value,
                left: value);

            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopColor"/>, <see cref="IStyle.borderRightColor"/>,
        /// <see cref="IStyle.borderBottomColor"/>, <see cref="IStyle.borderLeftColor"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>borderTopColor</c> –– Color of the element's top border.
        /// </para>
        /// <c>borderRightColor</c> –– Color of the element's right border.
        /// <para>
        /// <c>borderBottomColor</c> –– Color of the element's bottom border.
        /// </para>
        /// <c>borderLeftColor</c> –– Color of the element's left border.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="top">The top border color, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="right">The right border color, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottom">The bottom border color, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="left">The left border color, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderColor<T>(
            this T element,
            StyleColor? top = null,
            StyleColor? right = null,
            StyleColor? bottom = null,
            StyleColor? left = null)
            where T : VisualElement
        {
            element.style.SetBorderColor(
                top: top,
                right: right,
                bottom: bottom,
                left: left);

            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderRightColor"/>, <see cref="IStyle.borderLeftColor"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>borderRightColor</c> –– Color of the element's right border.
        /// </para>
        /// <c>borderLeftColor</c> –– Color of the element's left border.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The border color to apply to the left and right sides.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderColorX<T>(
            this T element,
            StyleColor value)
            where T : VisualElement
        {
            element.style.SetBorderColorX(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopColor"/> and <see cref="IStyle.borderBottomColor"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>borderTopColor</c> –– Color of the element's top border.
        /// </para>
        /// <c>borderBottomColor</c> –– Color of the element's bottom border.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The border color to apply to the top and bottom sides.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderColorY<T>(
            this T element,
            StyleColor value)
            where T : VisualElement
        {
            element.style.SetBorderColorY(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopColor"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>borderTopColor</c> –– Color of the element's top border.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The top border color to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderColorTop<T>(
            this T element,
            StyleColor value)
            where T : VisualElement
        {
            element.style.SetBorderColorTop(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderRightColor"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>borderRightColor</c> –– Color of the element's right border.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The right border color to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderColorRight<T>(
            this T element,
            StyleColor value)
            where T : VisualElement
        {
            element.style.SetBorderColorRight(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderBottomColor"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>borderBottomColor</c> –– Color of the element's bottom border.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The bottom border color to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderColorBottom<T>(
            this T element,
            StyleColor value)
            where T : VisualElement
        {
            element.style.SetBorderColorBottom(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderLeftColor"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>borderLeftColor</c> –– Color of the element's left border.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The left border color to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderColorLeft<T>(
            this T element,
            StyleColor value)
            where T : VisualElement
        {
            element.style.SetBorderColorLeft(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopLeftRadius"/>, <see cref="IStyle.borderTopRightRadius"/>,
        /// <see cref="IStyle.borderBottomRightRadius"/>, <see cref="IStyle.borderBottomLeftRadius"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>borderTopLeftRadius</c> –– The radius of the top-left corner when a rounded rectangle is drawn in the element's box.
        /// </para>
        /// <c>borderTopRightRadius</c> –– The radius of the top-right corner when a rounded rectangle is drawn in the element's box.
        /// <para>
        /// <c>borderBottomRightRadius</c> –– The radius of the bottom-right corner when a rounded rectangle is drawn in the element's box.
        /// </para>
        /// <c>borderBottomLeftRadius</c> –– The radius of the bottom-left corner when a rounded rectangle is drawn in the element's box.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The border radius to apply to all corners.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderRadius<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetBorderRadius(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopLeftRadius"/>, <see cref="IStyle.borderTopRightRadius"/>,
        /// <see cref="IStyle.borderBottomRightRadius"/>, <see cref="IStyle.borderBottomLeftRadius"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>borderTopLeftRadius</c> –– The radius of the top-left corner when a rounded rectangle is drawn in the element's box.
        /// </para>
        /// <c>borderTopRightRadius</c> –– The radius of the top-right corner when a rounded rectangle is drawn in the element's box.
        /// <para>
        /// <c>borderBottomRightRadius</c> –– The radius of the bottom-right corner when a rounded rectangle is drawn in the element's box.
        /// </para>
        /// <c>borderBottomLeftRadius</c> –– The radius of the bottom-left corner when a rounded rectangle is drawn in the element's box.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="topLeft">The top-left radius, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="topRight">The top-right radius, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottomRight">The bottom-right radius, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottomLeft">The bottom-left radius, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderRadius<T>(
            this T element,
            StyleLength? topLeft = null,
            StyleLength? topRight = null,
            StyleLength? bottomRight = null,
            StyleLength? bottomLeft = null)
            where T : VisualElement
        {
            element.style.SetBorderRadius(
                topLeft: topLeft,
                topRight: topRight,
                bottomRight: bottomRight,
                bottomLeft: bottomLeft);

            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopLeftRadius"/>, <see cref="IStyle.borderTopRightRadius"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>borderTopLeftRadius</c> –– The radius of the top-left corner when a rounded rectangle is drawn in the element's box.
        /// </para>
        /// <c>borderTopRightRadius</c> –– The radius of the top-right corner when a rounded rectangle is drawn in the element's box.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The radius to apply to both top corners.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderRadiusTop<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetBorderRadiusTop(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderBottomRightRadius"/>, <see cref="IStyle.borderBottomLeftRadius"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>borderBottomRightRadius</c> –– The radius of the bottom-right corner when a rounded rectangle is drawn in the element's box.
        /// </para>
        /// <c>borderBottomLeftRadius</c> –– The radius of the bottom-left corner when a rounded rectangle is drawn in the element's box.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The radius to apply to both bottom corners.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderRadiusBottom<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetBorderRadiusBottom(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopLeftRadius"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>borderTopLeftRadius</c> –– The radius of the top-left corner when a rounded rectangle is drawn in the element's box.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The top-left corner radius to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderRadiusTopLeft<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetBorderRadiusTopLeft(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopRightRadius"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>borderTopRightRadius</c> –– The radius of the top-right corner when a rounded rectangle is drawn in the element's box.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The top-right corner radius to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderRadiusTopRight<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetBorderRadiusTopRight(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderBottomRightRadius"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>borderBottomRightRadius</c> –– The radius of the bottom-right corner when a rounded rectangle is drawn in the element's box.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The bottom-right corner radius to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderRadiusBottomRight<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetBorderRadiusBottomRight(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderBottomLeftRadius"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>borderBottomLeftRadius</c> –– The radius of the bottom-left corner when a rounded rectangle is drawn in the element's box.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The bottom-left corner radius to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderRadiusBottomLeft<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetBorderRadiusBottomLeft(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopWidth"/>, <see cref="IStyle.borderRightWidth"/>,
        /// <see cref="IStyle.borderBottomWidth"/>, <see cref="IStyle.borderLeftWidth"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>borderTopWidth</c> –– Space reserved for the top edge of the border during the layout phase.
        /// </para>
        /// <c>borderRightWidth</c> –– Space reserved for the right edge of the border during the layout phase.
        /// <para>
        /// <c>borderBottomWidth</c> –– Space reserved for the bottom edge of the border during the layout phase.
        /// </para>
        /// <c>borderLeftWidth</c> –– Space reserved for the left edge of the border during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The border width to apply to all sides.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderWidth<T>(
            this T element,
            StyleFloat value)
            where T : VisualElement
        {
            element.style.SetBorderWidth(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopWidth"/>, <see cref="IStyle.borderRightWidth"/>,
        /// <see cref="IStyle.borderBottomWidth"/>, <see cref="IStyle.borderLeftWidth"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>borderTopWidth</c> –– Space reserved for the top edge of the border during the layout phase.
        /// </para>
        /// <c>borderRightWidth</c> –– Space reserved for the right edge of the border during the layout phase.
        /// <para>
        /// <c>borderBottomWidth</c> –– Space reserved for the bottom edge of the border during the layout phase.
        /// </para>
        /// <c>borderLeftWidth</c> –– Space reserved for the left edge of the border during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="top">The top border width, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="right">The right border width, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottom">The bottom border width, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="left">The left border width, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderWidth<T>(this T element,
            StyleFloat? top = null,
            StyleFloat? right = null,
            StyleFloat? bottom = null,
            StyleFloat? left = null)
            where T : VisualElement
        {
            element.style.SetBorderWidth(
                top: top,
                right: right,
                bottom: bottom,
                left: left);

            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderLeftWidth"/> and <see cref="IStyle.borderRightWidth"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>borderRightWidth</c> –– Space reserved for the right edge of the border during the layout phase.
        /// </para>
        /// <c>borderLeftWidth</c> –– Space reserved for the left edge of the border during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The border width to apply to the left and right sides.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderWidthX<T>(
            this T element,
            StyleFloat value)
            where T : VisualElement
        {
            element.style.SetBorderWidthX(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderTopWidth"/> and <see cref="IStyle.borderBottomWidth"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>borderTopWidth</c> –– Space reserved for the top edge of the border during the layout phase.
        /// </para>
        /// <c>borderBottomWidth</c> –– Space reserved for the bottom edge of the border during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The border width to apply to the top and bottom sides.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderWidthY<T>(
            this T element,
            StyleFloat value)
            where T : VisualElement
        {
            element.style.SetBorderWidthY(value);
            return element;
        }
        /// <summary>
        /// Sets <see cref="IStyle.borderTopWidth"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>borderTopWidth</c> –– Space reserved for the top edge of the border during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The top border width to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderWidthTop<T>(
            this T element,
            StyleFloat value)
            where T : VisualElement
        {
            element.style.SetBorderWidthTop(value);
            return element;
        }
        /// <summary>
        /// Sets <see cref="IStyle.borderRightWidth"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>borderRightWidth</c> –– Space reserved for the right edge of the border during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The right border width to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderWidthRight<T>(
            this T element,
            StyleFloat value)
            where T : VisualElement
        {
            element.style.SetBorderWidthRight(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderBottomWidth"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>borderBottomWidth</c> –– Space reserved for the bottom edge of the border during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The bottom border width to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderWidthBottom<T>(
            this T element,
            StyleFloat value)
            where T : VisualElement
        {
            element.style.SetBorderWidthBottom(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.borderLeftWidth"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>borderLeftWidth</c> –– Space reserved for the left edge of the border during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The left border width to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBorderWidthLeft<T>(
            this T element,
            StyleFloat value)
            where T : VisualElement
        {
            element.style.SetBorderWidthLeft(value);
            return element;
        }
        #endregion

        #region Cursor
        /// <summary>
        /// Sets <see cref="IStyle.cursor"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Mouse cursor to display when the mouse pointer is over an element.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The cursor style to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetCursor<T>(
            this T element,
            StyleCursor value)
            where T : VisualElement
        {
            element.style.SetCursor(value);
            return element;
        }
        #endregion

        #region Margin
        /// <summary>
        /// Sets <see cref="IStyle.marginTop"/>, <see cref="IStyle.marginRight"/>,
        /// <see cref="IStyle.marginBottom"/>, <see cref="IStyle.marginLeft"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>marginTop</c> –– Space reserved for the top edge of the margin during the layout phase.
        /// </para>
        /// <c>marginRight</c> –– Space reserved for the right edge of the margin during the layout phase.
        /// <para>
        /// <c>marginBottom</c> –– Space reserved for the bottom edge of the margin during the layout phase.
        /// </para>
        /// <c>marginLeft</c> –– Space reserved for the left edge of the margin during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The margin to apply to all sides.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMargin<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetMargin(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.marginTop"/>, <see cref="IStyle.marginRight"/>,
        /// <see cref="IStyle.marginBottom"/>, <see cref="IStyle.marginLeft"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>marginTop</c> –– Space reserved for the top edge of the margin during the layout phase.
        /// </para>
        /// <c>marginRight</c> –– Space reserved for the right edge of the margin during the layout phase.
        /// <para>
        /// <c>marginBottom</c> –– Space reserved for the bottom edge of the margin during the layout phase.
        /// </para>
        /// <c>marginLeft</c> –– Space reserved for the left edge of the margin during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="top">The top margin, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="right">The right margin, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottom">The bottom margin, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="left">The left margin, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMargin<T>(
            this T element,
            StyleLength? top = null,
            StyleLength? right = null,
            StyleLength? bottom = null,
            StyleLength? left = null)
            where T : VisualElement
        {
            element.style.SetMargin(
                top: top,
                right: right,
                bottom: bottom,
                left: left);

            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.marginRight"/>, <see cref="IStyle.marginLeft"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>marginRight</c> –– Space reserved for the right edge of the margin during the layout phase.
        /// </para>
        /// <c>marginLeft</c> –– Space reserved for the left edge of the margin during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The horizontal margin to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMarginX<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetMarginX(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.marginTop"/>, <see cref="IStyle.marginBottom"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>marginTop</c> –– Space reserved for the top edge of the margin during the layout phase.
        /// </para>
        /// <c>marginBottom</c> –– Space reserved for the bottom edge of the margin during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The vertical margin to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMarginY<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetMarginY(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.marginTop"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>marginTop</c> –– Space reserved for the top edge of the margin during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The top margin to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMarginTop<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetMarginTop(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.marginRight"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>marginRight</c> –– Space reserved for the right edge of the margin during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The right margin to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMarginRight<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetMarginRight(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.marginBottom"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>marginBottom</c> –– Space reserved for the bottom edge of the margin during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The bottom margin to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMarginBottom<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetMarginBottom(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.marginLeft"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>marginLeft</c> –– Space reserved for the left edge of the margin during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The left margin to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMarginLeft<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetMarginLeft(value);
            return element;
        }
        #endregion

        #region Padding
        /// <summary>
        /// Sets <see cref="IStyle.paddingTop"/>, <see cref="IStyle.paddingRight"/>,
        /// <see cref="IStyle.paddingBottom"/>, <see cref="IStyle.paddingLeft"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>paddingTop</c> –– Space reserved for the top edge of the padding during the layout phase.
        /// </para>
        /// <c>paddingRight</c> –– Space reserved for the right edge of the padding during the layout phase.
        /// <para>
        /// <c>paddingBottom</c> –– Space reserved for the bottom edge of the padding during the layout phase.
        /// </para>
        /// <c>paddingLeft</c> –– Space reserved for the left edge of the padding during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The padding to apply to all sides.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetPadding<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetPadding(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.paddingTop"/>, <see cref="IStyle.paddingRight"/>,
        /// <see cref="IStyle.paddingBottom"/>, <see cref="IStyle.paddingLeft"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>paddingTop</c> –– Space reserved for the top edge of the padding during the layout phase.
        /// </para>
        /// <c>paddingRight</c> –– Space reserved for the right edge of the padding during the layout phase.
        /// <para>
        /// <c>paddingBottom</c> –– Space reserved for the bottom edge of the padding during the layout phase.
        /// </para>
        /// <c>paddingLeft</c> –– Space reserved for the left edge of the padding during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="top">The top padding, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="right">The right padding, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottom">The bottom padding, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="left">The left padding, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetPadding<T>(
            this T element,
            StyleLength? top = null,
            StyleLength? right = null,
            StyleLength? bottom = null,
            StyleLength? left = null)
            where T : VisualElement
        {
            element.style.SetPadding(
                top: top,
                right: right,
                bottom: bottom,
                left: left);

            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.paddingRight"/>, <see cref="IStyle.paddingLeft"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>paddingRight</c> –– Space reserved for the right edge of the padding during the layout phase.
        /// </para>
        /// <c>paddingLeft</c> –– Space reserved for the left edge of the padding during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The horizontal padding to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetPaddingX<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetPaddingX(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.paddingTop"/>, <see cref="IStyle.paddingBottom"/>  and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>paddingTop</c> –– Space reserved for the top edge of the padding during the layout phase.
        /// </para>
        /// <c>paddingBottom</c> –– Space reserved for the bottom edge of the padding during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The vertical padding to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetPaddingY<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetPaddingY(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.paddingTop"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>paddingTop</c> –– Space reserved for the top edge of the padding during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The top padding to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetPaddingTop<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetPaddingTop(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.paddingRight"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>paddingRight</c> –– Space reserved for the right edge of the padding during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The right padding to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetPaddingRight<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetPaddingRight(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.paddingBottom"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>paddingBottom</c> –– Space reserved for the bottom edge of the padding during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The bottom padding to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetPaddingBottom<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetPaddingBottom(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.paddingLeft"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>paddingLeft</c> –– Space reserved for the left edge of the padding during the layout phase.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The left padding to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetPaddingLeft<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetPaddingLeft(value);
            return element;
        }
        #endregion

        #region Display
        /// <summary>
        /// Sets <see cref="IStyle.display"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Defines how an element is displayed in the layout.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The display mode to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetDisplay<T>(
            this T element,
            DisplayStyle value)
            where T : VisualElement
        {
            element.style.SetDisplay(value);
            return element;
        }
        #endregion

        #region Overflow
        /// <summary>
        /// Sets <see cref="IStyle.overflow"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// How a container behaves if its content overflows its own box.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The overflow behavior to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetOverflow<T>(
            this T element,
            StyleEnum<Overflow> value)
            where T : VisualElement
        {
            element.style.SetOverflow(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityOverflowClipBox"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies which box the element content is clipped against.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The overflow clip box to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnityOverflowClipBox<T>(
            this T element,
            StyleEnum<OverflowClipBox> value)
            where T : VisualElement
        {
            element.style.SetUnityOverflowClipBox(value);
            return element;
        }
        #endregion

        #region Distance
        /// <summary>
        /// Sets <see cref="IStyle.top"/>, <see cref="IStyle.right"/>,
        /// <see cref="IStyle.bottom"/>, <see cref="IStyle.left"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>top</c> –– Top distance from the element's box during layout.
        /// </para>
        /// <c>right</c> –– Right distance from the element's box during layout.
        /// <para>
        /// <c>bottom</c> –– Bottom distance from the element's box during layout.
        /// </para>
        /// <c>left</c> –– Left distance from the element's box during layout.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The distance to apply to all sides.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetDistance<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetDistance(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.top"/>, <see cref="IStyle.right"/>,
        /// <see cref="IStyle.bottom"/>, <see cref="IStyle.left"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>top</c> –– Top distance from the element's box during layout.
        /// </para>
        /// <c>right</c> –– Right distance from the element's box during layout.
        /// <para>
        /// <c>bottom</c> –– Bottom distance from the element's box during layout.
        /// </para>
        /// <c>left</c> –– Left distance from the element's box during layout.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="top">The top offset, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="right">The right offset, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottom">The bottom offset, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="left">The left offset, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetDistance<T>(
            this T element,
            StyleLength? top = null,
            StyleLength? right = null,
            StyleLength? bottom = null,
            StyleLength? left = null)
            where T : VisualElement
        {
            element.style.SetDistance(
                top: top,
                right: right,
                bottom: bottom,
                left: left);

            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.right"/>, <see cref="IStyle.left"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>right</c> –– Right distance from the element's box during layout.
        /// </para>
        /// <c>left</c> –– Left distance from the element's box during layout.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The horizontal offset to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetDistanceX<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetDistanceX(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.top"/>, <see cref="IStyle.bottom"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>top</c> –– Top distance from the element's box during layout.
        /// </para>
        /// <c>bottom</c> –– Bottom distance from the element's box during layout.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The vertical offset to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetDistanceY<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetDistanceY(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.top"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>top</c> –– Top distance from the element's box during layout.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The top offset to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetTop<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetTop(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.right"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>right</c> –– Right distance from the element's box during layout.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The right offset to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetRight<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetRight(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.bottom"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>bottom</c> –– Bottom distance from the element's box during layout.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The bottom offset to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBottom<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetBottom(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.left"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>left</c> –– Left distance from the element's box during layout.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The left offset to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLeft<T>(
            this T element,
            StyleLength value)
            where T : VisualElement
        {
            element.style.SetLeft(value);
            return element;
        }
        #endregion

        #region Material
#if UNITY_6000_3_OR_NEWER
        /// <summary>
        /// Sets <see cref="IStyle.unityMaterial"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Custom material to use on the element.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The material to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnityMaterial<T>(
            this T element,
            StyleMaterialDefinition value)
            where T : VisualElement
        {
            element.style.SetUnityMaterial(value);
            return element;
        }
#endif
        #endregion

        #region Transform
        /// <summary>
        /// Sets <see cref="IStyle.scale"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// A scaling transformation.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The scale transformation to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetScale<T>(
            this T element,
            StyleScale value)
            where T : VisualElement
        {
            element.style.SetScale(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.rotate"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// A rotation transformation.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The rotation to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetRotate<T>(
            this T element,
            StyleRotate value)
            where T : VisualElement
        {
            element.style.SetRotate(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.translate"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// A translate transformation.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The translation to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetTranslate<T>(
            this T element,
            StyleTranslate value)
            where T : VisualElement
        {
            element.style.SetTranslate(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.position"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Element's positioning in its parent container.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The position type to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetPosition<T>(
            this T element,
            StyleEnum<Position> value)
            where T : VisualElement
        {
            element.style.SetPosition(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.transformOrigin"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The transformation origin is the point around which a transformation is applied.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The transform origin to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetTransformOrigin<T>(
            this T element,
            StyleTransformOrigin value)
            where T : VisualElement
        {
            element.style.SetTransformOrigin(value);
            return element;
        }
        #endregion

        #region Background
        /// <summary>
        /// Sets <see cref="IStyle.backgroundColor"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Background color to paint in the element's box.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The background color to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBackgroundColor<T>(
            this T element,
            StyleColor value)
            where T : VisualElement
        {
            element.style.SetBackgroundColor(value);
            return element;
        }
        
        /// <summary>
        /// Parses an HTML color string and sets <see cref="IStyle.backgroundColor"/>, returning the element for chaining.
        /// </summary>
        /// <remarks>
        /// Background color to paint in the element's box.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The HTML color string to parse (e.g. "#RRGGBB" or a named color).</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBackgroundColor<T>(
            this T element,
            string value)
            where T : VisualElement
        {
            element.style.SetBackgroundColor(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.backgroundImage"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Background image to paint in the element's box.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The background image to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBackgroundImage<T>(
            this T element,
            StyleBackground value)
            where T : VisualElement
        {
            element.style.SetBackgroundImage(value);
            return element;
        }
        
        /// <summary>
        /// Loads a <see cref="Texture2D"/> from Resources and sets the <see cref="IStyle.backgroundImage"/> property.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="path">The Resources path of the texture to load.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetBackgroundImageFromResource<T>(
            this T element,
            string path)
            where T : VisualElement
        {
            element.style.SetBackgroundImageFromResource(path);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.backgroundSize"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Background image size value. Transitions are fully supported only when using size in pixels or percentages, such as pixel-to-pixel or percentage-to-percentage transitions.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The background size to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBackgroundSize<T>(
            this T element,
            StyleBackgroundSize value)
            where T : VisualElement
        {
            element.style.SetBackgroundSize(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.backgroundRepeat"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Background image repeat value.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The background repeat mode to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBackgroundRepeat<T>(
            this T element,
            StyleBackgroundRepeat value)
            where T : VisualElement
        {
            element.style.SetBackgroundRepeat(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unityBackgroundImageTintColor"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Tinting color for the element's backgroundImage.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The background image tint color to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnityBackgroundImageTintColor<T>(
            this T element,
            StyleColor value)
            where T : VisualElement
        {
            element.style.SetUnityBackgroundImageTintColor(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.backgroundPositionX"/>, <see cref="IStyle.backgroundPositionY"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>backgroundPositionX</c> –– Background image x position value.
        /// </para>
        /// <c>backgroundPositionY</c> –– Background image y position value.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The background position to apply to both axes.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBackgroundPosition<T>(
            this T element,
            StyleBackgroundPosition value)
            where T : VisualElement
        {
            element.style.SetBackgroundPosition(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.backgroundPositionX"/>, <see cref="IStyle.backgroundPositionY"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>backgroundPositionX</c> –– Background image x position value.
        /// </para>
        /// <c>backgroundPositionY</c> –– Background image y position value.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="x">The horizontal background position, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="y">The vertical background position, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBackgroundPosition<T>(
            this T element,
            StyleBackgroundPosition? x = null,
            StyleBackgroundPosition? y = null)
            where T : VisualElement
        {
            element.style.SetBackgroundPosition(x, y);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.backgroundPositionX"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>backgroundPositionX</c> –– Background image x position value.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The horizontal background position to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBackgroundPositionX<T>(
            this T element,
            StyleBackgroundPosition value)
            where T : VisualElement
        {
            element.style.SetBackgroundPositionX(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.backgroundPositionY"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>backgroundPositionY</c> –– Background image y position value.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The vertical background position to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBackgroundPositionY<T>(
            this T element,
            StyleBackgroundPosition value)
            where T : VisualElement
        {
            element.style.SetBackgroundPositionY(value);
            return element;
        }
        #endregion

        #region Transition
        /// <summary>
        /// Sets <see cref="IStyle.transitionDelay"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Duration to wait before starting a property's transition effect when its value changes.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The transition delays to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetTransitionDelay<T>(
            this T element,
            StyleList<TimeValue> value)
            where T : VisualElement
        {
            element.style.SetTransitionDelay(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.transitionDuration"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Time a transition animation should take to complete.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The transition durations to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetTransitionDuration<T>(
            this T element,
            StyleList<TimeValue> value)
            where T : VisualElement
        {
            element.style.SetTransitionDuration(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.transitionProperty"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Properties to which a transition effect should be applied.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The transition properties to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetTransitionProperty<T>(
            this T element,
            StyleList<StylePropertyName> value)
            where T : VisualElement
        {
            element.style.SetTransitionProperty(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.transitionTimingFunction"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Determines how intermediate values are calculated for properties modified by a transition effect.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The transition timing functions to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetTransitionTimingFunction<T>(
            this T element,
            StyleList<EasingFunction> value)
            where T : VisualElement
        {
            element.style.SetTransitionTimingFunction(value);
            return element;
        }
        #endregion

        #region UnitySlice
        /// <summary>
        /// Sets <see cref="IStyle.unitySliceScale"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Scale applied to an element's slices.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The slice scale to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnitySliceScale<T>(
            this T element,
            StyleFloat value)
            where T : VisualElement
        {
            element.style.SetUnitySliceScale(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unitySliceTop"/>, <see cref="IStyle.unitySliceRight"/>,
        /// <see cref="IStyle.unitySliceBottom"/>, <see cref="IStyle.unitySliceLeft"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>unitySliceTop</c> –– Size of the 9-slice's top edge when painting an element's background image.
        /// </para>
        /// <c>unitySliceRight</c> –– Size of the 9-slice's right edge when painting an element's background image.
        /// <para>
        /// <c>unitySliceBottom</c> –– Size of the 9-slice's bottom edge when painting an element's background image.
        /// </para>
        /// <c>unitySliceLeft</c> –– Size of the 9-slice's left edge when painting an element's background image.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The slice width to apply to all sides.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnitySlice<T>(
            this T element,
            StyleInt value)
            where T : VisualElement
        {
            element.style.SetUnitySlice(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unitySliceTop"/>, <see cref="IStyle.unitySliceRight"/>,
        /// <see cref="IStyle.unitySliceBottom"/>, <see cref="IStyle.unitySliceLeft"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>unitySliceTop</c> –– Size of the 9-slice's top edge when painting an element's background image.
        /// </para>
        /// <c>unitySliceRight</c> –– Size of the 9-slice's right edge when painting an element's background image.
        /// <para>
        /// <c>unitySliceBottom</c> –– Size of the 9-slice's bottom edge when painting an element's background image.
        /// </para>
        /// <c>unitySliceLeft</c> –– Size of the 9-slice's left edge when painting an element's background image.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="top">The top slice width, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="right">The right slice width, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="bottom">The bottom slice width, or <see langword="null"/> to leave unchanged.</param>
        /// <param name="left">The left slice width, or <see langword="null"/> to leave unchanged.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnitySlice<T>(
            this T element,
            StyleInt? top = null,
            StyleInt? right = null,
            StyleInt? bottom = null,
            StyleInt? left = null)
            where T : VisualElement
        {
            element.style.SetUnitySlice(
                top: top,
                right: right,
                bottom: bottom,
                left: left);

            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unitySliceRight"/>, <see cref="IStyle.unitySliceLeft"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>unitySliceRight</c> –– Size of the 9-slice's right edge when painting an element's background image.
        /// </para>
        /// <c>unitySliceLeft</c> –– Size of the 9-slice's left edge when painting an element's background image.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The horizontal slice width to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnitySliceX<T>(
            this T element,
            StyleInt value)
            where T : VisualElement
        {
            element.style.SetUnitySliceX(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unitySliceTop"/>, <see cref="IStyle.unitySliceBottom"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>unitySliceTop</c> –– Size of the 9-slice's top edge when painting an element's background image.
        /// </para>
        /// <c>unitySliceBottom</c> –– Size of the 9-slice's bottom edge when painting an element's background image.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The vertical slice width to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnitySliceY<T>(
            this T element,
            StyleInt value)
            where T : VisualElement
        {
            element.style.SetUnitySliceY(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unitySliceTop"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>unitySliceTop</c> –– Size of the 9-slice's top edge when painting an element's background image.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The top slice width to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnitySliceTop<T>(
            this T element,
            StyleInt value)
            where T : VisualElement
        {
            element.style.SetUnitySliceTop(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unitySliceRight"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>unitySliceRight</c> –– Size of the 9-slice's right edge when painting an element's background image.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The right slice width to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnitySliceRight<T>(
            this T element,
            StyleInt value)
            where T : VisualElement
        {
            element.style.SetUnitySliceRight(value);
            return element;
        }

        /// <summary>
        /// <see cref="IStyle.unitySliceBottom"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>unitySliceBottom</c> –– Size of the 9-slice's bottom edge when painting an element's background image.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The bottom slice width to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnitySliceBottom<T>(
            this T element,
            StyleInt value)
            where T : VisualElement
        {
            element.style.SetUnitySliceBottom(value);
            return element;
        }

        /// <summary>
        /// Sets <see cref="IStyle.unitySliceLeft"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// <c>unitySliceLeft</c> –– Size of the 9-slice's left edge when painting an element's background image.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The left slice width to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnitySliceLeft<T>(
            this T element,
            StyleInt value)
            where T : VisualElement
        {
            element.style.SetUnitySliceLeft(value);
            return element;
        }

#if UNITY_6000_0_OR_NEWER
        /// <summary>
        /// Sets <see cref="IStyle.unitySliceType"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies the type of sclicing.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The slice type to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnitySliceType<T>(
            this T element,
            StyleEnum<SliceType> value)
            where T : VisualElement
        {
            element.style.SetUnitySliceType(value);
            return element;
        }
#endif
        #endregion

        #region Visibility
        /// <summary>
        /// Sets <see cref="IStyle.visibility"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies whether an element is visible.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The visibility to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetVisibility<T>(
            this T element,
            StyleEnum<Visibility> value)
            where T : VisualElement
        {
            element.style.SetVisibility(value);
            return element;
        }
        #endregion

        #region WhiteSpace
        /// <summary>
        /// Sets <see cref="IStyle.whiteSpace"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Word wrap over multiple lines if not enough space is available to draw the text of an element.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The white-space mode to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetWhiteSpace<T>(
            this T element,
            StyleEnum<WhiteSpace> value)
            where T : VisualElement
        {
            element.style.SetWhiteSpace(value);
            return element;
        }
        #endregion

        #region JustifyContent
        /// <summary>
        /// Sets <see cref="IStyle.justifyContent"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Justification of children on the main axis of this container.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The justify content mode to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetJustifyContent<T>(
            this T element,
            StyleEnum<Justify> value)
            where T : VisualElement
        {
            element.style.SetJustifyContent(value);
            return element;
        }
        #endregion
    }
}
