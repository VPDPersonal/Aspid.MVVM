using UnityEngine;
using UnityEngine.UIElements;

namespace Aspid.CustomEditors
{
    public static class VisualElementStyleExtensions
    {
        public static T SetMargin<T>(this T element,
            StyleLength? top = null, 
            StyleLength? bottom = null,
            StyleLength? left = null,
            StyleLength? right = null)
            where T : VisualElement
        {
            if (top != null) element.style.marginTop = top.Value;
            if (bottom != null) element.style.marginBottom = bottom.Value;
            
            if (left != null) element.style.marginLeft = left.Value;
            if (right != null) element.style.marginRight = right.Value;

            return element;
        }
        
        public static T SetPadding<T>(this T element,
            StyleLength? top = null, 
            StyleLength? bottom = null,
            StyleLength? left = null,
            StyleLength? right = null)
            where T : VisualElement
        {
            if (top != null) element.style.paddingTop = top.Value;
            if (bottom != null) element.style.paddingBottom = bottom.Value;
            
            if (left != null) element.style.paddingLeft = left.Value;
            if (right != null) element.style.paddingRight = right.Value;

            return element;
        }
        
        public static T SetBorderRadius<T>(this T element,
            StyleLength? topLeft = null, 
            StyleLength? topRight = null,
            StyleLength? bottomLeft = null,
            StyleLength? bottomRight = null)
            where T : VisualElement
        {
            if (topLeft != null) element.style.borderTopLeftRadius = topLeft.Value;
            if (topRight != null) element.style.borderTopRightRadius = topRight.Value;
            
            if (bottomLeft != null) element.style.borderBottomLeftRadius = bottomLeft.Value;
            if (bottomRight != null) element.style.borderBottomRightRadius = bottomRight.Value;

            return element;
        }

        #region Font
        public static T SetFontSize<T>(this T element, StyleLength size)
            where T : VisualElement
        {
            element.style.fontSize = size;
            return element;
        }
        
        public static T SetUnityFont<T>(this T element, StyleFont style)
            where T : VisualElement
        {
            element.style.unityFont = style;
            return element;
        }
        
        public static T SetUnityFontDefinition<T>(this T element, StyleFontDefinition style)
            where T : VisualElement
        {
            element.style.unityFontDefinition = style;
            return element;
        }
        
        public static T SetUnityFontStyleAndWeight<T>(this T element, StyleEnum<FontStyle> style)
            where T : VisualElement
        {
            element.style.unityFontStyleAndWeight = style;
            return element;
        }
        #endregion

        #region Align
        public static T SetAlignSelf<T>(this T element, StyleEnum<Align> align)
            where T : VisualElement
        {
            element.style.alignSelf = align;
            return element;
        }
        
        public static T SetAlignItems<T>(this T element, StyleEnum<Align> align)
            where T : VisualElement
        {
            element.style.alignItems = align;
            return element;
        }
        
        public static T SetAlignContent<T>(this T element, StyleEnum<Align> align)
            where T : VisualElement
        {
            element.style.alignContent = align;
            return element;
        }
        #endregion

        #region Color
        public static T SetColor<T>(this T element, StyleColor color)
            where T : VisualElement
        {
            element.style.color = color;
            return element;
        }
        
        public static T SetBackgroundColor<T>(this T element, StyleColor color)
            where T : VisualElement
        {
            element.style.backgroundColor = color;
            return element;
        }
        #endregion

        #region Flex
        public static T SetFlexGrow<T>(this T element, StyleFloat flexGrow)
            where T : VisualElement
        {
            element.style.flexGrow = flexGrow;
            return element;
        }
        
        public static T SetFlexBasis<T>(this T element, StyleLength flexBasis)
            where T : VisualElement
        {
            element.style.flexBasis = flexBasis;
            return element;
        }
        
        public static T SetFlexShrink<T>(this T element, StyleFloat flexShrink)
            where T : VisualElement
        {
            element.style.flexShrink = flexShrink;
            return element;
        }
        
        public static T SetFlexWrap<T>(this T element, StyleEnum<Wrap> flexWrap)
            where T : VisualElement
        {
            element.style.flexWrap = flexWrap;
            return element;
        }   
        
        
        public static T SetFlexDirection<T>(this T element, FlexDirection flexDirection)
            where T : VisualElement
        {
            element.style.flexDirection = flexDirection;
            return element;
        }
        #endregion

        #region Overflow
        public static T SetOverflow<T>(this T element, StyleEnum<Overflow> overflow)
            where T : VisualElement
        {
            element.style.overflow = overflow;
            return element;
        }
        
        public static T SetTextOverflow<T>(this T element, StyleEnum<TextOverflow> textOverflow)
            where T : VisualElement
        {
            element.style.textOverflow = textOverflow;
            return element;
        } 
        #endregion
        
        public static T SetWhiteSpace<T>(this T element, StyleEnum<WhiteSpace> whiteSpace)
            where T : VisualElement
        {
            element.style.whiteSpace = whiteSpace;
            return element;
        }
        
        public static T SetDisplay<T>(this T element, DisplayStyle style)
            where T : VisualElement
        {
            element.style.display = style;
            return element;
        }
        
        public static T SetSize<T>(this T element, StyleLength? width = null, StyleLength? height = null)
            where T : VisualElement
        {
            if (width != null) element.style.width = width.Value;
            if (height != null) element.style.height = height.Value;

            return element;
        }
    }
}