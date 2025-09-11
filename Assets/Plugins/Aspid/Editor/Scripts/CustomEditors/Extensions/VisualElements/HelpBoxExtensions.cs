using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.CustomEditors
{
    public static class HelpBoxExtensions
    {
        public static T SetHelpBoxFontSize<T>(this T helpBox, int size)
            where T : HelpBox
        {
            helpBox.Q<Label>().SetFontSize(size);
            return helpBox;
        }
    }
}