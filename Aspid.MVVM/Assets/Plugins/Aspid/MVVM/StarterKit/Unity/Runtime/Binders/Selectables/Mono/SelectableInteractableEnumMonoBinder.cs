using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Selectable), serializePropertyNames: "m_Interactable", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – Interactable Enum")]
    public sealed class SelectableInteractableEnumMonoBinder : EnumMonoBinder<Selectable, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.interactable = value;
    }
}