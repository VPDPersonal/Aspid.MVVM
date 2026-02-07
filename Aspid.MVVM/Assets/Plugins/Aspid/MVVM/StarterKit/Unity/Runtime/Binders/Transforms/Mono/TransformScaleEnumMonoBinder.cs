using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Scale Enum")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalScale", SubPath = "Enum")]
    public sealed class TransformScaleEnumMonoBinder : EnumMonoBinder<Vector3>
    {
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
        protected override void SetValue(Vector3 value) =>
            transform.SetScale(value, _converter);
    }
}