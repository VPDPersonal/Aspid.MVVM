using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Transform), "m_LocalRotation")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder - EulerAngles Enum")]
    [AddComponentContextMenu(typeof(Transform),"Add Transform Binder/Transform Binder - EulerAngles Enum")]
    public sealed class TransformEulerAnglesEnumMonoBinder : EnumMonoBinder<Vector3>
    {
        [Header("Parameter")]
        [SerializeField] private Space _space = Space.World;

        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;

        protected override void SetValue(Vector3 value) =>
            transform.SetEulerAngles(value, _space, _converter);
    }
}