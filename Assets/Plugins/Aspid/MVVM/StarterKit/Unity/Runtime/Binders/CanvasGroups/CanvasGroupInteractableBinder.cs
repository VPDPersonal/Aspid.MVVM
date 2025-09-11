#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class CanvasGroupInteractableBinder : TargetBinder<CanvasGroup>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;

        public CanvasGroupInteractableBinder(CanvasGroup target, BindMode mode)
            : this(target, false, mode) { }
        
        public CanvasGroupInteractableBinder(CanvasGroup target, bool isInvert = false, BindMode mode = BindMode.OneTime)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _isInvert = isInvert;
        }
        
        public void SetValue(bool value) =>
            Target.interactable = _isInvert ? !value : value;
    }
}