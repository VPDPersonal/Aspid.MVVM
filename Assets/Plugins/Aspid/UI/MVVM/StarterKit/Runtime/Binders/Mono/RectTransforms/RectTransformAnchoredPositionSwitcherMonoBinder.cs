using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.RectTransforms
{
    [AddComponentMenu("UI/Binders/Transform/Rect Transform Binder - Anchored Position Switcher")]
    public sealed class RectTransformAnchoredPositionSwitcherMonoBinder : SwitcherMonoBinder<RectTransform, Vector3>
    {
        protected override void SetValue(Vector3 value) =>
            CachedComponent.anchoredPosition3D = value;
    }
}