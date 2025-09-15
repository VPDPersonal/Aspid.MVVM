using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.CustomEditors
{
    public static class TextElementExtensions
    {
        public static T SetText<T>(this T element, string text)
            where T : TextElement
        {
            element.text = text;
            return element;
        }
    }
}