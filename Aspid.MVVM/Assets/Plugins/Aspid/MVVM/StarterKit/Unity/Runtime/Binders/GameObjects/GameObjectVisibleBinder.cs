#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed class GameObjectVisibleBinder : TargetBinder<GameObject>, 
        IBinder<bool>,
        IReverseBinder<bool>
    {
        public event Action<bool>? ValueChanged;
        
        [SerializeField] private bool _isInvert;
        
        public GameObjectVisibleBinder(GameObject target, BindMode mode)
            : this(target, isInvert: false, mode) { }
        
        public GameObjectVisibleBinder(GameObject target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _isInvert = isInvert;
        }
        
        public void SetValue(bool value) =>
            Target.SetActive(GetConvertedValue(value));
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedValue(Target.activeSelf));
        }

        private bool GetConvertedValue(bool value) =>
            _isInvert ? !value : value;
    }
}