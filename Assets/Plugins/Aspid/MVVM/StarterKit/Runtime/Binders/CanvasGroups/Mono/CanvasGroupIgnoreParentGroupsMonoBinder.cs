using UnityEngine;
using Aspid.MVVM.Mono.Generation;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Canvas Group/CanvasGroup Binder - IgnoreParentGroups")]
    public partial class CanvasGroupIgnoreParentGroupsMonoBinder : ComponentMonoBinder<CanvasGroup>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedComponent.ignoreParentGroups = _isInvert ? !value : value;
    }
}