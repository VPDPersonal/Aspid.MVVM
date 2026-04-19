using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class BaseFieldExtensions
    {
        /// <summary>
        /// Sets the <see cref="BaseField{TValueType}.label"/> property displayed next to the field and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The label text to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static TField SetLabel<TField, TValue>(this TField element, string value)
            where TField : BaseField<TValue>
        {
            element.label = value;
            return element;
        }
    }
}
