using System;
using Conditional = System.Diagnostics.ConditionalAttribute;
using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<object, string>;

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
        [Tooltip("Converter used to format bound values as log messages. Defaults to GenericToStringConverter.")]
        [SerializeReference] private Converter _converter = new GenericToStringConverter<object>();

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
        /// <remarks>
        /// Marked with both <see cref="System.Diagnostics.ConditionalAttribute"/> symbols rather than wrapped in <c>#if</c>: the compiler
        /// removes the call and the interpolated string with it, so a release build pays nothing — and the binder
        /// itself still exists, which matters because a component compiled out of a build takes every scene reference
        /// to it with it.
        /// </remarks>
        [Conditional("UNITY_EDITOR")]
        [Conditional("DEVELOPMENT_BUILD")]
        private static void Log(string message) =>
            Debug.Log(message);

        /// <summary>
        /// Formats <paramref name="value"/> for the console, without assuming it is there.
        /// </summary>
        /// <remarks>
        /// This binder accepts every bound type through <see cref="IAnyBinder"/>, and a bindable member of a
        /// reference type publishes <see langword="null"/> the moment the binder is added — so the very first
        /// message a debug binder is asked to produce is usually for a null value. Both the converter and the
        /// fallback used to dereference it.
        /// </remarks>
        private string GetMessage(object value)
        {
            if (value is null) return "null";
            return _converter?.Convert(value) ?? value.ToString();
        }
    }
}