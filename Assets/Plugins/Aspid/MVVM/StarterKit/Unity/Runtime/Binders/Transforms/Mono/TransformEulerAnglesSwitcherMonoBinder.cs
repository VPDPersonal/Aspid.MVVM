using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(Transform), "m_LocalRotation")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder - EulerAngles Switcher")]
    [AddComponentContextMenu(typeof(Transform),"Add Transform Binder/Transform Binder - EulerAngles Switcher")]
    public sealed class TransformEulerAnglesSwitcherMonoBinder : SwitcherMonoBinder<Vector3>
    {
        [SerializeField] private Space _space = Space.World;    
        
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;

        protected override void SetValue(Vector3 value) =>
            transform.SetEulerAngles(value, _space, _converter);
    }
}