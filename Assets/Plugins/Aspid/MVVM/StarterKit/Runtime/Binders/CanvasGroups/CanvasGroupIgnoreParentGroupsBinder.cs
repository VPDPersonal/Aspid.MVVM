#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class CanvasGroupIgnoreParentGroupsBinder : Binder, IBinder<bool>
    {
        [Header("Component")]
        [SerializeField] private CanvasGroup _canvasGroup;
        
        [Header("Parameter")]
        [SerializeField] private bool _isInvert;

        public CanvasGroupIgnoreParentGroupsBinder(CanvasGroup canvasGroup, bool isInvert = false)
        {
            _isInvert = isInvert;
            _canvasGroup = canvasGroup;
        }
        
        public void SetValue(bool value) =>
            _canvasGroup.ignoreParentGroups = _isInvert ? !value : value;
    }
}