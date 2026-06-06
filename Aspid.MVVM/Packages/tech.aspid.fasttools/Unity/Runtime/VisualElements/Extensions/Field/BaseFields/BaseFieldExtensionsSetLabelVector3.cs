using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class BaseFieldExtensionsSetLabelVector3
    {
        /// <summary>
        /// Sets the label of the field via <see cref="BaseField{TValueType}.label"/>.
        /// </summary>
        /// <typeparam name="T">The field type.</typeparam>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The label text to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLabel<T>(this T element, string value)
            where T : BaseField<Vector3>
        {
            element.label = value;
            return element;
        }
    }
}
