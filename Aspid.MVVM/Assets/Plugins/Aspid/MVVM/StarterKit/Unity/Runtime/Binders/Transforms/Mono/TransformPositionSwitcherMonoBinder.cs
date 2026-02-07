using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder – Position Switcher")]
    [AddBinderContextMenu(typeof(Transform), serializePropertyNames: "m_LocalPosition", SubPath = "Switcher")]
    public sealed class TransformPositionSwitcherMonoBinder : SwitcherMonoBinder<Vector3>
    {
        [SerializeField] private Space _space = Space.World;    
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;

        protected override void SetValue(Vector3 value) =>
            transform.SetPosition(value, _space, _converter);
    }
}