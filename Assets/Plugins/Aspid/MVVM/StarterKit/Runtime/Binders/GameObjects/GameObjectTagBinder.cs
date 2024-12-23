#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class GameObjectTagBinder : TargetBinder<GameObject>, IBinder<string>
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<string?, string?>? _converter;

        public GameObjectTagBinder(GameObject target, Func<string?, string?> converter)
            : this(target, converter.ToConvert()) { }
        
        public GameObjectTagBinder(GameObject target, IConverter<string?, string?>? converter = null)
            : base(target)
        {
            _converter = converter;
        }

        public void SetValue(string? value) =>
            Target.tag = _converter?.Convert(value) ?? value;
    }
}