using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder - Position Enum")]
    public sealed class TransformPositionEnumMonoBinder : EnumMonoBinder<Vector3>
    {
        [Header("Parameter")]
        [SerializeField] private Space _space = Space.World;    
        
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;

        protected override void SetValue(Vector3 value) =>
            transform.SetPosition(value, _space, _converter);
    }
}