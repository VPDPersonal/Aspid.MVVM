using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    public static class IStyleExtensions
    {
        public static T SetMargin<T>(this T style,
            StyleLength? top = null, 
            StyleLength? bottom = null,
            StyleLength? left = null,
            StyleLength? right = null)
            where T : IStyle
        {
            if (top is not null) style.marginTop = top.Value;
            if (bottom is not null) style.marginBottom = bottom.Value;
            
            if (left is not null) style.marginLeft = left.Value;
            if (right is not null) style.marginRight = right.Value;

            return style;
        }
        
        public static T SetPadding<T>(this T style,
            StyleLength? top = null, 
            StyleLength? bottom = null,
            StyleLength? left = null,
            StyleLength? right = null)
            where T : IStyle
        {
            if (top is not null) style.paddingTop = top.Value;
            if (bottom is not null) style.paddingBottom = bottom.Value;
            
            if (left is not null) style.paddingLeft = left.Value;
            if (right is not null) style.paddingRight = right.Value;

            return style;
        }
        
        #region Font
        public static T SetFontSize<T>(this T style, StyleLength size)
            where T : IStyle
        {
            style.fontSize = size;
            return style;
        }
        
        public static T SetUnityFont<T>(this T style, StyleFont font)
            where T : IStyle
        {
            style.unityFont = font;
            return style;
        }
        
        public static T SetUnityFontDefinition<T>(this T style, StyleFontDefinition fontDefinition)
            where T : IStyle
        {
            style.unityFontDefinition = fontDefinition;
            return style;
        }
        
        public static T SetUnityFontStyleAndWeight<T>(this T style, StyleEnum<FontStyle> fontStyle)
            where T : IStyle
        {
            style.unityFontStyleAndWeight = fontStyle;
            return style;
        }
        #endregion
        
        #region Flex
        public static T SetFlexGrow<T>(this T style, StyleFloat flexGrow)
            where T : IStyle
        {
            style.flexGrow = flexGrow;
            return style;
        }
        
        public static T SetFlexBasis<T>(this T style, StyleLength flexBasis)
            where T : IStyle
        {
            style.flexBasis = flexBasis;
            return style;
        }
        
        public static T SetFlexShrink<T>(this T style, StyleFloat flexShrink)
            where T : IStyle
        {
            style.flexShrink = flexShrink;
            return style;
        }
        
        public static T SetFlexWrap<T>(this T style, StyleEnum<Wrap> flexWrap)
            where T : IStyle
        {
            style.flexWrap = flexWrap;
            return style;
        }   
        
        
        public static T SetFlexDirection<T>(this T style, FlexDirection flexDirection)
            where T : IStyle
        {
            style.flexDirection = flexDirection;
            return style;
        }
        #endregion
        
        #region Size
        public static T SetSize<T>(this T style, StyleLength? width = null, StyleLength? height = null)
            where T : IStyle
        {
            if (width is not null) style.width = width.Value;
            if (height is not null) style.height = height.Value;

            return style;
        }
        
        public static T SetMinSize<T>(this T style, StyleLength? width = null, StyleLength? height = null)
            where T : IStyle
        {
            if (width is not null) style.minWidth = width.Value;
            if (height is not null) style.minHeight = height.Value;

            return style;
        }
        #endregion

        #region Text
        public static T SetUnityTextAlign<T>(this T style, TextAnchor textAnchor)
            where T : IStyle
        {
            style.unityTextAlign = textAnchor;
            return style;
        }
        
        public static T SetTextShadow<T>(this T style, StyleTextShadow textShadow)
            where T : IStyle
        {
            style.textShadow = textShadow;
            return style;
        }
        
        public static T SetUnityTextOutlineColor<T>(this T style, StyleColor color)
            where T : IStyle
        {
            style.unityTextOutlineColor = color;
            return style;
        }
        
        public static T SetUnityTextAutoSize<T>(this T style, StyleTextAutoSize textAutoSize)
            where T : IStyle
        {
            style.unityTextAutoSize = textAutoSize;
            return style;
        }
        
        public static T SetTextOverflow<T>(this T style, StyleEnum<TextOverflow> textOverflow)
            where T : IStyle
        {
            style.textOverflow = textOverflow;
            return style;
        } 
        
        public static T SetUnityTextGenerator<T>(this T style, TextGeneratorType textGeneratorType)
            where T : IStyle
        {
            style.unityTextGenerator = textGeneratorType;
            return style;
        }
        
        public static T SetUnityTextOutlineWidth<T>(this T style, StyleFloat unityTextOutlineWidth)
            where T : IStyle
        {
            style.unityTextOutlineWidth = unityTextOutlineWidth;
            return style;
        }
        
        public static T SetUnityTextOverflowPosition<T>(this T style, TextOverflowPosition unityTextOutlineWidth)
            where T : IStyle
        {
            style.unityTextOverflowPosition = unityTextOutlineWidth;
            return style;
        }
        
        public static T SetUnityEditorTextRenderingMode<T>(this T style, EditorTextRenderingMode editorTextRenderingMode)
            where T : IStyle
        {
            style.unityEditorTextRenderingMode = editorTextRenderingMode;
            return style;
        }
        #endregion
        
        #region Align
        public static T SetAlignSelf<T>(this T style, StyleEnum<Align> align)
            where T : IStyle
        {
            style.alignSelf = align;
            return style;
        }
        
        public static T SetAlignItems<T>(this T style, StyleEnum<Align> align)
            where T : IStyle
        {
            style.alignItems = align;
            return style;
        }
        
        public static T SetAlignContent<T>(this T style, StyleEnum<Align> align)
            where T : IStyle
        {
            style.alignContent = align;
            return style;
        }
        #endregion
        
        #region Color
        public static T SetColor<T>(this T style, StyleColor color)
            where T : IStyle
        {
            style.color = color;
            return style;
        }
        
        public static T SetBackgroundColor<T>(this T style, StyleColor color)
            where T : IStyle
        {
            style.backgroundColor = color;
            return style;
        }
        #endregion
        
        #region Border
        public static T SetBorderWidth<T>(this T style,
            StyleFloat? top = null, 
            StyleFloat? bottom = null,
            StyleFloat? left = null,
            StyleFloat? right = null)
            where T : IStyle
        {
            if (top is not null) style.borderTopWidth = top.Value;
            if (bottom is not null) style.borderBottomWidth = bottom.Value;
            
            if (left is not null) style.borderLeftWidth = left.Value;
            if (right is not null) style.borderRightWidth = right.Value;

            return style;
        }
        
        public static T SetBorderRadius<T>(this T style,
            StyleLength? topLeft = null, 
            StyleLength? topRight = null,
            StyleLength? bottomLeft = null,
            StyleLength? bottomRight = null)
            where T : IStyle
        {
            if (topLeft is not null) style.borderTopLeftRadius = topLeft.Value;
            if (topRight is not null) style.borderTopRightRadius = topRight.Value;
            
            if (bottomLeft is not null) style.borderBottomLeftRadius = bottomLeft.Value;
            if (bottomRight is not null) style.borderBottomRightRadius = bottomRight.Value;

            return style;
        }
        #endregion
        
        public static T SetDisplay<T>(this T style, DisplayStyle display)
            where T : IStyle
        {
            style.display = display;
            return style;
        }
        
        public static T SetOverflow<T>(this T style, StyleEnum<Overflow> overflow)
            where T : IStyle
        {
            style.overflow = overflow;
            return style;
        }
        
        public static T SetWhiteSpace<T>(this T style, StyleEnum<WhiteSpace> whiteSpace)
            where T : IStyle
        {
            style.whiteSpace = whiteSpace;
            return style;
        }
    }
}