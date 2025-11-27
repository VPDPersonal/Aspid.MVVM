using UnityEngine;
using UnityEngine.UIElements;

// TODO Aspid.UnityFastTools
// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    public static partial class VisualElementExtensions
    {
        #region Flex
        public static T SetFlexBasis<T>(this T element, StyleLength value)
            where T : VisualElement
        {
            element.style.SetFlexBasis(value);
            return element;
        }
        
        public static T SetFlexGrow<T>(this T element, StyleFloat value)
            where T : VisualElement
        {
            element.style.SetFlexGrow(value);
            return element;
        }
        
        public static T SetFlexShrink<T>(this T element, StyleFloat value)
            where T : VisualElement
        {
            element.style.SetFlexShrink(value);
            return element;
        }
        
        public static T SetFlexWrap<T>(this T element, StyleEnum<Wrap> value)
            where T : VisualElement
        {
            element.style.SetFlexWrap(value);
            return element;
        }
        
        public static T SetFlexDirection<T>(this T element, FlexDirection value)
            where T : VisualElement
        {
            element.style.SetFlexDirection(value);
            return element;
        }
        #endregion
        
        #region Size
        public static T SetSize<T>(this T element, StyleLength value)
            where T : VisualElement
        { 
            element.style.SetSize(value);
            return element;
        }
        
        public static T SetSize<T>(this T element, StyleLength? width = null, StyleLength? height = null)
            where T : VisualElement
        {
            element.style.SetSize(width, height);
            return element;
        }
        
        public static T SetMinSize<T>(this T element, StyleLength value)
            where T : VisualElement
        {
            element.style.SetMinSize(value);
            return element;
        }
        
        public static T SetMinSize<T>(this T element, StyleLength? width = null, StyleLength? height = null)
            where T : VisualElement
        {
            element.style.SetMinSize(width, height);
            return element;
        }
        
        public static T SetMaxSize<T>(this T element, StyleLength value)
            where T : VisualElement
        {
            element.style.SetMaxSize(value);
            return element;
        }
        
        public static T SetMaxSize<T>(this T element, StyleLength? width = null, StyleLength? height = null)
            where T : VisualElement
        {
            element.style.SetMaxSize(width, height);
            return element;
        }
        #endregion
        
        #region Font
        public static T SetUnityFont<T>(this T element, StyleFont value)
            where T : VisualElement
        {
            element.style.SetUnityFont(value);
            return element;
        }
        
        public static T SetFontSize<T>(this T element, StyleLength value)
            where T : VisualElement
        {
            element.style.SetFontSize(value);
            return element;
        }
        
        public static T SetUnityFontDefinition<T>(this T element, StyleFontDefinition value)
            where T : VisualElement
        {
            element.style.SetUnityFontDefinition(value);
            return element;
        }
        
        public static T SetUnityFontStyleAndWeight<T>(this T element, StyleEnum<FontStyle> value)
            where T : VisualElement
        {
            element.style.SetUnityFontStyleAndWeight(value);
            return element;
        }
        #endregion

        #region Text
        public static T SetWorldSpacing<T>(this T element, StyleLength value)
            where T : VisualElement
        {
            element.style.SetWorldSpacing(value);
            return element;
        }
        
        public static T SetLetterSpacing<T>(this T element, StyleLength value)
            where T : VisualElement
        {
            element.style.SetLetterSpacing(value);
            return element;
        }
        
        public static T SetUnityTextAlign<T>(this T element, TextAnchor value)
            where T : VisualElement
        {
            element.style.SetUnityTextAlign(value);
            return element;
        }
        
        public static T SetTextShadow<T>(this T element, StyleTextShadow value)
            where T : VisualElement
        {
            element.style.SetTextShadow(value);
            return element;
        }
        
        public static T SetUnityTextOutlineColor<T>(this T element, StyleColor value)
            where T : VisualElement
        {
            element.style.SetUnityTextOutlineColor(value);
            return element;
        }
        
        public static T SetUnityTextOutlineWidth<T>(this T element, StyleFloat value)
            where T : VisualElement
        {
            element.style.SetUnityTextOutlineWidth(value);
            return element;
        }
        
        public static T SetUnityParagraphSpacing<T>(this T element, StyleLength value)
            where T : VisualElement
        {
            element.style.SetUnityParagraphSpacing(value);
            return element;
        }
        
#if UNITY_6000_0_OR_NEWER
        public static T SetUnityTextAutoSize<T>(this T element, StyleTextAutoSize value)
            where T : VisualElement
        {
            element.style.SetUnityTextAutoSize(value);
            return element;
        }
        
        public static T SetUnityTextGenerator<T>(this T element, TextGeneratorType value)
            where T : VisualElement
        {
            element.style.SetUnityTextGenerator(value);
            return element;
        }
        
        public static T SetUnityEditorTextRenderingMode<T>(this T element, EditorTextRenderingMode value)
            where T : VisualElement
        {
            element.style.SetUnityEditorTextRenderingMode(value);
            return element;
        }
#endif
        
        public static T SetTextOverflow<T>(this T element, StyleEnum<TextOverflow> value)
            where T : VisualElement
        {
            element.style.SetTextOverflow(value);
            return element;
        }
        
        public static T SetUnityTextOverflowPosition<T>(this T element, TextOverflowPosition value)
            where T : VisualElement
        {
            element.style.SetUnityTextOverflowPosition(value);
            return element;
        }
        #endregion
        
        #region Color
        public static T SetColor<T>(this T element, StyleColor value)
            where T : VisualElement
        {
            element.style.SetColor(value);
            return element;
        }
        
        public static T SetOpacity<T>(this T element, StyleFloat value)
            where T : VisualElement
        {
            element.style.SetOpacity(value);
            return element;
        }
        #endregion
        
        #region Align
        public static T SetAlignSelf<T>(this T element, StyleEnum<Align> value)
            where T : VisualElement
        {
            element.style.SetAlignSelf(value);
            return element;
        }
        
        public static T SetAlignItems<T>(this T element, StyleEnum<Align> value)
            where T : VisualElement
        {
            element.style.SetAlignItems(value);
            return element;
        }
        
        public static T SetAlignContent<T>(this T element, StyleEnum<Align> value)
            where T : VisualElement
        {
            element.style.SetAlignContent(value);
            return element;
        }
        #endregion
        
        #region Border
        public static T SetBorderColor<T>(this T element, StyleColor value)
            where T : VisualElement
        { 
            element.style.SetBorderColor(value);
            return element;
        }
        
        public static T SetBorderColor<T>(this T element, 
            StyleColor? top,
            StyleColor? bottom,
            StyleColor? left,
            StyleColor? right)
            where T : VisualElement
        {
            element.style.SetBorderColor(top, bottom, left, right);
            return element;
        }
        
        public static T SetBorderRadius<T>(this T element, StyleLength value)
            where T : VisualElement
        {
            element.style.SetBorderRadius(value);
            return element;
        }
        
        public static T SetBorderRadius<T>(this T element,
            StyleLength? topLeft = null, 
            StyleLength? topRight = null,
            StyleLength? bottomLeft = null,
            StyleLength? bottomRight = null)
            where T : VisualElement
        {
            element.style.SetBorderRadius(topLeft,  topRight, bottomLeft, bottomRight);
            return element;
        }
        
        public static T SetBorderWidth<T>(this T element, StyleFloat value)
            where T : VisualElement
        {
            element.style.SetBorderWidth(value);
            return element;
        }
        
        public static T SetBorderWidth<T>(this T element,
            StyleFloat? top = null, 
            StyleFloat? bottom = null,
            StyleFloat? left = null,
            StyleFloat? right = null)
            where T : VisualElement
        {
            element.style.SetBorderWidth(top, bottom, left, right);
            return element;
        }
        #endregion

        #region Cursor
        public static T SetCursor<T>(this T element, StyleCursor value)
            where T : VisualElement
        {
            element.style.SetCursor(value);
            return element;
        }
        #endregion
        
        #region Margin
        public static T SetMargin<T>(this T element, StyleLength value)
            where T : VisualElement
        {
            element.style.SetMargin(value);
            return element;
        }
        
        public static T SetMargin<T>(this T element,
            StyleLength? top = null, 
            StyleLength? bottom = null,
            StyleLength? left = null,
            StyleLength? right = null)
            where T : VisualElement
        {
            element.style.SetMargin(top, bottom, left, right);
            return element;
        }
        #endregion

        #region Padding
        public static T SetPadding<T>(this T element, StyleLength value)
            where T : VisualElement
        {
            element.style.SetPadding(value);
            return element;
        }
        
        public static T SetPadding<T>(this T element,
            StyleLength? top = null, 
            StyleLength? bottom = null,
            StyleLength? left = null,
            StyleLength? right = null)
            where T : VisualElement
        {
            element.style.SetPadding(top, bottom, left, right);
            return element;
        }
        #endregion
        
        #region Display
        public static T SetDisplay<T>(this T element, DisplayStyle value)
            where T : VisualElement
        {
            element.style.SetDisplay(value);
            return element;
        }
        #endregion
        
        #region Overflow
        public static T SetOverflow<T>(this T element, StyleEnum<Overflow> value)
            where T : VisualElement
        {
            element.style.SetOverflow(value);
            return element;
        }
        
        public static T SetUnityOverflowClipBox<T>(this T element, StyleEnum<OverflowClipBox> value)
            where T : VisualElement
        {
            element.style.SetUnityOverflowClipBox(value);
            return element;
        }
        #endregion
        
        #region Distance
        public static T SetDistance<T>(this T element, StyleLength value)
            where T : VisualElement
        {
            element.style.SetDistance(value);
            return element; 
        }
        
        public static T SetDistance<T>(this T element, 
            StyleLength? top = null,
            StyleLength? bottom = null,
            StyleLength? left = null,
            StyleLength? right = null)
            where T : VisualElement
        {
            element.style.SetDistance(top, bottom, left, right);
            return element; 
        }
        #endregion

        #region Transform
        public static T SetScale<T>(this T element, StyleScale value)
            where T : VisualElement
        {
            element.style.SetScale(value);
            return element;
        }
        
        public static T SetRotate<T>(this T element, StyleRotate value)
            where T : VisualElement
        {
            element.style.SetRotate(value);
            return element;
        }
        
        public static T SetTranslate<T>(this T element, StyleTranslate value)
            where T : VisualElement
        {
            element.style.SetTranslate(value);
            return element;
        }
        
        public static T SetPosition<T>(this T element, StyleEnum<Position> value)
            where T : VisualElement
        {
            element.style.SetPosition(value);
            return element;
        }
        
        public static T SetTransformOrigin<T>(this T element, StyleTransformOrigin value)
            where T : VisualElement
        {
            element.style.SetTransformOrigin(value);
            return element;
        }
        #endregion
        
        #region Background
        public static T SetBackgroundColor<T>(this T element, StyleColor value)
            where T : VisualElement
        {
            element.style.SetBackgroundColor(value);
            return element;
        }
        
        public static T SetBackgroundImage<T>(this T element, StyleBackground value)
            where T : VisualElement
        {
            element.style.SetBackgroundImage(value);
            return element;
        }
        
        public static T SetBackgroundSize<T>(this T element, StyleBackgroundSize value)
            where T : VisualElement
        {
            element.style.SetBackgroundSize(value);
            return element;
        }
        
        public static T SetBackgroundRepeat<T>(this T element, StyleBackgroundRepeat value)
            where T : VisualElement
        {
            element.style.SetBackgroundRepeat(value);
            return element;
        }
        
        public static T SetUnityBackgroundImageTintColor<T>(this T element, StyleColor value)
            where T : VisualElement
        {
            element.style.SetUnityBackgroundImageTintColor(value);
            return element;
        }
        
        public static T SetBackgroundPosition<T>(this T element, StyleBackgroundPosition value)
            where T : VisualElement
        {
            element.style.SetBackgroundPosition(value);
            return element;
        }
        
        public static T SetBackgroundPosition<T>(this T element,
            StyleBackgroundPosition? x = null,
            StyleBackgroundPosition? y = null)
            where T : VisualElement
        {
            element.style.SetBackgroundPosition(x, y);
            return element;
        }
        #endregion

        #region Transition
        public static T SetTransitionDelay<T>(this T element, StyleList<TimeValue> value)
            where T : VisualElement
        {
            element.style.SetTransitionDelay(value);
            return element;
        }
        
        public static T SetTransitionDuration<T>(this T element, StyleList<TimeValue> value)
            where T : VisualElement
        {
            element.style.SetTransitionDuration(value);
            return element;
        }
        
        public static T SetTransitionProperty<T>(this T element, StyleList<StylePropertyName> value)
            where T : VisualElement
        {
            element.style.SetTransitionProperty(value);
            return element;
        }
        
        public static T SetTransitionTimingFunction<T>(this T element, StyleList<EasingFunction> value)
            where T : VisualElement
        {
            element.style.SetTransitionTimingFunction(value);
            return element;
        }
        #endregion

        #region UnitySlice
        public static T SetUnitySlice<T>(this T element, StyleInt value)
            where T : VisualElement
        { 
            element.style.SetUnitySlice(value);
            return element;
        }
        
        public static T SetUnitySlice<T>(this T element, 
            StyleInt? top = null,
            StyleInt? bottom = null,
            StyleInt? left = null,
            StyleInt? right = null)
            where T : VisualElement
        {
            element.style.SetUnitySlice(top, bottom, left, right);
            return element;
        }
        
#if UNITY_6000_0_OR_NEWER
        public static T SetUnitySliceType<T>(this T element, StyleEnum<SliceType> value)
            where T : VisualElement
        {
            element.style.SetUnitySliceType(value);
            return element;
        }
#endif
        #endregion
        
        #region Visibility
        public static T SetVisibility<T>(this T element, StyleEnum<Visibility> value)
            where T : VisualElement
        {
            element.style.SetVisibility(value);
            return element;
        }
        #endregion
        
        #region WhiteSpace
        public static T SetWhiteSpace<T>(this T element, StyleEnum<WhiteSpace> value)
            where T : VisualElement
        {
            element.style.SetWhiteSpace(value);
            return element;
        }
        #endregion
        
        #region JustifyContent
        public static T SetJustifyContent<T>(this T element, StyleEnum<Justify> value)
            where T : VisualElement
        {
            element.style.SetJustifyContent(value);
            return element;
        }
        #endregion
    }
}