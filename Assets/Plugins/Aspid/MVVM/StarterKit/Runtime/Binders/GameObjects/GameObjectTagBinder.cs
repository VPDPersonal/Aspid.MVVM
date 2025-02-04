#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterString;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class GameObjectTagBinder : TargetBinder<GameObject>, IBinder<string>
    {
        [Header("Converter")]
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