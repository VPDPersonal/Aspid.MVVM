using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Selectable), serializePropertyNames: "m_Interactable", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – Interactable EnumGroup")]
    public sealed class SelectableInteractableEnumGroupMonoBinder : EnumGroupMonoBinder<Selectable, bool>
    {
        protected override void SetValue(Selectable element, bool value) =>
            element.interactable = value;
    }
}