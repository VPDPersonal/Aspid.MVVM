using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Transform/Rect Transform Binder - Size Delta Enum")]
    public sealed class RectTransformSizeDeltaEnumMonoBinder : EnumMonoBinder<RectTransform, Vector2>
    {
        [SerializeField] private SizeDeltaMode _mode = SizeDeltaMode.SizeDelta;
        
        protected override void SetValue(Vector2 value) =>
            CachedComponent.SetSizeDelta(value, _mode);
    }
}