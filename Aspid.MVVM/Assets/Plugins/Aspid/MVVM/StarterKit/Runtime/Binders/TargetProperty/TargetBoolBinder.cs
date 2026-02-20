using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public abstract class TargetBoolBinder<TTarget> : TargetBinder<TTarget, bool>
    {
        [SerializeField] private bool _isInvert;
        
        public TargetBoolBinder(TTarget target, bool isInvert, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            _isInvert = isInvert;
        }
        
        protected override bool GetConvertedValue(bool value) =>
            _isInvert ? !value : value;
    }
}