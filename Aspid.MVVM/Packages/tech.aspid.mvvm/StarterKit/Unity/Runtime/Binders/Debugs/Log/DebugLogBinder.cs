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
            add => Debug.Log($"Add ValueChanged: {GetMessage(value)}");
            remove => Debug.Log($"Remove ValueChanged: {GetMessage(value)}");
        }

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("Converter used to format bound values as log messages. Defaults to ObjectToStringConverter.")]
        [SerializeReference] private Converter _converter = new ObjectToStringConverter();

        /// <summary>
        /// Initializes a new instance of <see cref="DebugLogBinder"/>.
        /// </summary>
        /// <param name="converter">The converter used to format bound values as log messages. Pass <see langword="null"/> to use <see cref="ObjectToStringConverter"/>.</param>
        public DebugLogBinder(Converter converter = null) : base(BindMode.TwoWay)
        {
            // The field initializer above does not survive the constructor, so the documented default has to be
            // repeated here — otherwise the parameterless call leaves the binder with no converter at all.
            _converter = converter ?? new ObjectToStringConverter();
        }

        /// <summary>
        /// Logs the received value to the Unity console.
        /// </summary>
        /// <typeparam name="T">The runtime type of the incoming value.</typeparam>
        /// <param name="value">The bound value received from the ViewModel.</param>
        public void SetValue<T>(T value) =>
            Debug.Log($"SetValue: {GetMessage(value)}");

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
