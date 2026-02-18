using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Scale EnumGroup")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalScale", SubPath = "EnumGroup")]
    public sealed class TransformScaleEnumGroupMonoBinder : EnumGroupMonoBinder<Transform, Vector3, Converter>
    {
        [SerializeField] private Vector3 _defaultValue;
        [SerializeField] private Vector3 _selectedValue;

        protected override void SetValue(Transform element, Vector3 value) =>
            element.localScale = value;
    }
}