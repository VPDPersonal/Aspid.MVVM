using System;
using UnityEngine;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.GameObjects
{
    public class GameObjectTagBinder : Binder, IBinder<string>
    {
        protected readonly GameObject GameObject;
        protected readonly IConverter<string, string> Converter;

        public GameObjectTagBinder(GameObject gameObject, Func<string, string> converter)
            : this(gameObject, new GenericFuncConverter<string, string>(converter)) { }
        
        public GameObjectTagBinder(GameObject gameObject, IConverter<string, string> converter = null)
        {
            Converter = converter;
            GameObject = gameObject;
        }

        public void SetValue(string value) =>
            GameObject.tag = Converter?.Convert(value) ?? value;
    }
}