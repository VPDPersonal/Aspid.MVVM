using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – BlocksRaycasts Enum")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_BlocksRaycasts", SubPath = "Enum")]
    public sealed class CanvasGroupBlocksRaycastsEnumMonoBinder : EnumMonoBinder<CanvasGroup, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.blocksRaycasts = value;
    }
}