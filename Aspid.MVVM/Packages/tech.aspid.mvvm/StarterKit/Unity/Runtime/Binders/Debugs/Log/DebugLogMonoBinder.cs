using System;
using UnityEngine;
using Conditional = System.Diagnostics.ConditionalAttribute;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IAnyBinder"/> and <see cref="IAnyReverseBinder"/>
    /// that logs all binding events and incoming values to the Unity console.
    /// </summary>
    [GenerateSerializableBinder]
    [BindModeOverride(IsAll = true)]
    [AddComponentMenu("Aspid/MVVM/Binders/Debug/Debug Binder – Log")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Debug/Debug Binder – Log")]
    public sealed partial class DebugLogMonoBinder : MonoBinder, IAnyBinder, IAnyReverseBinder
    {
        [Tooltip("Formats bound values as log messages. Defaults to GenericToStringConverter.")]
        [SerializeReference] private IConverter<object, string> _converter = new GenericToStringConverter<object>();

        /// <summary>
        /// Raised with the bound value when propagating back to the ViewModel in <see cref="BindMode.OneWayToSource"/>.
        /// Both <see langword="add"/> and <see langword="remove"/> operations log the subscriber reference to the Unity console.
        /// </summary>
        public event Action<object> ValueChanged
        {
            add => Log($"Add ValueChanged: {GetMessage(value)}");
            remove => Log($"Remove ValueChanged: {GetMessage(value)}");
        }

        /// <summary>
        /// Logs the received value to the Unity console.
        /// </summary>
        /// <typeparam name="T">The runtime type of the incoming value.</typeparam>
        /// <param name="value">The bound value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue<T>(T value) =>
            Log($"SetValue: {GetMessage(value)}");

        /// <summary>
        /// Writes <paramref name="message"/> to the Unity console, in the Editor and in a development build only.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <remarks>The call and its interpolated string are compiled out entirely in other builds.</remarks>
        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        private static void Log(string message) =>
            Debug.Log(message);

        /// <summary>
        /// Formats <paramref name="value"/> for the console, without assuming it is there.
        /// </summary>
        private string GetMessage(object value)
        {
            if (value is null) return "null";
            return _converter?.Convert(value) ?? value.ToString();
        }
    }
}