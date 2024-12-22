using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Transform/Transform Binder - Scale Enum")]
    public sealed class TransformScaleEnumMonoBinder : EnumMonoBinder<Vector3>
    {
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
        protected override void SetValue(Vector3 value) =>
            transform.SetScale(value, _converter);
    }
}