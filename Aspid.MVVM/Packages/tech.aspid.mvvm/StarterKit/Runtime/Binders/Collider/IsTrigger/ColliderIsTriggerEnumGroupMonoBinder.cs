using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Collider.isTrigger"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_IsTrigger", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – IsTrigger EnumGroup")]
    public sealed class ColliderIsTriggerEnumGroupMonoBinder : EnumGroupMonoBinder<Collider, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(Collider element, bool value) =>
            element.isTrigger = value;
    }
}
