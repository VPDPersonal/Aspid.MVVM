using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="Selectable.interactable"/> property on a group of <see cref="Selectable"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable), serializePropertyNames: "m_Interactable", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – Interactable EnumGroup")]
    public sealed class SelectableInteractableEnumGroupMonoBinder : EnumGroupMonoBinder<Selectable, bool>
    {
        protected override void SetValue(Selectable element, bool value) =>
            element.interactable = value;
    }
}