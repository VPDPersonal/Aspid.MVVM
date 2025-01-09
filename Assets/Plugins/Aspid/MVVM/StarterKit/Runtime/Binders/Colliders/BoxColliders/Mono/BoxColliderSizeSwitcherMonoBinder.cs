using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Collider/BoxCollider Binder - Size Switcher")]
    public sealed class BoxColliderSizeSwitcherMonoBinder : SwitcherMonoBinder<BoxCollider, Vector3>
    {
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
        protected override void SetValue(Vector3 value) =>
            CachedComponent.size = _converter.Convert(value, CachedComponent.size);
    }
}