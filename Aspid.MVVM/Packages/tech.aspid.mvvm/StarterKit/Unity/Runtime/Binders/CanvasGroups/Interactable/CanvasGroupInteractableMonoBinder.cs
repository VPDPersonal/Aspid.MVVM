using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds the <see cref="CanvasGroup.interactable"/> property.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Interactable")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Interactable")]
    public class CanvasGroupInteractableMonoBinder : ComponentMonoBinder<CanvasGroup, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.interactable;
            set => CachedComponent.interactable = value;
        }
    }
}