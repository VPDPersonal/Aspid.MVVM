using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Quaternion, UnityEngine.Quaternion>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterQuaternion;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder - Rotation Enum")]
    public sealed class TransformRotationEnumMonoBinder : EnumMonoBinder<Vector3>
    {
        [SerializeField] private Space _space = Space.World;

        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        protected override void SetValue(Vector3 value)
        {
            var rotation = Quaternion.Euler(value);
            rotation = _converter?.Convert(rotation) ?? rotation;
            transform.SetRotation(rotation, _space);
        }
    }
}