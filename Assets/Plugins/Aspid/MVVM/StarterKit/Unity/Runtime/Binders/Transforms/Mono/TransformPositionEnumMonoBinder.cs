using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(Transform), "m_LocalPosition")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder - Position Enum")]
    [AddComponentContextMenu(typeof(Transform),"Add Transform Binder/Transform Binder - Position Enum")]
    public sealed class TransformPositionEnumMonoBinder : EnumMonoBinder<Vector3>
    {
        [SerializeField] private Space _space = Space.World;    
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;

        protected override void SetValue(Vector3 value) =>
            transform.SetPosition(value, _space, _converter);
    }
}