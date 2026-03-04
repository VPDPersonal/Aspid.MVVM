using UnityEngine.UIElements;

// TODO Aspid.FastTools
// ReSharper disable CheckNamespace
namespace Aspid.FastTools
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