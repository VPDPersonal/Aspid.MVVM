#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class GameObjectVisibleBinder : Binder, IBinder<bool>
    {
        [Header("Component")]
        private GameObject _gameObject;
        
        [Header("Converter")]
        private bool _isInvert;
        
        public GameObjectVisibleBinder(GameObject gameObject, bool isInvert = false)
        {
            _isInvert = isInvert;
            _gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
        }
        
        public void SetValue(bool value) =>
            _gameObject.SetActive(_isInvert ? !value : value);
    }
    
}