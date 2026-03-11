using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{Selectable, bool}"/> that sets the <see cref="Selectable.interactable"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable), serializePropertyNames: "m_Interactable", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – Interactable Enum")]
    public sealed class SelectableInteractableEnumMonoBinder : EnumMonoBinder<Selectable, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            CachedComponent.interactable = value;
    }
}