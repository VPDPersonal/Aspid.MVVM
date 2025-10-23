#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class GameObjectVisibleBinder : TargetBinder<GameObject>, IBinder<bool>
    {
        [SerializeField] private bool _isInvert;
        
        public GameObjectVisibleBinder(GameObject target, BindMode mode)
            : this(target, false, mode) { }
        
        public GameObjectVisibleBinder(GameObject target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _isInvert = isInvert;
        }
        
        public void SetValue(bool value) =>
            Target.SetActive(_isInvert ? !value : value);
    }
}