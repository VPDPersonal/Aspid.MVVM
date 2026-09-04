using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Selectable.interactable"/> on each
    /// element.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable), serializePropertyNames: "m_Interactable", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – Interactable EnumGroup")]
    public sealed class SelectableInteractableEnumGroupMonoBinder : EnumGroupMonoBinder<Selectable, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(Selectable element, bool value) =>
            element.interactable = value;
    }
}
