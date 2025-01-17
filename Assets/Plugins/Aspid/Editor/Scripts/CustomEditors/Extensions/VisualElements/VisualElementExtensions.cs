using UnityEngine.UIElements;

namespace Aspid.CustomEditors
{
    public static class VisualElementExtensions
    {
        public static T SetName<T>(this T element, string name)
            where T : VisualElement
        {
            element.name = name;
            return element;
        }
        
        public static T SetVisible<T>(this T element, bool visible)
            where T : VisualElement
        {
            element.visible = visible;
            return element;
        }

        public static T AddChild<T>(this T element, VisualElement childElement)
            where T : VisualElement
        {
            element.Add(childElement);
            return element;
        }
    }
}