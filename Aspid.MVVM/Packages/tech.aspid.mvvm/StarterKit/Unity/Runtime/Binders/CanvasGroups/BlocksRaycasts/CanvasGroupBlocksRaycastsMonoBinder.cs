using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds the <see cref="CanvasGroup.blocksRaycasts"/> property.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_BlocksRaycasts")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – BlocksRaycasts")]
    public class CanvasGroupBlocksRaycastsMonoBinder : ComponentMonoBinder<CanvasGroup, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.blocksRaycasts;
            set => CachedComponent.blocksRaycasts = value;
        }
    }
}