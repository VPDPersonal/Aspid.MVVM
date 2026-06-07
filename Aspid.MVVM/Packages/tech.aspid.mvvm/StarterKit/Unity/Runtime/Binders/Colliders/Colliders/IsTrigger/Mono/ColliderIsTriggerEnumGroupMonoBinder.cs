using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{Collider, bool}"/> that sets the <see cref="Collider.isTrigger"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – IsTrigger EnumGroup")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_IsTrigger", SubPath = "EnumGroup")]
    public sealed class ColliderIsTriggerEnumGroupMonoBinder : EnumGroupMonoBinder<Collider, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(Collider element, bool value) =>
            element.isTrigger = value;
    }
}