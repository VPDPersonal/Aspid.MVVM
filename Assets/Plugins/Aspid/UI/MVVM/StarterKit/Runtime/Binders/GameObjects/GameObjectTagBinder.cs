#nullable enable
using System;
using UnityEngine;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders
{
    public class GameObjectTagBinder : Binder, IBinder<string>
    {
        private readonly GameObject _gameObject;
        private readonly IConverter<string?, string>? _converter;

        public GameObjectTagBinder(GameObject gameObject, Func<string?, string> converter)
            : this(gameObject, new GenericFuncConverter<string?, string>(converter)) { }
        
        public GameObjectTagBinder(GameObject gameObject, IConverter<string?, string>? converter = null)
        {
            _converter = converter;
            _gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
        }

        public void SetValue(string? value) =>
            _gameObject.tag = _converter?.Convert(value) ?? value;
    }
}