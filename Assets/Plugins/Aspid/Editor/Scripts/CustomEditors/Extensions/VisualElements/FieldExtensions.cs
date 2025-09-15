using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.CustomEditors
{
    public static class FieldExtensions
    {
        public static T SetLabel<T, TValueType>(this T element, string label)
            where T : BaseField<TValueType>
        {
            element.label = label;
            return element;
        }
        
        public static T SetValue<T, TValueType>(this T element, TValueType value)
            where T : BaseField<TValueType>
        {
            element.value = value;
            return element;
        }
    }
}