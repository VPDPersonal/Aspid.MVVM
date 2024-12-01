using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Canvas Group/Canvas Group Binder - Alpha Switcher")]
    public sealed class CanvasGroupAlphaSwitcherMonoBinder : SwitcherMonoBinder<CanvasGroup, float>
    {
        protected override void SetValue(float value) =>
            CachedComponent.alpha = value;
    }
}