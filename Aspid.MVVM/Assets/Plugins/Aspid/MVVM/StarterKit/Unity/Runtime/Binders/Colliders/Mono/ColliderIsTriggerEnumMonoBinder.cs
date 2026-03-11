using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{Collider, bool}"/> that sets the <see cref="Collider.isTrigger"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – IsTrigger Enum")]
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_IsTrigger", SubPath = "Enum")]
    public sealed class ColliderIsTriggerEnumMonoBinder : EnumMonoBinder<Collider, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            CachedComponent.isTrigger = value;
    }
}