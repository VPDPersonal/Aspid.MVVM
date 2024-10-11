#nullable enable
using System;
using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.GameObjects
{
    public class GameObjectVisibleBinder : Binder, IBinder<bool>
    {
        private readonly bool _isInvert;
        private readonly GameObject _gameObject;
        
        public GameObjectVisibleBinder(GameObject gameObject, bool isInvert = false)
        {
            _isInvert = isInvert;
            _gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
        }
        
        public void SetValue(bool value)
        {
            if (_isInvert) value = !value;
            _gameObject.SetActive(value);
        }
    }
}