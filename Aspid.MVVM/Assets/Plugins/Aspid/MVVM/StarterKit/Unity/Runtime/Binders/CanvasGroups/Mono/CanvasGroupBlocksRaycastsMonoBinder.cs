using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{CanvasGroup}"/> that binds the <see cref="CanvasGroup.blocksRaycasts"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current blocksRaycasts value
    /// is sent back to the ViewModel. Supports optional value inversion.
    /// </remarks>
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_BlocksRaycasts")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – BlocksRaycasts")]
    public class CanvasGroupBlocksRaycastsMonoBinder : ComponentBoolMonoBinder<CanvasGroup>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.blocksRaycasts;
            set => CachedComponent.blocksRaycasts = value;
        }
    }
}