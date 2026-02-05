using UnityEngine;
using UnityEngine.UIElements;

// TODO Aspid.UnityFastTools 
// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    public static class IStyleExtensions
    {
        #region Flex
        public static T SetFlexBasis<T>(this T style, StyleLength value)
            where T : IStyle
        {
            style.flexBasis = value;
            return style;
        }
        
        public static T SetFlexGrow<T>(this T style, StyleFloat value)
            where T : IStyle
        {
            style.flexGrow = value;
            return style;
        }
        
        public static T SetFlexShrink<T>(this T style, StyleFloat value)
            where T : IStyle
        {
            style.flexShrink = value;
            return style;
        }
        
        public static T SetFlexWrap<T>(this T style, StyleEnum<Wrap> value)
            where T : IStyle
        {
            style.flexWrap = value;
            return style;
        }
        
        public static T SetFlexDirection<T>(this T style, FlexDirection value)
            where T : IStyle
        {
            style.flexDirection = value;
            return style;
        }
        #endregion
        
        #region Size
        public static T SetSize<T>(this T style, StyleLength value)
            where T : IStyle
        {
            return style.SetSize(width: value, height: value);
        }
        
        public static T SetSize<T>(this T style, StyleLength? width = null, StyleLength? height = null)
            where T : IStyle
        {
            if (width.HasValue) style.width = width.Value;
            if (height.HasValue) style.height = height.Value;

            return style;
        }
        
        public static T SetMinSize<T>(this T style, StyleLength value)
            where T : IStyle
        {
            return style.SetMinSize(width: value, height: value);
        }
        
        public static T SetMinSize<T>(this T style, StyleLength? width = null, StyleLength? height = null)
            where T : IStyle
        {
            if (width.HasValue) style.minWidth = width.Value;
            if (height.HasValue) style.minHeight = height.Value;

            return style;
        }
        
        public static T SetMaxSize<T>(this T style, StyleLength value)
            where T : IStyle
        {
            return style.SetMaxSize(width: value, height: value);
        }
        
        public static T SetMaxSize<T>(this T style, StyleLength? width = null, StyleLength? height = null)
            where T : IStyle
        {
            if (width.HasValue) style.maxWidth = width.Value;
            if (height.HasValue) style.maxHeight = height.Value;

            return style;
        }
        #endregion
        
        #region Font
        public static T SetUnityFont<T>(this T style, StyleFont value)
            where T : IStyle
        {
            style.unityFont = value;
            return style;
        }
        
        public static T SetFontSize<T>(this T style, StyleLength value)
            where T : IStyle
        {
            style.fontSize = value;
            return style;
        }
        
        public static T SetUnityFontDefinition<T>(this T style, StyleFontDefinition value)
            where T : IStyle
        {
            style.unityFontDefinition = value;
            return style;
        }
        
        public static T SetUnityFontStyleAndWeight<T>(this T style, StyleEnum<FontStyle> value)
            where T : IStyle
        {
            style.unityFontStyleAndWeight = value;
            return style;
        }
        #endregion

        #region Text
        public static T SetWorldSpacing<T>(this T style, StyleLength value)
            where T : IStyle
        {
            style.wordSpacing = value;
            return style;
        }
        
        public static T SetLetterSpacing<T>(this T style, StyleLength value)
            where T : IStyle
        {
            style.letterSpacing = value;
            return style;
        }
        
        public static T SetUnityTextAlign<T>(this T style, TextAnchor value)
            where T : IStyle
        {
            style.unityTextAlign = value;
            return style;
        }
        
        public static T SetTextShadow<T>(this T style, StyleTextShadow value)
            where T : IStyle
        {
            style.textShadow = value;
            return style;
        }
        
        public static T SetUnityTextOutlineColor<T>(this T style, StyleColor value)
            where T : IStyle
        {
            style.unityTextOutlineColor = value;
            return style;
        }
        
        public static T SetUnityTextOutlineWidth<T>(this T style, StyleFloat value)
            where T : IStyle
        {
            style.unityTextOutlineWidth = value;
            return style;
        }
        
        public static T SetUnityParagraphSpacing<T>(this T style, StyleLength value)
            where T : IStyle
        {
            style.unityParagraphSpacing = value;
            return style;
        }
        
#if UNITY_6000_2_OR_NEWER
        public static T SetUnityTextAutoSize<T>(this T style, StyleTextAutoSize value)
            where T : IStyle
        {
            style.unityTextAutoSize = value;
            return style;
        }
#endif
        
#if UNITY_6000_0_OR_NEWER
        public static T SetUnityTextGenerator<T>(this T style, TextGeneratorType value)
            where T : IStyle
        {
            style.unityTextGenerator = value;
            return style;
        }
        
        public static T SetUnityEditorTextRenderingMode<T>(this T style, EditorTextRenderingMode value)
            where T : IStyle
        {
            style.unityEditorTextRenderingMode = value;
            return style;
        }
#endif
        
        public static T SetTextOverflow<T>(this T style, StyleEnum<TextOverflow> value)
            where T : IStyle
        {
            style.textOverflow = value;
            return style;
        }
        
        public static T SetUnityTextOverflowPosition<T>(this T style, TextOverflowPosition value)
            where T : IStyle
        {
            style.unityTextOverflowPosition = value;
            return style;
        }
        #endregion
        
        #region Color
        public static T SetColor<T>(this T style, StyleColor value)
            where T : IStyle
        {
            style.color = value;
            return style;
        }
        
        public static T SetOpacity<T>(this T style, StyleFloat value)
            where T : IStyle
        {
            style.opacity = value;
            return style;
        }
        #endregion
        
        #region Align
        public static T SetAlignSelf<T>(this T style, StyleEnum<Align> value)
            where T : IStyle
        {
            style.alignSelf = value;
            return style;
        }
        
        public static T SetAlignItems<T>(this T style, StyleEnum<Align> value)
            where T : IStyle
        {
            style.alignItems = value;
            return style;
        }
        
        public static T SetAlignContent<T>(this T style, StyleEnum<Align> value)
            where T : IStyle
        {
            style.alignContent = value;
            return style;
        }
        #endregion
        
        #region Border
        public static T SetBorderColor<T>(this T style, StyleColor value)
            where T : IStyle
        {
            return style.SetBorderColor(top: value, bottom: value, left: value, right: value);
        }
        
        public static T SetBorderColor<T>(this T style, 
            StyleColor? top,
            StyleColor? bottom,
            StyleColor? left,
            StyleColor? right)
            where T : IStyle
        {
            if (top.HasValue) style.borderTopColor = top.Value;
            if (bottom.HasValue) style.borderBottomColor = bottom.Value;
            
            if (left.HasValue) style.borderLeftColor = left.Value;
            if (right.HasValue) style.borderRightColor = right.Value;
            
            return style;
        }
        
        public static T SetBorderRadius<T>(this T style, StyleLength value)
            where T : IStyle
        {
            return style.SetBorderRadius(topLeft: value, topRight: value, bottomLeft: value, bottomRight: value);
        }
        
        public static T SetBorderRadius<T>(this T style,
            StyleLength? topLeft = null, 
            StyleLength? topRight = null,
            StyleLength? bottomLeft = null,
            StyleLength? bottomRight = null)
            where T : IStyle
        {
            if (topLeft.HasValue) style.borderTopLeftRadius = topLeft.Value;
            if (topRight.HasValue) style.borderTopRightRadius = topRight.Value;
            
            if (bottomLeft.HasValue) style.borderBottomLeftRadius = bottomLeft.Value;
            if (bottomRight.HasValue) style.borderBottomRightRadius = bottomRight.Value;

            return style;
        }
        
        public static T SetBorderWidth<T>(this T style, StyleFloat value)
            where T : IStyle
        {
            return style.SetBorderWidth(top: value, bottom: value, left: value, right: value);
        }
        
        public static T SetBorderWidth<T>(this T style,
            StyleFloat? top = null, 
            StyleFloat? bottom = null,
            StyleFloat? left = null,
            StyleFloat? right = null)
            where T : IStyle
        {
            if (top.HasValue) style.borderTopWidth = top.Value;
            if (bottom.HasValue) style.borderBottomWidth = bottom.Value;
            
            if (left.HasValue) style.borderLeftWidth = left.Value;
            if (right.HasValue) style.borderRightWidth = right.Value;

            return style;
        }
        #endregion
        
        #region Cursor
        public static T SetCursor<T>(this T style, StyleCursor value)
            where T : IStyle
        {
            style.cursor = value;
            return style;
        }
        #endregion
        
        #region Margin
        public static T SetMargin<T>(this T style, StyleLength value)
            where T : IStyle
        {
            return style.SetMargin(top: value, bottom: value, left: value, right: value);
        }
        
        public static T SetMargin<T>(this T style,
            StyleLength? top = null, 
            StyleLength? bottom = null,
            StyleLength? left = null,
            StyleLength? right = null)
            where T : IStyle
        {
            if (top.HasValue) style.marginTop = top.Value;
            if (bottom.HasValue) style.marginBottom = bottom.Value;
            
            if (left.HasValue) style.marginLeft = left.Value;
            if (right.HasValue) style.marginRight = right.Value;

            return style;
        }
        #endregion

        #region Padding
        public static T SetPadding<T>(this T style, StyleLength value)
            where T : IStyle
        {
            return style.SetPadding(top: value, bottom: value, left: value, right: value);
        }
        
        public static T SetPadding<T>(this T style,
            StyleLength? top = null, 
            StyleLength? bottom = null,
            StyleLength? left = null,
            StyleLength? right = null)
            where T : IStyle
        {
            if (top.HasValue) style.paddingTop = top.Value;
            if (bottom.HasValue) style.paddingBottom = bottom.Value;
            
            if (left.HasValue) style.paddingLeft = left.Value;
            if (right.HasValue) style.paddingRight = right.Value;

            return style;
        }
        #endregion
        
        #region Display
        public static T SetDisplay<T>(this T style, DisplayStyle value)
            where T : IStyle
        {
            style.display = value;
            return style;
        }
        #endregion
        
        #region Overflow
        public static T SetOverflow<T>(this T style, StyleEnum<Overflow> value)
            where T : IStyle
        {
            style.overflow = value;
            return style;
        }
        
        public static T SetUnityOverflowClipBox<T>(this T style, StyleEnum<OverflowClipBox> value)
            where T : IStyle
        {
            style.unityOverflowClipBox = value;
            return style;
        }
        #endregion
        
        #region Distance
        public static T SetDistance<T>(this T style, StyleLength value)
            where T : IStyle
        {
            return style.SetDistance(top: value, bottom: value, left: value, right: value);
        }
        
        public static T SetDistance<T>(this T style, 
            StyleLength? top = null,
            StyleLength? bottom = null,
            StyleLength? left = null,
            StyleLength? right = null)
            where T : IStyle
        {
            if (top.HasValue) style.top =  top.Value;
            if (bottom.HasValue) style.bottom =  bottom.Value;
            
            if (left.HasValue) style.left =  left.Value;
            if (right.HasValue) style.right =  right.Value;

            return style;
        }
        #endregion

        #region Transform
        public static T SetScale<T>(this T style, StyleScale value)
            where T : IStyle
        {
            style.scale = value;
            return style;
        }
        
        public static T SetRotate<T>(this T style, StyleRotate value)
            where T : IStyle
        {
            style.rotate = value;
            return style;
        }
        
        public static T SetTranslate<T>(this T style, StyleTranslate value)
            where T : IStyle
        {
            style.translate = value;
            return style;
        }
        
        public static T SetPosition<T>(this T style, StyleEnum<Position> value)
            where T : IStyle
        {
            style.position = value;
            return style;
        }
        
        public static T SetTransformOrigin<T>(this T style, StyleTransformOrigin value)
            where T : IStyle
        {
            style.transformOrigin = value;
            return style;
        }
        #endregion
        
        #region Background
        public static T SetBackgroundColor<T>(this T style, StyleColor value)
            where T : IStyle
        {
            style.backgroundColor = value;
            return style;
        }
        
        public static T SetBackgroundImage<T>(this T style, StyleBackground value)
            where T : IStyle
        {
            style.backgroundImage = value;
            return style;
        }
        
        public static T SetBackgroundSize<T>(this T style, StyleBackgroundSize value)
            where T : IStyle
        {
            style.backgroundSize = value;
            return style;
        }
        
        public static T SetBackgroundRepeat<T>(this T style, StyleBackgroundRepeat value)
            where T : IStyle
        {
            style.backgroundRepeat = value;
            return style;
        }
        
        public static T SetUnityBackgroundImageTintColor<T>(this T style, StyleColor value)
            where T : IStyle
        {
            style.unityBackgroundImageTintColor = value;
            return style;
        }
        
        public static T SetBackgroundPosition<T>(this T style, StyleBackgroundPosition value)
            where T : IStyle
        {
            return style.SetBackgroundPosition(x: value, y: value);
        }
        
        public static T SetBackgroundPosition<T>(this T style,
            StyleBackgroundPosition? x = null,
            StyleBackgroundPosition? y = null)
            where T : IStyle
        {
            if (x.HasValue) style.backgroundPositionX = x.Value;
            if (y.HasValue) style.backgroundPositionY = y.Value;
            
            return style;
        }
        #endregion

        #region Transition
        public static T SetTransitionDelay<T>(this T style, StyleList<TimeValue> value)
            where T : IStyle
        {
            style.transitionDelay = value;
            return style;
        }
        
        public static T SetTransitionDuration<T>(this T style, StyleList<TimeValue> value)
            where T : IStyle
        {
            style.transitionDuration = value;
            return style;
        }
        
        public static T SetTransitionProperty<T>(this T style, StyleList<StylePropertyName> value)
            where T : IStyle
        {
            style.transitionProperty = value;
            return style;
        }
        
        public static T SetTransitionTimingFunction<T>(this T style, StyleList<EasingFunction> value)
            where T : IStyle
        {
            style.transitionTimingFunction = value;
            return style;
        }
        #endregion

        #region UnitySlice
        public static T SetUnitySlice<T>(this T style, StyleInt value)
            where T : IStyle
        {
            return style.SetUnitySlice(top: value, bottom: value, left: value, right: value);
        }
        
        public static T SetUnitySlice<T>(this T style, 
            StyleInt? top = null,
            StyleInt? bottom = null,
            StyleInt? left = null,
            StyleInt? right = null)
            where T : IStyle
        {
            if (top.HasValue) style.unitySliceTop = top.Value;
            if (bottom.HasValue) style.unitySliceBottom = bottom.Value;
            
            if (left.HasValue) style.unitySliceLeft = left.Value;
            if (right.HasValue) style.unitySliceRight = right.Value;
            
            return style;
        }
        
#if UNITY_6000_0_OR_NEWER
        public static T SetUnitySliceType<T>(this T style, StyleEnum<SliceType> value)
            where T : IStyle
        {
            style.unitySliceType = value;
            return style;
        }
#endif
        #endregion
        
        #region Visibility
        public static T SetVisibility<T>(this T style, StyleEnum<Visibility> value)
            where T : IStyle
        {
            style.visibility = value;
            return style;
        }
        #endregion

        #region WhiteSpace
        public static T SetWhiteSpace<T>(this T style, StyleEnum<WhiteSpace> value)
            where T : IStyle
        {
            style.whiteSpace = value;
            return style;
        }
        #endregion
        
        #region JustifyContent
        public static T SetJustifyContent<T>(this T style, StyleEnum<Justify> value)
            where T : IStyle
        {
            style.justifyContent = value;
            return style;
        }
        #endregion
    }
}