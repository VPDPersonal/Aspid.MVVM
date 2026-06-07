using UnityEngine;
using UnityEngine.UIElements;

namespace Aspid.FastTools.UIElements
{
    public static partial class BaseFieldExtensionsSetLabelQuaternion
    {
        /// <summary>
        /// Sets the label of the field via <see cref="BaseField{TValueType}.label"/>.
        /// </summary>
        /// <typeparam name="T">The field type.</typeparam>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The label text to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLabel<T>(this T element, string value)
            where T : BaseField<Quaternion>
        {
            element.label = value;
            return element;
        }
    }
}
