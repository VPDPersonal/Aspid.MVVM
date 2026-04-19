using System;
using UnityEngine;
using UnityEngine.UIElements;
#if ASPID_FASTTOOLS_UNITY_MATHEMATICS_INTEGRATION
using Unity.Mathematics;
#endif
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
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int>> value)
            where T : INotifyValueChanged<int>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int>> value)
            where T : INotifyValueChanged<int>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<uint>> value)
            where T : INotifyValueChanged<uint>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<uint>> value)
            where T : INotifyValueChanged<uint>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<nint>> value)
            where T : INotifyValueChanged<nint>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<nint>> value)
            where T : INotifyValueChanged<nint>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<nuint>> value)
            where T : INotifyValueChanged<nuint>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<nuint>> value)
            where T : INotifyValueChanged<nuint>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Long
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<long>> value)
            where T : INotifyValueChanged<long>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<long>> value)
            where T : INotifyValueChanged<long>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<ulong>> value)
            where T : INotifyValueChanged<ulong>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<ulong>> value)
            where T : INotifyValueChanged<ulong>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Byte
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<byte>> value)
            where T : INotifyValueChanged<byte>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<byte>> value)
            where T : INotifyValueChanged<byte>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<sbyte>> value)
            where T : INotifyValueChanged<sbyte>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<sbyte>> value)
            where T : INotifyValueChanged<sbyte>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Bool
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool>> value)
            where T : INotifyValueChanged<bool>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool>> value)
            where T : INotifyValueChanged<bool>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Char
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<char>> value)
            where T : INotifyValueChanged<char>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<char>> value)
            where T : INotifyValueChanged<char>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Rect
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Rect>> value)
            where T : INotifyValueChanged<Rect>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Rect>> value)
            where T : INotifyValueChanged<Rect>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<RectInt>> value)
            where T : INotifyValueChanged<RectInt>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<RectInt>> value)
            where T : INotifyValueChanged<RectInt>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
                
        #region Enum
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Enum>> value)
            where T : INotifyValueChanged<Enum>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Enum>> value)
            where T : INotifyValueChanged<Enum>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Guid
#if UNITY_6000_4_OR_NEWER
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<GUID>> value)
            where T : INotifyValueChanged<GUID>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<GUID>> value)
            where T : INotifyValueChanged<GUID>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
#endif
        #endregion
        
        #region Color
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Color>> value)
            where T : INotifyValueChanged<Color>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Color>> value)
            where T : INotifyValueChanged<Color>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Short
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<short>> value)
            where T : INotifyValueChanged<short>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<short>> value)
            where T : INotifyValueChanged<short>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<ushort>> value)
            where T : INotifyValueChanged<ushort>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<ushort>> value)
            where T : INotifyValueChanged<ushort>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Float
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float>> value)
            where T : INotifyValueChanged<float>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float>> value)
            where T : INotifyValueChanged<float>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Double
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<double>> value)
            where T : INotifyValueChanged<double>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<double>> value)
            where T : INotifyValueChanged<double>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region String
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<string>> value)
            where T : INotifyValueChanged<string>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<string>> value)
            where T : INotifyValueChanged<string>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Bounds
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Bounds>> value)
            where T : INotifyValueChanged<Bounds>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Bounds>> value)
            where T : INotifyValueChanged<Bounds>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<BoundsInt>> value)
            where T : INotifyValueChanged<BoundsInt>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<BoundsInt>> value)
            where T : INotifyValueChanged<BoundsInt>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Hash128
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Hash128>> value)
            where T : INotifyValueChanged<Hash128>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Hash128>> value)
            where T : INotifyValueChanged<Hash128>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Decimal
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<decimal>> value)
            where T : INotifyValueChanged<decimal>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<decimal>> value)
            where T : INotifyValueChanged<decimal>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Vector2
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector2>> value)
            where T : INotifyValueChanged<Vector2>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector2>> value)
            where T : INotifyValueChanged<Vector2>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector2Int>> value)
            where T : INotifyValueChanged<Vector2Int>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector2Int>> value)
            where T : INotifyValueChanged<Vector2Int>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Vector3
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector3>> value)
            where T : INotifyValueChanged<Vector3>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector3>> value)
            where T : INotifyValueChanged<Vector3>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector3Int>> value)
            where T : INotifyValueChanged<Vector3Int>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector3Int>> value)
            where T : INotifyValueChanged<Vector3Int>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Vector4
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector4>> value)
            where T : INotifyValueChanged<Vector4>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Vector4>> value)
            where T : INotifyValueChanged<Vector4>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Delegate
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Delegate>> value)
            where T : INotifyValueChanged<Delegate>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Delegate>> value)
            where T : INotifyValueChanged<Delegate>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Gradient
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Gradient>> value)
            where T : INotifyValueChanged<Gradient>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Gradient>> value)
            where T : INotifyValueChanged<Gradient>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Matrix4x4
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Matrix4x4>> value)
            where T : INotifyValueChanged<Matrix4x4>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Matrix4x4>> value)
            where T : INotifyValueChanged<Matrix4x4>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region Quaternion
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Quaternion>> value)
            where T : INotifyValueChanged<Quaternion>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Quaternion>> value)
            where T : INotifyValueChanged<Quaternion>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region System.Object
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<object>> value)
            where T : INotifyValueChanged<object>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }

        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<object>> value)
            where T : INotifyValueChanged<object>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region AnimationCurve
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<AnimationCurve>> value)
            where T : INotifyValueChanged<AnimationCurve>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<AnimationCurve>> value)
            where T : INotifyValueChanged<AnimationCurve>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion
        
        #region UnityEngine.Object
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<Object>> value)
            where T : INotifyValueChanged<Object>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<Object>> value)
            where T : INotifyValueChanged<Object>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        #endregion

        #region Unity.Mathematics.Int
