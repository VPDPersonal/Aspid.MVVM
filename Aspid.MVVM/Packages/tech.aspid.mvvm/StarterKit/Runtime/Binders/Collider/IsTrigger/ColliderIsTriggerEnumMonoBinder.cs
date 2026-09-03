using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Collider.isTrigger"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_IsTrigger", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – IsTrigger Enum")]
    public sealed class ColliderIsTriggerEnumMonoBinder : EnumMonoBinder<Collider, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            CachedComponent.isTrigger = value;
    }
}
