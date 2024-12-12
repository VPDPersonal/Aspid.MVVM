#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class GameObjectTagBinder : Binder, IBinder<string>
    {
        [Header("Component")]
        [SerializeField] private GameObject _gameObject;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<string?, string?>? _converter;

        public GameObjectTagBinder(GameObject gameObject, Func<string?, string?> converter)
            : this(gameObject, new GenericFuncConverter<string?, string?>(converter)) { }
        
        public GameObjectTagBinder(GameObject gameObject, IConverter<string?, string?>? converter = null)
        {
            _converter = converter;
            _gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
        }

        public void SetValue(string? value) =>
            _gameObject.tag = _converter?.Convert(value) ?? value;
    }
}