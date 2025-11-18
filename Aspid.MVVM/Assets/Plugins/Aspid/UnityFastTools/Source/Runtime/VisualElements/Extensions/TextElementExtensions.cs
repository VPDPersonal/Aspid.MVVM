using UnityEngine.UIElements;

// TODO Aspid.UnityFastTools
// ReSharper disable CheckNamespace
namespace Aspid.UnityFastTools
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