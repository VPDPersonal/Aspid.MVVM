using System;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static partial class INotifyValueChangedExtensions
    {
        #region Int
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, int value, bool notify = true)
            where T : INotifyValueChanged<int>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, uint value, bool notify = true)
            where T : INotifyValueChanged<uint>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, nint value, bool notify = true)
            where T : INotifyValueChanged<nint>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, nuint value, bool notify = true)
            where T : INotifyValueChanged<nuint>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion

        #region Long
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, long value, bool notify = true)
            where T : INotifyValueChanged<long>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, ulong value, bool notify = true)
            where T : INotifyValueChanged<ulong>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Byte
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, byte value, bool notify = true)
            where T : INotifyValueChanged<byte>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, sbyte value, bool notify = true)
            where T : INotifyValueChanged<sbyte>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion

        #region Bool
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, bool value, bool notify = true)
            where T : INotifyValueChanged<bool>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Char
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, char value, bool notify = true)
            where T : INotifyValueChanged<char>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Rect
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, Rect value, bool notify = true)
            where T : INotifyValueChanged<Rect>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, RectInt value, bool notify = true)
            where T : INotifyValueChanged<RectInt>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Enum
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, Enum value, bool notify = true)
            where T : INotifyValueChanged<Enum>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Guid
#if UNITY_6000_4_OR_NEWER
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, GUID value, bool notify = true)
            where T : INotifyValueChanged<GUID>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
#endif
        #endregion
        
        #region Color
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, Color value, bool notify = true)
            where T : INotifyValueChanged<Color>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Short
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, short value, bool notify = true)
            where T : INotifyValueChanged<short>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, ushort value, bool notify = true)
            where T : INotifyValueChanged<ushort>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Float
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, float value, bool notify = true)
            where T : INotifyValueChanged<float>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Double
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, double value, bool notify = true)
            where T : INotifyValueChanged<double>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region String
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, string value, bool notify = true)
            where T : INotifyValueChanged<string>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Bounds
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, Bounds value, bool notify = true)
            where T : INotifyValueChanged<Bounds>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, BoundsInt value, bool notify = true)
            where T : INotifyValueChanged<BoundsInt>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Hash128
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, Hash128 value, bool notify = true)
            where T : INotifyValueChanged<Hash128>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Decimal
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, decimal value, bool notify = true)
            where T : INotifyValueChanged<decimal>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion

        #region Vector2
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, Vector2 value, bool notify = true)
            where T : INotifyValueChanged<Vector2>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, Vector2Int value, bool notify = true)
            where T : INotifyValueChanged<Vector2Int>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Vector3
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, Vector3 value, bool notify = true)
            where T : INotifyValueChanged<Vector3>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, Vector3Int value, bool notify = true)
            where T : INotifyValueChanged<Vector3Int>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Vector4
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, Vector4 value, bool notify = true)
            where T : INotifyValueChanged<Vector4>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Delegate
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, Delegate value, bool notify = true)
            where T : INotifyValueChanged<Delegate>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Gradient
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, Gradient value, bool notify = true)
            where T : INotifyValueChanged<Gradient>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Matrix4x4
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, Matrix4x4 value, bool notify = true)
            where T : INotifyValueChanged<Matrix4x4>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Quaternion
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, Quaternion value, bool notify = true)
            where T : INotifyValueChanged<Quaternion>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region System.Object
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, object value, bool notify = true)
            where T : INotifyValueChanged<object>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region AnimationCurve
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, AnimationCurve value, bool notify = true)
            where T : INotifyValueChanged<AnimationCurve>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region UnityEngine.Object
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, Object value, bool notify = true)
            where T : INotifyValueChanged<Object>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T, TValue>(this T element, TValue value, bool notify = true)
            where T : INotifyValueChanged<TValue>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
    }
}
