using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="Selectable.interactable"/> property on a <see cref="Selectable"/>
    /// to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddBinderContextMenu(typeof(Selectable), serializePropertyNames: "m_Interactable", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Selectable/Selectable Binder – Interactable Enum")]
    public sealed class SelectableInteractableEnumMonoBinder : EnumMonoBinder<Selectable, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.interactable = value;
    }
}