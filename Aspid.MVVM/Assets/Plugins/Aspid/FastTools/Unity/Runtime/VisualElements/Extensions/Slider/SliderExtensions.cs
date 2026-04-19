using System;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class SliderExtensions
    {
        #region Int
        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.lowValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The low value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLowValue<T>(this T element, int value)
            where T : BaseSlider<int>
        {
            element.lowValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.highValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The high value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHighValue<T>(this T element, int value)
            where T : BaseSlider<int>
        {
            element.highValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.lowValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The low value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLowValue<T>(this T element, uint value)
            where T : BaseSlider<uint>
        {
            element.lowValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.highValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The high value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHighValue<T>(this T element, uint value)
            where T : BaseSlider<uint>
        {
            element.highValue = value;
            return element;
        }
        #endregion

        #region Long
        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.lowValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The low value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLowValue<T>(this T element, long value)
            where T : BaseSlider<long>
        {
            element.lowValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.highValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The high value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHighValue<T>(this T element, long value)
            where T : BaseSlider<long>
        {
            element.highValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.lowValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The low value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLowValue<T>(this T element, ulong value)
            where T : BaseSlider<ulong>
        {
            element.lowValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.highValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The high value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHighValue<T>(this T element, ulong value)
            where T : BaseSlider<ulong>
        {
            element.highValue = value;
            return element;
        }
        #endregion

        #region Byte
        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.lowValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The low value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLowValue<T>(this T element, byte value)
            where T : BaseSlider<byte>
        {
            element.lowValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.highValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The high value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHighValue<T>(this T element, byte value)
            where T : BaseSlider<byte>
        {
            element.highValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.lowValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The low value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLowValue<T>(this T element, sbyte value)
            where T : BaseSlider<sbyte>
        {
            element.lowValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.highValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The high value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHighValue<T>(this T element, sbyte value)
            where T : BaseSlider<sbyte>
        {
            element.highValue = value;
            return element;
        }
        #endregion

        #region Short
        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.lowValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The low value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLowValue<T>(this T element, short value)
            where T : BaseSlider<short>
        {
            element.lowValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.highValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The high value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHighValue<T>(this T element, short value)
            where T : BaseSlider<short>
        {
            element.highValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.lowValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The low value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLowValue<T>(this T element, ushort value)
            where T : BaseSlider<ushort>
        {
            element.lowValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.highValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The high value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHighValue<T>(this T element, ushort value)
            where T : BaseSlider<ushort>
        {
            element.highValue = value;
            return element;
        }
        #endregion

        #region Float
        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.lowValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The low value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLowValue<T>(this T element, float value)
            where T : BaseSlider<float>
        {
            element.lowValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.highValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The high value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHighValue<T>(this T element, float value)
            where T : BaseSlider<float>
        {
            element.highValue = value;
            return element;
        }
        #endregion

        #region Double
        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.lowValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The low value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLowValue<T>(this T element, double value)
            where T : BaseSlider<double>
        {
            element.lowValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.highValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The high value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHighValue<T>(this T element, double value)
            where T : BaseSlider<double>
        {
            element.highValue = value;
            return element;
        }
        #endregion

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.fill"/> property controlling whether the track is filled up to the current value and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether the track is filled up to the current value.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetFill<T, TValue>(this T element, bool value)
            where T : BaseSlider<TValue>
            where TValue : IComparable<TValue>
        {
            element.fill = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.inverted"/> property reversing the direction of the element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether the slider direction is inverted.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetInverted<T, TValue>(this T element, bool value)
            where T : BaseSlider<TValue>
            where TValue : IComparable<TValue>
        {
            element.inverted = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.pageSize"/> property controlling how much the value changes per page step and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The page size to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetPageSize<T, TValue>(this T element, float value)
            where T : BaseSlider<TValue>
            where TValue : IComparable<TValue>
        {
            element.pageSize = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.lowValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The low value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLowValue<T, TValue>(this T element, TValue value)
            where T : BaseSlider<TValue>
            where TValue : IComparable<TValue>
        {
            element.lowValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.highValue"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The high value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHighValue<T, TValue>(this T element, TValue value)
            where T : BaseSlider<TValue>
            where TValue : IComparable<TValue>
        {
            element.highValue = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.showInputField"/> property controlling whether a numeric input field is shown alongside the element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether to show a numeric input field.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetShowInputField<T, TValue>(this T element, bool value)
            where T : BaseSlider<TValue>
            where TValue : IComparable<TValue>
        {
            element.showInputField = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseSlider{TValueType}.direction"/> property controlling the orientation of the element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The slider direction to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetDirection<T, TValue>(this T element, SliderDirection value)
            where T : BaseSlider<TValue>
            where TValue : IComparable<TValue>
        {
            element.direction = value;
            return element;
        }
    }
}
