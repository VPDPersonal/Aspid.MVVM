#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.CustomEditors
{
    public static class ElementsExtensions
    {
        public static T AddHeader<T>(this T element, Object obj, string iconPath)
            where T : VisualElement
        {
            element.AddChild(Elements.CreateHeader(obj, iconPath));
            return element;
        }

        public static T AddContainer<T>(this T element, StyleColor color, string? name = null)
            where T : VisualElement
        {
            element.AddChild(Elements.CreateContainer(color, name));
            return element;
        }
        
        public static T AddTitle<T>(this T element, StyleColor color, string text, string? name = null)
            where T : VisualElement
        {
            element.AddChild(Elements.CreateTitle(color, text, name));
            return element;
        }
        
        public static T AddHelpBox<T>(this T element, string text, HelpBoxMessageType type, string? name = "HelpBox")
            where T : VisualElement
        {
            element.AddChild(Elements.CreateHelpBox(text, type, name));
            return element;
        }
    }
}