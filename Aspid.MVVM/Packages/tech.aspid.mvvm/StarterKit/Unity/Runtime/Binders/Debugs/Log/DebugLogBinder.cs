using System;
using Conditional = System.Diagnostics.ConditionalAttribute;
using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<object, string>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IAnyBinder"/> and <see cref="IAnyReverseBinder"/>
    /// that logs all binding events and incoming values to the Unity console.
    /// </summary>
    /// <include file="XmlExampleDoc-Debug-Log-1.1.0.xml" path="doc//member[@name='DebugLogBinder']/*" />
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public sealed class DebugLogBinder : Binder, IAnyBinder, IAnyReverseBinder
    {
        /// <summary>
        /// Raised with the bound value when propagating back to the ViewModel in <see cref="BindMode.OneWayToSource"/>.
        /// Both <see langword="add"/> and <see langword="remove"/> operations log the subscriber reference to the Unity console.
        /// </summary>
        public event Action<object> ValueChanged
        {
            add => Log($"Add ValueChanged: {GetMessage(value)}");
            remove => Log($"Remove ValueChanged: {GetMessage(value)}");
        }

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        [Tooltip("Formats bound values as log messages. Defaults to GenericToStringConverter.")]
        [SerializeReference] private Converter _converter;

        /// <param name="converter">The converter used to format bound values as log messages. Pass <see langword="null"/> to use <see cref="GenericToStringConverter{T}"/>.</param>
        public DebugLogBinder(Converter converter = null) : base(BindMode.TwoWay)
        {
            _converter = converter ?? new GenericToStringConverter<object>();
        }

        /// <summary>
        /// Logs the received value to the Unity console.
        /// </summary>
        /// <typeparam name="T">The runtime type of the incoming value.</typeparam>
        /// <param name="value">The bound value received from the ViewModel.</param>
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
