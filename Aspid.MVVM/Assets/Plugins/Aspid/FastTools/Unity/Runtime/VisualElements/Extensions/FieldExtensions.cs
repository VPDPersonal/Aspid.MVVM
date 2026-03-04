using UnityEngine.UIElements;

// TODO Aspid.FastTools
// ReSharper disable CheckNamespace
namespace Aspid.FastTools
{
    public static class FieldExtensions
    {
        public static T SetLabel<T, TValueType>(this T field, string label)
            where T : BaseField<TValueType>
        {
            field.label = label;
            return field;
        }
        
        public static T SetValue<T, TValueType>(this T field, TValueType value)
            where T : BaseField<TValueType>
        {
            field.value = value;
            return field;
        }
    }
}