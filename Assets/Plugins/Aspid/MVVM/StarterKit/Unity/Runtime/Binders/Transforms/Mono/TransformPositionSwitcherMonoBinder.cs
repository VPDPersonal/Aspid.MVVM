using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Transform), "m_LocalPosition")]
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder - Position Switcher")]
    [AddComponentContextMenu(typeof(Transform),"Add Transform Binder/Transform Binder - Position Switcher")]
    public sealed class TransformPositionSwitcherMonoBinder : SwitcherMonoBinder<Vector3>
    {
        [SerializeField] private Space _space = Space.World;    
        
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;

        protected override void SetValue(Vector3 value) =>
            transform.SetPosition(value, _space, _converter);
    }
}