#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class CanvasGroupIgnoreParentGroupsBinder : TargetBinder<CanvasGroup>, IBinder<bool>
    {
        [Header("Parameter")]
        [SerializeField] private bool _isInvert;

        public CanvasGroupIgnoreParentGroupsBinder(CanvasGroup target, bool isInvert = false)
            : base(target)
        {
            _isInvert = isInvert;
        }
        
        public void SetValue(bool value) =>
            Target.ignoreParentGroups = _isInvert ? !value : value;
    }
}