using UnityEngine.UIElements;

namespace Aspid.CustomEditors.Extensions.VisualElements
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