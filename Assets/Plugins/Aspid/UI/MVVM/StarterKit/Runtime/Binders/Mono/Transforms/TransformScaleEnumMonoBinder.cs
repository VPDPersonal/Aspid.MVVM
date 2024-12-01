using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Scale Enum")]
    public sealed class TransformScaleEnumMonoBinder : EnumMonoBinder<Vector3>
    {
        [Header("Parameter")]
        [SerializeField] private VectorMode _mode = VectorMode.XYZ;

        protected override void SetValue(Vector3 value) =>
            transform.SetScale(value, _mode);
    }
}