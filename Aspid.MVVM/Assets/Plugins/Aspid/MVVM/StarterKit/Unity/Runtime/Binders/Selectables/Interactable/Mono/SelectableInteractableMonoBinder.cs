using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{Selectable}"/> that binds the <see cref="Selectable.interactable"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current interactable value
    /// is sent back to the ViewModel. Supports optional value inversion.
    /// </remarks>
    [AddBinderContextMenu(typeof(Selectable), serializePropertyNames: "m_Interactable")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – Interactable")]
    public class SelectableInteractableMonoBinder : ComponentBoolMonoBinder<Selectable>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.interactable;
            set => CachedComponent.interactable = value;
        }
    }
}