#if ASPID_FASTTOOLS_UNITY_MATHEMATICS_INTEGRATION
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int2>> value)
            where T : INotifyValueChanged<int2>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int2>> value)
            where T : INotifyValueChanged<int2>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int3>> value)
            where T : INotifyValueChanged<int3>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int3>> value)
            where T : INotifyValueChanged<int3>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int4>> value)
            where T : INotifyValueChanged<int4>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int4>> value)
            where T : INotifyValueChanged<int4>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int2x2>> value)
            where T : INotifyValueChanged<int2x2>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int2x2>> value)
            where T : INotifyValueChanged<int2x2>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int2x3>> value)
            where T : INotifyValueChanged<int2x3>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int2x3>> value)
            where T : INotifyValueChanged<int2x3>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int2x4>> value)
            where T : INotifyValueChanged<int2x4>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int2x4>> value)
            where T : INotifyValueChanged<int2x4>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int3x2>> value)
            where T : INotifyValueChanged<int3x2>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int3x2>> value)
            where T : INotifyValueChanged<int3x2>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int3x3>> value)
            where T : INotifyValueChanged<int3x3>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int3x3>> value)
            where T : INotifyValueChanged<int3x3>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int3x4>> value)
            where T : INotifyValueChanged<int3x4>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int3x4>> value)
            where T : INotifyValueChanged<int3x4>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>element
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int4x2>> value)
            where T : INotifyValueChanged<int4x2>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int4x2>> value)
            where T : INotifyValueChanged<int4x2>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>element
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int4x3>> value)
            where T : INotifyValueChanged<int4x3>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int4x3>> value)
            where T : INotifyValueChanged<int4x3>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<int4x4>> value)
            where T : INotifyValueChanged<int4x4>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<int4x4>> value)
            where T : INotifyValueChanged<int4x4>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
#endif
        #endregion
        
        #region Unity.Mathematics.Bool
