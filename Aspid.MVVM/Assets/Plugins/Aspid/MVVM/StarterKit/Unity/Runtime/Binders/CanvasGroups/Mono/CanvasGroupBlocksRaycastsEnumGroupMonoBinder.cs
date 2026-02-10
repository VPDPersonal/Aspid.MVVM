using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – BlocksRaycasts EnumGroup")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_BlocksRaycasts", SubPath = "EnumGroup")]
    public sealed class CanvasGroupBlocksRaycastsEnumGroupMonoBinder : EnumGroupMonoBinder<CanvasGroup, bool>
    {
        protected override void SetValue(CanvasGroup element, bool value) =>
            element.blocksRaycasts = value;
    }
}