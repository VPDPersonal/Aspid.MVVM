using UnityEngine.UIElements;

// TODO Aspid.FastTools
// ReSharper disable CheckNamespace
namespace Aspid.FastTools
{
    public static class TextElementExtensions
    {
        public static T SetText<T>(this T textElement, string text)
            where T : TextElement
        {
            textElement.text = text;
            return textElement;
        }
    }
}