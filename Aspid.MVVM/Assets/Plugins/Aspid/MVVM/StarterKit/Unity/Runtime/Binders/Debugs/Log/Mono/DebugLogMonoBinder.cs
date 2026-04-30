using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<object, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterObjectToString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IAnyBinder"/> and <see cref="IAnyReverseBinder"/>
    /// that logs all binding events and incoming values to the Unity console.
    /// </summary>
    [BindModeOverride(IsAll = true)]
    [AddComponentMenu("Aspid/MVVM/Binders/Debug/Debug Binder – Log")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Debug/Debug Binder – Log")]
    public sealed partial class DebugLogMonoBinder : MonoBinder, IAnyBinder, IAnyReverseBinder
    {
        [SerializeReferenceDropdown]
        [Tooltip("Converter used to format bound values as log messages. Defaults to ObjectToStringConverter.")]
        [SerializeReference] private Converter _converter = new ObjectToStringConverter();

        /// <summary>
        /// Raised with the bound value when propagating back to the ViewModel in <see cref="BindMode.OneWayToSource"/>.
        /// Both <see langword="add"/> and <see langword="remove"/> operations log the subscriber reference to the Unity console.
        /// </summary>
        public event Action<object> ValueChanged
        {
            add => Debug.Log($"Add ValueChanged: {GetMessage(value)}");
            remove => Debug.Log($"Remove ValueChanged: {GetMessage(value)}");
        }

        /// <summary>
        /// Logs the received value to the Unity console.
        /// </summary>
        /// <typeparam name="T">The runtime type of the incoming value.</typeparam>
        /// <param name="value">The bound value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue<T>(T value) =>
            Debug.Log($"SetValue: {GetMessage(value)}");

        private string GetMessage(object value) =>
            _converter?.Convert(value) ?? value.ToString();
    }
}