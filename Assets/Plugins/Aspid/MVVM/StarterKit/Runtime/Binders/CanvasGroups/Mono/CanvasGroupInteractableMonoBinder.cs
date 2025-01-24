using UnityEngine;
using Aspid.MVVM.Mono.Generation;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas Group/CanvasGroup Binder - Interactable")]
    public partial class CanvasGroupInteractableMonoBinder : ComponentMonoBinder<CanvasGroup>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedComponent.interactable = _isInvert ? !value : value;
    }
}