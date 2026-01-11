using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_BlocksRaycasts")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas Group/CanvasGroup Binder – BlocksRaycasts Enum")]
    public sealed class CanvasGroupBlocksRaycastsEnumMonoBinder : EnumMonoBinder<CanvasGroup, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.blocksRaycasts = value;
    }
}