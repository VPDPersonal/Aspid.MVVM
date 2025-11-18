using UnityEngine.UIElements;

// TODO Aspid.UnityFastTools
// ReSharper disable CheckNamespace
namespace Aspid.UnityFastTools
{
    public static class FoldoutExtensions
    {
        public static T SetText<T>(this T foldout, string text)
            where T : Foldout
        {
            foldout.text = text;
            return foldout;
        }

        public static T SetValue<T>(this T foldout, bool value)
            where T : Foldout
        {
            foldout.value = value;
            return foldout;
        }
    }
}