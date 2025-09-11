using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.CustomEditors
{
    public static class FoldoutExtensions
    {
        public static T SetText<T>(this T element, string text)
            where T : Foldout
        {
            element.text = text;
            return element;
        }

        public static T SetValue<T>(this T element, bool value)
            where T : Foldout
        {
            element.value = value;
            return element;
        }
    }
}