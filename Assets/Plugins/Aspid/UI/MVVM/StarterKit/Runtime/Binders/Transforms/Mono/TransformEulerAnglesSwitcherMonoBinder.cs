using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Euler Angles Switcher")]
    public sealed class TransformEulerAnglesSwitcherMonoBinder : SwitcherMonoBinder<Vector3>
    {
        [SerializeField] private Space _space = Space.World;
        [SerializeField] private VectorMode _mode = VectorMode.XYZ;

        protected override void SetValue(Vector3 value) =>
            transform.SetEulerAngles(value, _mode, _space);
    }
}