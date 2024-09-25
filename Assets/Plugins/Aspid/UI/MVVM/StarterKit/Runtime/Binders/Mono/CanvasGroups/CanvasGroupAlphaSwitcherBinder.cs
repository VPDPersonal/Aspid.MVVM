using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.CanvasGroups
{
    [AddComponentMenu("UI/Binders/Canvas Group/Canvas Group Binder - Alpha Switcher")]
    public sealed class CanvasGroupAlphaSwitcherBinder : SwitcherMonoBinder<CanvasGroup, float>, IBinder<bool>
    {
        protected override void SetValue(float value) =>
            CachedComponent.alpha = value;
    }
}