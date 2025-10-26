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
    [BindModeOverride(IsAll = true)]
    public sealed class DebugLogBinder : Binder, IAnyBinder, IAnyReverseBinder
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter = new ObjectToStringConverter();

        public DebugLogBinder(Converter converter = null) : base(BindMode.TwoWay)
        {
            _converter = converter;
        }

        public event Action<object> ValueChanged
        {
            add => Debug.Log($"Add ValueChanged: {GetMessage(value)}");
            remove => Debug.Log($"Remove ValueChanged: {GetMessage(value)}");
        }

        public void SetValue<T>(T value) =>
            Debug.Log($"SetValue: {GetMessage(value)}");

        private string GetMessage(object value) =>
            _converter?.Convert(value) ?? value.ToString();
    }
}