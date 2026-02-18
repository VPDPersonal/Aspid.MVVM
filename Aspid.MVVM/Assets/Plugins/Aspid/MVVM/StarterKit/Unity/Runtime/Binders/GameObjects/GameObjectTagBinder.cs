#nullable enable
using System;
using UnityEngine;
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
    public sealed class GameObjectTagBinder : TargetBinder<GameObject>,
        IBinder<string>, 
        IReverseBinder<string>
    {
        public event Action<string?>? ValueChanged;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public GameObjectTagBinder(GameObject target, BindMode mode)
            : this(target, converter: null, mode) { }
        
        public GameObjectTagBinder(GameObject target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _converter = converter;
        }

        public void SetValue(string? value) =>
            Target.tag = GetConvertedValue(value);
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedValue(Target.tag));
        }
        
        private string GetConvertedValue(string value) =>
            _converter?.Convert(value) ?? value;
    }
}