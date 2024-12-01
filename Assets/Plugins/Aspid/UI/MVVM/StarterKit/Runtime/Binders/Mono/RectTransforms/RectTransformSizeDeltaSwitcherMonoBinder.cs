using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Transform/Rect Transform Binder - Size Delta Switcher")]
    public sealed class RectTransformSizeDeltaSwitcherMonoBinder : SwitcherMonoBinder<RectTransform, Vector2>
    {
        [SerializeField] private SizeDeltaMode _mode = SizeDeltaMode.SizeDelta;
        
        protected override void SetValue(Vector2 value) =>
            CachedComponent.SizeDelta(value, _mode);
    }
}