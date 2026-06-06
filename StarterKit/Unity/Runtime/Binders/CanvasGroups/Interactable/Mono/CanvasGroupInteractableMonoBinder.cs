using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{CanvasGroup}"/> that binds the <see cref="CanvasGroup.interactable"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current interactable value
    /// is sent back to the ViewModel. Supports optional value inversion.
    /// </remarks>
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Interactable")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Interactable")]
    public class CanvasGroupInteractableMonoBinder : ComponentBoolMonoBinder<CanvasGroup>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.interactable;
            set => CachedComponent.interactable = value;
        }
    }
}