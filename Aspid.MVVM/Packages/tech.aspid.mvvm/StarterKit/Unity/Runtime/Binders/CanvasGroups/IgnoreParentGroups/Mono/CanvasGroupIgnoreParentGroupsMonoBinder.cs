using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{CanvasGroup}"/> that binds the <see cref="CanvasGroup.ignoreParentGroups"/> property.
    /// </summary>
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_IgnoreParentGroups")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – IgnoreParentGroups")]
    public class CanvasGroupIgnoreParentGroupsMonoBinder : ComponentBoolMonoBinder<CanvasGroup>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.ignoreParentGroups;
            set => CachedComponent.ignoreParentGroups = value;
        }
    }
}