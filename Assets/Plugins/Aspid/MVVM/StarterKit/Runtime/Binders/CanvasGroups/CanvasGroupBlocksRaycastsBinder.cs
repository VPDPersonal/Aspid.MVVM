#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class CanvasGroupBlocksRaycastsBinder : TargetBinder<CanvasGroup>, IBinder<bool>
    {
        [Header("Parameter")]
        [SerializeField] private bool _isInvert;

        public CanvasGroupBlocksRaycastsBinder(CanvasGroup target, bool isInvert = false)
            : base(target)
        {
            _isInvert = isInvert;
        }
        
        public void SetValue(bool value) =>
            Target.blocksRaycasts = _isInvert ? !value : value;
    }
}