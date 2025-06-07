using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(CanvasGroup), "m_Interactable")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas Group/CanvasGroup Binder - Interactable")]
    [AddComponentContextMenu(typeof(CanvasGroup),"Add CanvasGroup Binder/CanvasGroup Binder - Interactable")]
    public partial class CanvasGroupInteractableMonoBinder : ComponentMonoBinder<CanvasGroup>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedComponent.interactable = _isInvert ? !value : value;
    }
}