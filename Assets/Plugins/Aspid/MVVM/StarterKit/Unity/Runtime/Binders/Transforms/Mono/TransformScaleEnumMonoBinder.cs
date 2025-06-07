using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Transform), "m_LocalScale")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder - Scale Enum")]
    [AddComponentContextMenu(typeof(Transform),"Add Transform Binder/Transform Binder - Scale Enum")]
    public sealed class TransformScaleEnumMonoBinder : EnumMonoBinder<Vector3>
    {
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
        protected override void SetValue(Vector3 value) =>
            transform.SetScale(value, _converter);
    }
}