using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – EulerAngles Enum")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalRotation", SubPath = "Enum")]
    public sealed class TransformEulerAnglesEnumMonoBinder : EnumMonoBinder<Vector3>
    {
        [SerializeField] private Space _space = Space.World;
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;

        protected override void SetValue(Vector3 value) =>
            transform.SetEulerAngles(value, _space, _converter);
    }
}