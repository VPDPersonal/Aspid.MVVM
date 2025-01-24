using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/Transform/Transform Binder - Scale Switcher")]
    public sealed class TransformScaleSwitcherMonoBinder : SwitcherMonoBinder<Vector3>
    {
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;

        protected override void SetValue(Vector3 value) =>
            transform.SetScale(value, _converter);
    }
}