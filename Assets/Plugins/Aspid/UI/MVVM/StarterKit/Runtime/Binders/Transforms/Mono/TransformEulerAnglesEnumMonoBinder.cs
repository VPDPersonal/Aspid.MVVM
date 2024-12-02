using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Euler Angles Enum")]
    public sealed class TransformEulerAnglesEnumMonoBinder : EnumMonoBinder<Vector3>
    {
        [SerializeField] private Space _space = Space.World;
        [SerializeField] private VectorMode _mode = VectorMode.XYZ;

        protected override void SetValue(Vector3 value) =>
            transform.SetEulerAngles(value, _mode, _space);
    }
}