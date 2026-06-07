#if ASPID_FASTTOOLS_UNITY_MATHEMATICS_INTEGRATION
using Unity.Mathematics;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static partial class INotifyValueChangedExtensions
    {
        #region Unity.Mathematics.Int
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, int2 value, bool notify = true)
            where T : INotifyValueChanged<int2>
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
        public static T SetValue<T>(this T element, int3 value, bool notify = true)
            where T : INotifyValueChanged<int3>
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
        public static T SetValue<T>(this T element, int4 value, bool notify = true)
            where T : INotifyValueChanged<int4>
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
        public static T SetValue<T>(this T element, int2x2 value, bool notify = true)
            where T : INotifyValueChanged<int2x2>
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
        public static T SetValue<T>(this T element, int2x3 value, bool notify = true)
            where T : INotifyValueChanged<int2x3>
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
        public static T SetValue<T>(this T element, int2x4 value, bool notify = true)
            where T : INotifyValueChanged<int2x4>
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
        public static T SetValue<T>(this T element, int3x2 value, bool notify = true)
            where T : INotifyValueChanged<int3x2>
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
        public static T SetValue<T>(this T element, int3x3 value, bool notify = true)
            where T : INotifyValueChanged<int3x3>
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
        public static T SetValue<T>(this T element, int3x4 value, bool notify = true)
            where T : INotifyValueChanged<int3x4>
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
        public static T SetValue<T>(this T element, int4x2 value, bool notify = true)
            where T : INotifyValueChanged<int4x2>
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
        public static T SetValue<T>(this T element, int4x3 value, bool notify = true)
            where T : INotifyValueChanged<int4x3>
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
        public static T SetValue<T>(this T element, int4x4 value, bool notify = true)
            where T : INotifyValueChanged<int4x4>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Unity.Mathematics.Bool
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, bool2 value, bool notify = true)
            where T : INotifyValueChanged<bool2>
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
        public static T SetValue<T>(this T element, bool3 value, bool notify = true)
            where T : INotifyValueChanged<bool3>
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
        public static T SetValue<T>(this T element, bool4 value, bool notify = true)
            where T : INotifyValueChanged<bool4>
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
        public static T SetValue<T>(this T element, bool2x2 value, bool notify = true)
            where T : INotifyValueChanged<bool2x2>
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
        public static T SetValue<T>(this T element, bool2x3 value, bool notify = true)
            where T : INotifyValueChanged<bool2x3>
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
        public static T SetValue<T>(this T element, bool2x4 value, bool notify = true)
            where T : INotifyValueChanged<bool2x4>
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
        public static T SetValue<T>(this T element, bool3x2 value, bool notify = true)
            where T : INotifyValueChanged<bool3x2>
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
        public static T SetValue<T>(this T element, bool3x3 value, bool notify = true)
            where T : INotifyValueChanged<bool3x3>
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
        public static T SetValue<T>(this T element, bool3x4 value, bool notify = true)
            where T : INotifyValueChanged<bool3x4>
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
        public static T SetValue<T>(this T element, bool4x2 value, bool notify = true)
            where T : INotifyValueChanged<bool4x2>
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
        public static T SetValue<T>(this T element, bool4x3 value, bool notify = true)
            where T : INotifyValueChanged<bool4x3>
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
        public static T SetValue<T>(this T element, bool4x4 value, bool notify = true)
            where T : INotifyValueChanged<bool4x4>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Unity.Mathematics.Float
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, float2 value, bool notify = true)
            where T : INotifyValueChanged<float2>
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
        public static T SetValue<T>(this T element, float3 value, bool notify = true)
            where T : INotifyValueChanged<float3>
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
        public static T SetValue<T>(this T element, float4 value, bool notify = true)
            where T : INotifyValueChanged<float4>
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
        public static T SetValue<T>(this T element, float2x2 value, bool notify = true)
            where T : INotifyValueChanged<float2x2>
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
        public static T SetValue<T>(this T element, float2x3 value, bool notify = true)
            where T : INotifyValueChanged<float2x3>
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
        public static T SetValue<T>(this T element, float2x4 value, bool notify = true)
            where T : INotifyValueChanged<float2x4>
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
        public static T SetValue<T>(this T element, float3x2 value, bool notify = true)
            where T : INotifyValueChanged<float3x2>
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
        public static T SetValue<T>(this T element, float3x3 value, bool notify = true)
            where T : INotifyValueChanged<float3x3>
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
        public static T SetValue<T>(this T element, float3x4 value, bool notify = true)
            where T : INotifyValueChanged<float3x4>
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
        public static T SetValue<T>(this T element, float4x2 value, bool notify = true)
            where T : INotifyValueChanged<float4x2>
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
        public static T SetValue<T>(this T element, float4x3 value, bool notify = true)
            where T : INotifyValueChanged<float4x3>
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
        public static T SetValue<T>(this T element, float4x4 value, bool notify = true)
            where T : INotifyValueChanged<float4x4>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Unity.Mathematics.Double
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, double2 value, bool notify = true)
            where T : INotifyValueChanged<double2>
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
        public static T SetValue<T>(this T element, double3 value, bool notify = true)
            where T : INotifyValueChanged<double3>
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
        public static T SetValue<T>(this T element, double4 value, bool notify = true)
            where T : INotifyValueChanged<double4>
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
        public static T SetValue<T>(this T element, double2x2 value, bool notify = true)
            where T : INotifyValueChanged<double2x2>
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
        public static T SetValue<T>(this T element, double2x3 value, bool notify = true)
            where T : INotifyValueChanged<double2x3>
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
        public static T SetValue<T>(this T element, double2x4 value, bool notify = true)
            where T : INotifyValueChanged<double2x4>
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
        public static T SetValue<T>(this T element, double3x2 value, bool notify = true)
            where T : INotifyValueChanged<double3x2>
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
        public static T SetValue<T>(this T element, double3x3 value, bool notify = true)
            where T : INotifyValueChanged<double3x3>
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
        public static T SetValue<T>(this T element, double3x4 value, bool notify = true)
            where T : INotifyValueChanged<double3x4>
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
        public static T SetValue<T>(this T element, double4x2 value, bool notify = true)
            where T : INotifyValueChanged<double4x2>
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
        public static T SetValue<T>(this T element, double4x3 value, bool notify = true)
            where T : INotifyValueChanged<double4x3>
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
        public static T SetValue<T>(this T element, double4x4 value, bool notify = true)
            where T : INotifyValueChanged<double4x4>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Unity.Mathematics.UInt
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, uint2 value, bool notify = true)
            where T : INotifyValueChanged<uint2>
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
        public static T SetValue<T>(this T element, uint3 value, bool notify = true)
            where T : INotifyValueChanged<uint3>
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
        public static T SetValue<T>(this T element, uint4 value, bool notify = true)
            where T : INotifyValueChanged<uint4>
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
        public static T SetValue<T>(this T element, uint2x2 value, bool notify = true)
            where T : INotifyValueChanged<uint2x2>
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
        public static T SetValue<T>(this T element, uint2x3 value, bool notify = true)
            where T : INotifyValueChanged<uint2x3>
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
        public static T SetValue<T>(this T element, uint2x4 value, bool notify = true)
            where T : INotifyValueChanged<uint2x4>
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
        public static T SetValue<T>(this T element, uint3x2 value, bool notify = true)
            where T : INotifyValueChanged<uint3x2>
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
        public static T SetValue<T>(this T element, uint3x3 value, bool notify = true)
            where T : INotifyValueChanged<uint3x3>
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
        public static T SetValue<T>(this T element, uint3x4 value, bool notify = true)
            where T : INotifyValueChanged<uint3x4>
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
        public static T SetValue<T>(this T element, uint4x2 value, bool notify = true)
            where T : INotifyValueChanged<uint4x2>
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
        public static T SetValue<T>(this T element, uint4x3 value, bool notify = true)
            where T : INotifyValueChanged<uint4x3>
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
        public static T SetValue<T>(this T element, uint4x4 value, bool notify = true)
            where T : INotifyValueChanged<uint4x4>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
        
        #region Unity.Mathematics.Quaternion
        /// <summary>
        /// Sets the element's value. If <paramref name="notify"/> is <see langword="true"/>, a change notification is raised.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="notify">If <see langword="true"/>, raises a change notification.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, quaternion value, bool notify = true)
            where T : INotifyValueChanged<quaternion>
        {
            if (notify) element.value = value;
            else element.SetValueWithoutNotify(value);
            
            return element;
        }
        #endregion
    }
}
#endif
