#nullable enable
using System;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed class ObjectNameBinder : TargetBinder<Object>,
        IBinder<string>, 
        IReverseBinder<string>
    {
        public event Action<string?>? ValueChanged;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public ObjectNameBinder(GameObject target, BindMode mode)
            : this(target, converter: null, mode) { }
        
        public ObjectNameBinder(GameObject target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _converter = converter;
        }

        public void SetValue(string? value) =>
            Target.name = GetConvertedValue(value);
        
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedValue(Target.name));
        }
        
        private string GetConvertedValue(string value) =>
            _converter?.Convert(value) ?? value ?? string.Empty;
    }
}