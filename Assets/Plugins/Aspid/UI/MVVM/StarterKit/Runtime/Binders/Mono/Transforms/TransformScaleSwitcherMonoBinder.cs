using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Scale Switcher")]
    public sealed class TransformScaleSwitcherMonoBinder : SwitcherMonoBinder<Vector3>
    {
        [SerializeField] private VectorMode _mode = VectorMode.XYZ;

        protected override void SetValue(Vector3 value) =>
            transform.SetScale(value, _mode);
    }
}