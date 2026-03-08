using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="CanvasGroup.blocksRaycasts"/> property on a <see cref="CanvasGroup"/>
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel. Supports optional value inversion.
    /// </summary>
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_BlocksRaycasts")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – BlocksRaycasts")]
    public class CanvasGroupBlocksRaycastsMonoBinder : ComponentBoolMonoBinder<CanvasGroup>
    {
        protected sealed override bool Property
        {
            get => CachedComponent.blocksRaycasts;
            set => CachedComponent.blocksRaycasts = value;
        }
    }
}