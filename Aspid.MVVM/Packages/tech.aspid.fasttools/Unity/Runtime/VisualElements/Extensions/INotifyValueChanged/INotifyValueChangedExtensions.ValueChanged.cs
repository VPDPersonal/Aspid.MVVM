using System;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static partial class INotifyValueChangedExtensions
    {
        #region Int
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int>> callback)
            where T : INotifyValueChanged<int>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int>> callback)
            where T : INotifyValueChanged<int>
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
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<uint>> callback)
            where T : INotifyValueChanged<uint>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<uint>> callback)
            where T : INotifyValueChanged<uint>
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
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<nint>> callback)
            where T : INotifyValueChanged<nint>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<nint>> callback)
            where T : INotifyValueChanged<nint>
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
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<nuint>> callback)
            where T : INotifyValueChanged<nuint>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<nuint>> callback)
            where T : INotifyValueChanged<nuint>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Long
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<long>> callback)
            where T : INotifyValueChanged<long>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<long>> callback)
            where T : INotifyValueChanged<long>
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
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<ulong>> callback)
            where T : INotifyValueChanged<ulong>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<ulong>> callback)
            where T : INotifyValueChanged<ulong>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Byte
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<byte>> callback)
            where T : INotifyValueChanged<byte>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<byte>> callback)
            where T : INotifyValueChanged<byte>
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
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<sbyte>> callback)
            where T : INotifyValueChanged<sbyte>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<sbyte>> callback)
            where T : INotifyValueChanged<sbyte>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Bool
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool>> callback)
            where T : INotifyValueChanged<bool>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool>> callback)
            where T : INotifyValueChanged<bool>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Char
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<char>> callback)
            where T : INotifyValueChanged<char>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<char>> callback)
            where T : INotifyValueChanged<char>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Rect
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Rect>> callback)
            where T : INotifyValueChanged<Rect>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Rect>> callback)
            where T : INotifyValueChanged<Rect>
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
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<RectInt>> callback)
            where T : INotifyValueChanged<RectInt>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<RectInt>> callback)
            where T : INotifyValueChanged<RectInt>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
                
        #region Enum
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Enum>> callback)
            where T : INotifyValueChanged<Enum>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Enum>> callback)
            where T : INotifyValueChanged<Enum>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Guid
#if UNITY_6000_4_OR_NEWER
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<GUID>> callback)
            where T : INotifyValueChanged<GUID>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<GUID>> callback)
            where T : INotifyValueChanged<GUID>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
#endif
        #endregion
        
        #region Color
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Color>> callback)
            where T : INotifyValueChanged<Color>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Color>> callback)
            where T : INotifyValueChanged<Color>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Short
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<short>> callback)
            where T : INotifyValueChanged<short>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<short>> callback)
            where T : INotifyValueChanged<short>
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
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<ushort>> callback)
            where T : INotifyValueChanged<ushort>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<ushort>> callback)
            where T : INotifyValueChanged<ushort>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Float
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float>> callback)
            where T : INotifyValueChanged<float>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float>> callback)
            where T : INotifyValueChanged<float>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Double
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<double>> callback)
            where T : INotifyValueChanged<double>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<double>> callback)
            where T : INotifyValueChanged<double>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region String
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<string>> callback)
            where T : INotifyValueChanged<string>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<string>> callback)
            where T : INotifyValueChanged<string>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Bounds
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Bounds>> callback)
            where T : INotifyValueChanged<Bounds>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Bounds>> callback)
            where T : INotifyValueChanged<Bounds>
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
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<BoundsInt>> callback)
            where T : INotifyValueChanged<BoundsInt>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<BoundsInt>> callback)
            where T : INotifyValueChanged<BoundsInt>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Hash128
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Hash128>> callback)
            where T : INotifyValueChanged<Hash128>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Hash128>> callback)
            where T : INotifyValueChanged<Hash128>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Decimal
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<decimal>> callback)
            where T : INotifyValueChanged<decimal>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<decimal>> callback)
            where T : INotifyValueChanged<decimal>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Vector2
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector2>> callback)
            where T : INotifyValueChanged<Vector2>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector2>> callback)
            where T : INotifyValueChanged<Vector2>
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
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector2Int>> callback)
            where T : INotifyValueChanged<Vector2Int>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector2Int>> callback)
            where T : INotifyValueChanged<Vector2Int>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Vector3
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector3>> callback)
            where T : INotifyValueChanged<Vector3>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector3>> callback)
            where T : INotifyValueChanged<Vector3>
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
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector3Int>> callback)
            where T : INotifyValueChanged<Vector3Int>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector3Int>> callback)
            where T : INotifyValueChanged<Vector3Int>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Vector4
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector4>> callback)
            where T : INotifyValueChanged<Vector4>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector4>> callback)
            where T : INotifyValueChanged<Vector4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Delegate
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Delegate>> callback)
            where T : INotifyValueChanged<Delegate>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Delegate>> callback)
            where T : INotifyValueChanged<Delegate>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Gradient
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Gradient>> callback)
            where T : INotifyValueChanged<Gradient>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Gradient>> callback)
            where T : INotifyValueChanged<Gradient>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Matrix4x4
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Matrix4x4>> callback)
            where T : INotifyValueChanged<Matrix4x4>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Matrix4x4>> callback)
            where T : INotifyValueChanged<Matrix4x4>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region Quaternion
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Quaternion>> callback)
            where T : INotifyValueChanged<Quaternion>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Quaternion>> callback)
            where T : INotifyValueChanged<Quaternion>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region System.Object
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<object>> callback)
            where T : INotifyValueChanged<object>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<object>> callback)
            where T : INotifyValueChanged<object>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region AnimationCurve
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<AnimationCurve>> callback)
            where T : INotifyValueChanged<AnimationCurve>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<AnimationCurve>> callback)
            where T : INotifyValueChanged<AnimationCurve>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        #region UnityEngine.Object
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Object>> callback)
            where T : INotifyValueChanged<Object>
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
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Object>> callback)
            where T : INotifyValueChanged<Object>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
        #endregion
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static TField AddValueChanged<TField, TValue>(this TField element, EventCallback<ChangeEvent<TValue>> callback)
            where TField : INotifyValueChanged<TValue>
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
        public static TField RemoveValueChanged<TField, TValue>(this TField element, EventCallback<ChangeEvent<TValue>> callback)
            where TField : INotifyValueChanged<TValue>
        {
            element.UnregisterValueChangedCallback(callback);
            return element;
        }
    }
}
