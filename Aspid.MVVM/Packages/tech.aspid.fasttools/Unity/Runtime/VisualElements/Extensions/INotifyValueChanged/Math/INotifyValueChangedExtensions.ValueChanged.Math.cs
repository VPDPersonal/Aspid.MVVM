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
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int2>> callback)
            where T : INotifyValueChanged<int2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int2>> callback)
            where T : INotifyValueChanged<int2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int3>> callback)
            where T : INotifyValueChanged<int3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int3>> callback)
            where T : INotifyValueChanged<int3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int4>> callback)
            where T : INotifyValueChanged<int4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int4>> callback)
            where T : INotifyValueChanged<int4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int2x2>> callback)
            where T : INotifyValueChanged<int2x2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int2x2>> callback)
            where T : INotifyValueChanged<int2x2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int2x3>> callback)
            where T : INotifyValueChanged<int2x3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int2x3>> callback)
            where T : INotifyValueChanged<int2x3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int2x4>> callback)
            where T : INotifyValueChanged<int2x4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int2x4>> callback)
            where T : INotifyValueChanged<int2x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int3x2>> callback)
            where T : INotifyValueChanged<int3x2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int3x2>> callback)
            where T : INotifyValueChanged<int3x2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int3x3>> callback)
            where T : INotifyValueChanged<int3x3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int3x3>> callback)
            where T : INotifyValueChanged<int3x3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int3x4>> callback)
            where T : INotifyValueChanged<int3x4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int3x4>> callback)
            where T : INotifyValueChanged<int3x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int4x2>> callback)
            where T : INotifyValueChanged<int4x2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int4x2>> callback)
            where T : INotifyValueChanged<int4x2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int4x3>> callback)
            where T : INotifyValueChanged<int4x3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int4x3>> callback)
            where T : INotifyValueChanged<int4x3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int4x4>> callback)
            where T : INotifyValueChanged<int4x4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int4x4>> callback)
            where T : INotifyValueChanged<int4x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Unity.Mathematics.Bool
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2>> callback)
            where T : INotifyValueChanged<bool2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2>> callback)
            where T : INotifyValueChanged<bool2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3>> callback)
            where T : INotifyValueChanged<bool3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3>> callback)
            where T : INotifyValueChanged<bool3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4>> callback)
            where T : INotifyValueChanged<bool4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4>> callback)
            where T : INotifyValueChanged<bool4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2x2>> callback)
            where T : INotifyValueChanged<bool2x2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2x2>> callback)
            where T : INotifyValueChanged<bool2x2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2x3>> callback)
            where T : INotifyValueChanged<bool2x3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2x3>> callback)
            where T : INotifyValueChanged<bool2x3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2x4>> callback)
            where T : INotifyValueChanged<bool2x4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2x4>> callback)
            where T : INotifyValueChanged<bool2x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3x2>> callback)
            where T : INotifyValueChanged<bool3x2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3x2>> callback)
            where T : INotifyValueChanged<bool3x2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3x3>> callback)
            where T : INotifyValueChanged<bool3x3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3x3>> callback)
            where T : INotifyValueChanged<bool3x3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3x4>> callback)
            where T : INotifyValueChanged<bool3x4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3x4>> callback)
            where T : INotifyValueChanged<bool3x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4x2>> callback)
            where T : INotifyValueChanged<bool4x2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4x2>> callback)
            where T : INotifyValueChanged<bool4x2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4x3>> callback)
            where T : INotifyValueChanged<bool4x3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4x3>> callback)
            where T : INotifyValueChanged<bool4x3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4x4>> callback)
            where T : INotifyValueChanged<bool4x4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4x4>> callback)
            where T : INotifyValueChanged<bool4x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Unity.Mathematics.Float
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float2>> callback)
            where T : INotifyValueChanged<float2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float2>> callback)
            where T : INotifyValueChanged<float2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float3>> callback)
            where T : INotifyValueChanged<float3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float3>> callback)
            where T : INotifyValueChanged<float3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float4>> callback)
            where T : INotifyValueChanged<float4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float4>> callback)
            where T : INotifyValueChanged<float4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float2x2>> callback)
            where T : INotifyValueChanged<float2x2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float2x2>> callback)
            where T : INotifyValueChanged<float2x2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float2x3>> callback)
            where T : INotifyValueChanged<float2x3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float2x3>> callback)
            where T : INotifyValueChanged<float2x3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float2x4>> callback)
            where T : INotifyValueChanged<float2x4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float2x4>> callback)
            where T : INotifyValueChanged<float2x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float3x2>> callback)
            where T : INotifyValueChanged<float3x2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float3x2>> callback)
            where T : INotifyValueChanged<float3x2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float3x3>> callback)
            where T : INotifyValueChanged<float3x3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float3x3>> callback)
            where T : INotifyValueChanged<float3x3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float3x4>> callback)
            where T : INotifyValueChanged<float3x4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float3x4>> callback)
            where T : INotifyValueChanged<float3x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float4x2>> callback)
            where T : INotifyValueChanged<float4x2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float4x2>> callback)
            where T : INotifyValueChanged<float4x2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float4x3>> callback)
            where T : INotifyValueChanged<float4x3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float4x3>> callback)
            where T : INotifyValueChanged<float4x3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float4x4>> callback)
            where T : INotifyValueChanged<float4x4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float4x4>> callback)
            where T : INotifyValueChanged<float4x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Unity.Mathematics.Double
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<double2>> callback)
            where T : INotifyValueChanged<double2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<double2>> callback)
            where T : INotifyValueChanged<double2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<double3>> callback)
            where T : INotifyValueChanged<double3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<double3>> callback)
            where T : INotifyValueChanged<double3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<double4>> callback)
            where T : INotifyValueChanged<double4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<double4>> callback)
            where T : INotifyValueChanged<double4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<double2x2>> callback)
            where T : INotifyValueChanged<double2x2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<double2x2>> callback)
            where T : INotifyValueChanged<double2x2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<double2x3>> callback)
            where T : INotifyValueChanged<double2x3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<double2x3>> callback)
            where T : INotifyValueChanged<double2x3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<double2x4>> callback)
            where T : INotifyValueChanged<double2x4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<double2x4>> callback)
            where T : INotifyValueChanged<double2x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<double3x2>> callback)
            where T : INotifyValueChanged<double3x2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<double3x2>> callback)
            where T : INotifyValueChanged<double3x2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<double3x3>> callback)
            where T : INotifyValueChanged<double3x3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<double3x3>> callback)
            where T : INotifyValueChanged<double3x3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<double3x4>> callback)
            where T : INotifyValueChanged<double3x4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<double3x4>> callback)
            where T : INotifyValueChanged<double3x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<double4x2>> callback)
            where T : INotifyValueChanged<double4x2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<double4x2>> callback)
            where T : INotifyValueChanged<double4x2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<double4x3>> callback)
            where T : INotifyValueChanged<double4x3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<double4x3>> callback)
            where T : INotifyValueChanged<double4x3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<double4x4>> callback)
            where T : INotifyValueChanged<double4x4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<double4x4>> callback)
            where T : INotifyValueChanged<double4x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Unity.Mathematics.UInt
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<uint2>> callback)
            where T : INotifyValueChanged<uint2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<uint2>> callback)
            where T : INotifyValueChanged<uint2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<uint3>> callback)
            where T : INotifyValueChanged<uint3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<uint3>> callback)
            where T : INotifyValueChanged<uint3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<uint4>> callback)
            where T : INotifyValueChanged<uint4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<uint4>> callback)
            where T : INotifyValueChanged<uint4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<uint2x2>> callback)
            where T : INotifyValueChanged<uint2x2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<uint2x2>> callback)
            where T : INotifyValueChanged<uint2x2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<uint2x3>> callback)
            where T : INotifyValueChanged<uint2x3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<uint2x3>> callback)
            where T : INotifyValueChanged<uint2x3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<uint2x4>> callback)
            where T : INotifyValueChanged<uint2x4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<uint2x4>> callback)
            where T : INotifyValueChanged<uint2x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<uint3x2>> callback)
            where T : INotifyValueChanged<uint3x2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<uint3x2>> callback)
            where T : INotifyValueChanged<uint3x2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<uint3x3>> callback)
            where T : INotifyValueChanged<uint3x3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<uint3x3>> callback)
            where T : INotifyValueChanged<uint3x3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<uint3x4>> callback)
            where T : INotifyValueChanged<uint3x4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<uint3x4>> callback)
            where T : INotifyValueChanged<uint3x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<uint4x2>> callback)
            where T : INotifyValueChanged<uint4x2>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<uint4x2>> callback)
            where T : INotifyValueChanged<uint4x2>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<uint4x3>> callback)
            where T : INotifyValueChanged<uint4x3>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<uint4x3>> callback)
            where T : INotifyValueChanged<uint4x3>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<uint4x4>> callback)
            where T : INotifyValueChanged<uint4x4>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<uint4x4>> callback)
            where T : INotifyValueChanged<uint4x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Unity.Mathematics.Quaternion
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<quaternion>> callback)
            where T : INotifyValueChanged<quaternion>
        {
            element.RegisterValueChangedCallback(callback);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<quaternion>> callback)
            where T : INotifyValueChanged<quaternion>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
    }
}
#endif
