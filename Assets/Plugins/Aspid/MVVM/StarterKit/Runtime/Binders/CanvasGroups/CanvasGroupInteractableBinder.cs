#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class CanvasGroupInteractableBinder : TargetBinder<CanvasGroup>, IBinder<bool>
    {
        [Header("Parameter")]
        [SerializeField] private bool _isInvert;

        public CanvasGroupInteractableBinder(CanvasGroup target, bool isInvert = false)
            : base(target)
        {
            _isInvert = isInvert;
        }
        
        public void SetValue(bool value) =>
            Target.interactable = _isInvert ? !value : value;
    }
}