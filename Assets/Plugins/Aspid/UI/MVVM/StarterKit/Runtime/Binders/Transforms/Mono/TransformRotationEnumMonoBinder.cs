using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Rotation Enum")]
    public sealed class TransformRotationEnumMonoBinder : EnumMonoBinder<Vector3>
    {
        [SerializeField] private Space _space = Space.World;

        protected override void SetValue(Vector3 value) =>
            transform.SetRotation(Quaternion.Euler(value), _space);
    }
}