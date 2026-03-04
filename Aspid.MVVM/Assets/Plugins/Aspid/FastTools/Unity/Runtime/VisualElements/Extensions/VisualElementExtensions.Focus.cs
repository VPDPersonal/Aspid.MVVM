using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools
{
    public static partial class VisualElementExtensions
    {
        public static T SetFocus<T>(this T element)
            where T : VisualElement
        {
            element.Focus();
            return element;
        }
        
        public static bool IsFocus(this VisualElement element) => 
            element.focusController.focusedElement == element;
    }
}