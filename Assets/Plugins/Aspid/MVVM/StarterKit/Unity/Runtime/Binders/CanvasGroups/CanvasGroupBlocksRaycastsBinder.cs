#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class CanvasGroupBlocksRaycastsBinder : TargetBinder<CanvasGroup>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public CanvasGroupBlocksRaycastsBinder(CanvasGroup target, BindMode mode)
            : this(target, false, mode) { }
        
        public CanvasGroupBlocksRaycastsBinder(CanvasGroup target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _isInvert = isInvert;
        }
        
        public void SetValue(bool value) =>
            Target.blocksRaycasts = _isInvert ? !value : value;
    }
}