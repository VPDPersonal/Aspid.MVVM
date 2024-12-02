using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Canvas Group/Canvas Group Binder - Alpha Enum")]
    public sealed class CanvasGroupAlphaEnumMonoBinder : EnumMonoBinder<CanvasGroup, float>
    {
        protected override void SetValue(float value) =>
            CachedComponent.alpha = value;
    }
}