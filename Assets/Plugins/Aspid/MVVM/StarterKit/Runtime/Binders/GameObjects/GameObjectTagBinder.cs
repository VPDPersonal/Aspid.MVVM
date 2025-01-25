#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
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

        public GameObjectTagBinder(GameObject target, Func<string?, string?> converter)
            : this(target, converter.ToConvert()) { }
        
        public GameObjectTagBinder(GameObject target, Converter? converter = null)
            : base(target)
        {
            _converter = converter;
        }

        public void SetValue(string? value) =>
            Target.tag = _converter?.Convert(value) ?? value;
    }
}