using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="CanvasGroup.interactable"/> property on a <see cref="CanvasGroup"/>
    /// when the bound ViewModel value changes. 
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel. Supports optional value inversion.
    /// </summary>
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Interactable")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Interactable")]
    public class CanvasGroupInteractableMonoBinder : ComponentBoolMonoBinder<CanvasGroup>
    {
        protected sealed override bool Property
        {
            get => CachedComponent.interactable;
            set => CachedComponent.interactable = value;
        }
    }
}