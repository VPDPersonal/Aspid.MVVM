using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds the <see cref="Selectable.interactable"/> property.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Selectable), serializePropertyNames: "m_Interactable")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – Interactable")]
    public class SelectableInteractableMonoBinder : ComponentMonoBinder<Selectable, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.interactable;
            set => CachedComponent.interactable = value;
        }
    }
}