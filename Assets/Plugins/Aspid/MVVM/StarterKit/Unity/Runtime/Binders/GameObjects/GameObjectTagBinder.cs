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
    public class GameObjectTagBinder : TargetBinder<GameObject>, IBinder<string>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public GameObjectTagBinder(GameObject target, BindMode mode)
            : this(target, null, mode) { }
        
        public GameObjectTagBinder(GameObject target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _converter = converter;
        }

        public void SetValue(string? value) =>
            Target.tag = _converter?.Convert(value) ?? value;
    }
}