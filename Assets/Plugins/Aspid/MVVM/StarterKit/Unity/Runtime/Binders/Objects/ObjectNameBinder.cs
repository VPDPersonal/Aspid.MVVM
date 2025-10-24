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
    public sealed class ObjectNameBinder : TargetBinder<Object>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public ObjectNameBinder(GameObject target, BindMode mode)
            : this(target, null, mode) { }
        
        public ObjectNameBinder(GameObject target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _converter = converter;
        }

        public void SetValue(string? value) =>
            Target.name = _converter?.Convert(value) ?? value ?? string.Empty;
    }
}