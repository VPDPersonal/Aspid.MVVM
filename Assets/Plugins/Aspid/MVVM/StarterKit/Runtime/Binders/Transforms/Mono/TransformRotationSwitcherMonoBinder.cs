using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Rotation Switcher")]
    public sealed class TransformRotationSwitcherMonoBinder : SwitcherMonoBinder<Vector3>
    {
        [SerializeField] private Space _space = Space.World;

        protected override void SetValue(Vector3 value) =>
            transform.SetRotation(Quaternion.Euler(value), _space);
    }
}