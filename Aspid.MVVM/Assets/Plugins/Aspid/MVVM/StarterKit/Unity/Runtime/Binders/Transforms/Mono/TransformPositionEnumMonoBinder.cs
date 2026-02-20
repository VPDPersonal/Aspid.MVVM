using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Position Enum")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalPosition", SubPath = "Enum")]
    public sealed class TransformPositionEnumMonoBinder : EnumMonoBinder<Transform, Vector3, Converter>
    {
        [SerializeField] private Space _space = Space.World; 
        
        protected override void SetValue(Vector3 value) =>
            transform.SetPosition(value, _space);
    }
}