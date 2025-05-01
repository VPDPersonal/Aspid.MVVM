#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public class CanvasGroupIgnoreParentGroupsBinder : TargetBinder<CanvasGroup>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public CanvasGroupIgnoreParentGroupsBinder(CanvasGroup target, BindMode mode)
            : this(target, false, mode) { }
        
        public CanvasGroupIgnoreParentGroupsBinder(
            CanvasGroup target, 
            bool isInvert = false, 
            BindMode mode = BindMode.OneTime)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _isInvert = isInvert;
        }
        
        public void SetValue(bool value) =>
            Target.ignoreParentGroups = _isInvert ? !value : value;
    }
}