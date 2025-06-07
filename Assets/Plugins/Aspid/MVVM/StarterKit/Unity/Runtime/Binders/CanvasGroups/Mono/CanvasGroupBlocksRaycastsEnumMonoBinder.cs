using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(CanvasGroup), "m_BlocksRaycasts")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas Group/CanvasGroup Binder - BlocksRaycasts Enum")]
    [AddComponentContextMenu(typeof(CanvasGroup),"Add CanvasGroup Binder/CanvasGroup Binder - BlocksRaycasts Enum")]
    public sealed class CanvasGroupBlocksRaycastsEnumMonoBinder : EnumComponentMonoBinder<CanvasGroup, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.blocksRaycasts = value;
    }
}