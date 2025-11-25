using UnityEngine.UIElements;

// TODO Aspid.UnityFastTools
// ReSharper disable CheckNamespace
namespace Aspid.UnityFastTools
{
    public static class HelpBoxExtensions
    {
        public static T SetFontSize<T>(this T helpBox, StyleLength value)
            where T : HelpBox
        {
            helpBox.Q<Label>().SetFontSize(value);
            return helpBox;
        }

        public static T SetMessageType<T>(this T helpBox, int size, HelpBoxMessageType value)
            where T : HelpBox
        {
            helpBox.messageType = value;
            return helpBox;
        }
    }
}