#if ASPID_FASTTOOLS_UNITY_MATHEMATICS_INTEGRATION
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2>> value)
            where T : INotifyValueChanged<bool2>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2>> value)
            where T : INotifyValueChanged<bool2>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3>> value)
            where T : INotifyValueChanged<bool3>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3>> value)
            where T : INotifyValueChanged<bool3>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4>> value)
            where T : INotifyValueChanged<bool4>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4>> value)
            where T : INotifyValueChanged<bool4>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2x2>> value)
            where T : INotifyValueChanged<bool2x2>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2x2>> value)
            where T : INotifyValueChanged<bool2x2>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2x3>> value)
            where T : INotifyValueChanged<bool2x3>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2x3>> value)
            where T : INotifyValueChanged<bool2x3>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2x4>> value)
            where T : INotifyValueChanged<bool2x4>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool2x4>> value)
            where T : INotifyValueChanged<bool2x4>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3x2>> value)
            where T : INotifyValueChanged<bool3x2>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3x2>> value)
            where T : INotifyValueChanged<bool3x2>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3x3>> value)
            where T : INotifyValueChanged<bool3x3>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3x3>> value)
            where T : INotifyValueChanged<bool3x3>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3x4>> value)
            where T : INotifyValueChanged<bool3x4>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool3x4>> value)
            where T : INotifyValueChanged<bool3x4>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4x2>> value)
            where T : INotifyValueChanged<bool4x2>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4x2>> value)
            where T : INotifyValueChanged<bool4x2>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4x3>> value)
            where T : INotifyValueChanged<bool4x3>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4x3>> value)
            where T : INotifyValueChanged<bool4x3>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4x4>> value)
            where T : INotifyValueChanged<bool4x4>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<bool4x4>> value)
            where T : INotifyValueChanged<bool4x4>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
#endif
        #endregion
        
        #region Unity.Mathematics.Float
#if ASPID_FASTTOOLS_UNITY_MATHEMATICS_INTEGRATION
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float2>> value)
            where T : INotifyValueChanged<float2>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float2>> value)
            where T : INotifyValueChanged<float2>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float3>> value)
            where T : INotifyValueChanged<float3>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float3>> value)
            where T : INotifyValueChanged<float3>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float4>> value)
            where T : INotifyValueChanged<float4>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float4>> value)
            where T : INotifyValueChanged<float4>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float2x2>> value)
            where T : INotifyValueChanged<float2x2>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float2x2>> value)
            where T : INotifyValueChanged<float2x2>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float2x3>> value)
            where T : INotifyValueChanged<float2x3>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float2x3>> value)
            where T : INotifyValueChanged<float2x3>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float2x4>> value)
            where T : INotifyValueChanged<float2x4>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float2x4>> value)
            where T : INotifyValueChanged<float2x4>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float3x2>> value)
            where T : INotifyValueChanged<float3x2>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float3x2>> value)
            where T : INotifyValueChanged<float3x2>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float3x3>> value)
            where T : INotifyValueChanged<float3x3>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float3x3>> value)
            where T : INotifyValueChanged<float3x3>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float3x4>> value)
            where T : INotifyValueChanged<float3x4>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float3x4>> value)
            where T : INotifyValueChanged<float3x4>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float4x2>> value)
            where T : INotifyValueChanged<float4x2>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float4x2>> value)
            where T : INotifyValueChanged<float4x2>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float4x3>> value)
            where T : INotifyValueChanged<float4x3>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float4x3>> value)
            where T : INotifyValueChanged<float4x3>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<float4x4>> value)
            where T : INotifyValueChanged<float4x4>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<float4x4>> value)
            where T : INotifyValueChanged<float4x4>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
#endif
        #endregion
        
        #region Unity.Mathematics.Quaternion
#if ASPID_FASTTOOLS_UNITY_MATHEMATICS_INTEGRATION
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<ChangeEvent<quaternion>> value)
            where T : INotifyValueChanged<quaternion>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<ChangeEvent<quaternion>> value)
            where T : INotifyValueChanged<quaternion>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
#endif
        #endregion
        
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static TField AddValueChanged<TField, TValue>(this TField element, EventCallback<ChangeEvent<TValue>> value)
            where TField : INotifyValueChanged<TValue>
        {
            element.RegisterValueChangedCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static TField RemoveValueChanged<TField, TValue>(this TField element, EventCallback<ChangeEvent<TValue>> value)
            where TField : INotifyValueChanged<TValue>
        {
            element.UnregisterValueChangedCallback(value);
            return element;
        }
    }
}
