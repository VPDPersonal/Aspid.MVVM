using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Transform/Transform Binder - Rotation Enum")]
    public sealed class TransformRotationEnumMonoBinder : EnumMonoBinder<Vector3>
    {
        [SerializeField] private Space _space = Space.World;

        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Quaternion, Quaternion> _converter;
#else
        private IConverterQuaternion _converter;
#endif

        protected override void SetValue(Vector3 value)
        {
            var rotation = Quaternion.Euler(value);
            rotation = _converter?.Convert(rotation) ?? rotation;
            transform.SetRotation(rotation, _space);
        }
    }
}