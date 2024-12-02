using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Transform/Rect Transform Binder - Anchored Position Enum")]
    public sealed class RectTransformAnchoredPositionEnumMonoBinder : EnumMonoBinder<RectTransform, Vector3>
    {
        [SerializeField] private Space _space = Space.World;
        [SerializeField] private VectorMode _mode = VectorMode.XYZ;

        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetAnchoredPosition(value, _mode, _space);
    }
}