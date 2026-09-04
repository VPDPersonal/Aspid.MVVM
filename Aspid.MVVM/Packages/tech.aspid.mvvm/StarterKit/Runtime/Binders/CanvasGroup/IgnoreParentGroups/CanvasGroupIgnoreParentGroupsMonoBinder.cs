using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds
    /// <see cref="CanvasGroup.ignoreParentGroups"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_IgnoreParentGroups")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Ignore Parent Groups")]
    public class CanvasGroupIgnoreParentGroupsMonoBinder : ComponentMonoBinder<CanvasGroup, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.ignoreParentGroups;
            set => CachedComponent.ignoreParentGroups = value;
        }
    }
}
