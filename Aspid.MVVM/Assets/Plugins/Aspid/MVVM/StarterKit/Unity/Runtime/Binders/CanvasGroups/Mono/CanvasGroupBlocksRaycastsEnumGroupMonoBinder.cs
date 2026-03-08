using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="CanvasGroup.blocksRaycasts"/> property on a group of <see cref="CanvasGroup"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – BlocksRaycasts EnumGroup")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_BlocksRaycasts", SubPath = "EnumGroup")]
    public sealed class CanvasGroupBlocksRaycastsEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup, bool>
    {
        protected override void SetValue(CanvasGroup element, bool value) =>
            element.blocksRaycasts = value;
    }
}