using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Collider.enabled"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(Collider), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Enabled EnumGroup")]
    public sealed class ColliderEnabledEnumGroupMonoBinder : EnumGroupMonoBinder<Collider, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(Collider element, bool value) =>
            element.enabled = value;
    }
}
