using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Transform/Rect Transform Binder - Anchored Position Switcher")]
    public sealed class RectTransformAnchoredPositionSwitcherMonoBinder : SwitcherMonoBinder<RectTransform, Vector3>
    {
        [SerializeField] private Space _space = Space.World;
        [SerializeField] private VectorMode _mode = VectorMode.XYZ;

        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetAnchoredPosition(value, _mode, _space);
    }
}