using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Canvas Group/CanvasGroup Binder - BlocksRaycasts Enum")]
    public sealed class CanvasGroupBlocksRaycastsEnumMonoBinder : EnumComponentMonoBinder<CanvasGroup, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.blocksRaycasts = value;
    }
}