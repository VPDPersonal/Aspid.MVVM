using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that logs every bound value and reverse subscription to the console.
    /// </summary>
    /// <remarks>
    /// Logs only in the Editor and in development builds.
    /// </remarks>
    [GenerateSerializableBinder]
    [BindModeOverride(IsAll = true)]
    [AddComponentMenu("Aspid/MVVM/Binders/Debug/Debug Binder – Log")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Debug/Debug Binder – Log")]
    public sealed partial class DebugLogMonoBinder : MonoBinder, IAnyBinder, IAnyReverseBinder
    {
        [Tooltip("Formats values for the log; empty uses ToString.")]
        [SerializeReference] private IConverter<object, string> _converter = new ValueToStringConverter<object>();

        /// <inheritdoc/>
        public event Action<object> ValueChanged
        {
            add => Log($"Add ValueChanged: {Format(value)}");
            remove => Log($"Remove ValueChanged: {Format(value)}");
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue<T>(T value) =>
            Log($"SetValue: {Format(value)}");

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        [System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
        private static void Log(string message) =>
            UnityEngine.Debug.Log(message);

        private string Format(object value) =>
            value is null ? "null" : _converter?.Convert(value) ?? value.ToString();
    }
}
