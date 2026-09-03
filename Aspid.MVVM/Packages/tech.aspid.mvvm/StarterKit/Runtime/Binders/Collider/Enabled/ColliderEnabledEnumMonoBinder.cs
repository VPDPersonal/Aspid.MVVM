using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Collider.enabled"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Collider), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Enabled Enum")]
    public sealed class ColliderEnabledEnumMonoBinder : EnumMonoBinder<Collider, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            CachedComponent.enabled = value;
    }
}
