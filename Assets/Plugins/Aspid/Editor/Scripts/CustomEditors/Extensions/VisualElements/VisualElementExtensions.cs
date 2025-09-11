using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.CustomEditors
{
    public static class VisualElementExtensions
    {
        public static T SetName<T>(this T element, string name)
            where T : VisualElement
        {
            element.name = name;
            return element;
        }
        
        public static T SetVisible<T>(this T element, bool visible)
            where T : VisualElement
        {
            element.visible = visible;
            return element;
        }

        public static T AddChild<T>(this T element, VisualElement childElement)
            where T : VisualElement
        {
            element.Add(childElement);
            return element;
        }
        
        public static T AddChildren<T>(this T element, params VisualElement[] children)
            where T : VisualElement
        {
            foreach (var child in children)
                element.Add(child);
            
            return element;
        }
        
        public static T AddChildren<T>(this T element, IEnumerable<VisualElement> children)
            where T : VisualElement
        {
            foreach (var child in children)
                element.Add(child);
            
            return element;
        }
    }
}