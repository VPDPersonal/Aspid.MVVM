using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    public static partial class VisualElementExtensions
    {
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
        
        public static T SetBorderRadius<T>(this T element,
            StyleLength? topLeft = null, 
            StyleLength? topRight = null,
            StyleLength? bottomLeft = null,
            StyleLength? bottomRight = null)
            where T : VisualElement
        {
            element.style.SetBorderRadius(topLeft, topRight, bottomLeft, bottomRight);
            return element;
        }
        
        #region Font
        public static T SetFontSize<T>(this T element, StyleLength size)
            where T : VisualElement
        {
            element.style.SetFontSize(size);
            return element;
        }
        
        public static T SetUnityFont<T>(this T element, StyleFont font)
            where T : VisualElement
        {
            element.style.SetUnityFont(font);
            return element;
        }
        
        public static T SetUnityFontDefinition<T>(this T element, StyleFontDefinition fontDefinition)
            where T : VisualElement
        {
            element.style.SetUnityFontDefinition(fontDefinition);
            return element;
        }
        
        public static T SetUnityFontStyleAndWeight<T>(this T element, StyleEnum<FontStyle> fontStyle)
            where T : VisualElement
        {
            element.style.SetUnityFontStyleAndWeight(fontStyle);
            return element;
        }
        #endregion
        
        #region Align
        public static T SetAlignSelf<T>(this T element, StyleEnum<Align> align)
            where T : VisualElement
        {
            element.style.SetAlignSelf(align);
            return element;
        }
        
        public static T SetAlignItems<T>(this T element, StyleEnum<Align> align)
            where T : VisualElement
        {
            element.style.SetAlignItems(align);
            return element;
        }
        
        public static T SetAlignContent<T>(this T element, StyleEnum<Align> align)
            where T : VisualElement
        {
            element.style.SetAlignContent(align);
            return element;
        }
        #endregion
        
        #region Color
        public static T SetColor<T>(this T element, StyleColor color)
            where T : VisualElement
        {
            element.style.SetColor(color);
            return element;
        }
        
        public static T SetBackgroundColor<T>(this T element, StyleColor color)
            where T : VisualElement
        {
            element.style.SetBackgroundColor(color);
            return element;
        }
        #endregion
        
        #region Flex
        public static T SetFlexGrow<T>(this T element, StyleFloat flexGrow)
            where T : VisualElement
        {
            element.style.SetFlexGrow(flexGrow);
            return element;
        }
        
        public static T SetFlexBasis<T>(this T element, StyleLength flexBasis)
            where T : VisualElement
        {
            element.style.SetFlexBasis(flexBasis);
            return element;
        }
        
        public static T SetFlexShrink<T>(this T element, StyleFloat flexShrink)
            where T : VisualElement
        {
            element.style.SetFlexShrink(flexShrink);
            return element;
        }
        
        public static T SetFlexWrap<T>(this T element, StyleEnum<Wrap> flexWrap)
            where T : VisualElement
        {
            element.style.SetFlexWrap(flexWrap);
            return element;
        }   
        
        
        public static T SetFlexDirection<T>(this T element, FlexDirection flexDirection)
            where T : VisualElement
        {
            element.style.SetFlexDirection(flexDirection);
            return element;
        }
        #endregion
        
        #region Overflow
        public static T SetOverflow<T>(this T element, StyleEnum<Overflow> overflow)
            where T : VisualElement
        {
            element.style.SetOverflow(overflow);
            return element;
        }
        
        public static T SetTextOverflow<T>(this T element, StyleEnum<TextOverflow> textOverflow)
            where T : VisualElement
        {
            element.style.SetTextOverflow(textOverflow);
            return element;
        } 
        #endregion
        
        public static T SetWhiteSpace<T>(this T element, StyleEnum<WhiteSpace> whiteSpace)
            where T : VisualElement
        {
            element.style.SetWhiteSpace(whiteSpace);
            return element;
        }
        
        public static T SetDisplay<T>(this T element, DisplayStyle display)
            where T : VisualElement
        {
            element.style.SetDisplay(display);
            return element;
        }
        
        public static T SetSize<T>(this T element, StyleLength? width = null, StyleLength? height = null)
            where T : VisualElement
        {
            element.style.SetSize(width, height);
            return element;
        }
    }
}