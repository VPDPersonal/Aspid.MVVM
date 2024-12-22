using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/RectTransform/RectTransform Binder - AnchoredPosition Enum")]
    public sealed class RectTransformAnchoredPositionEnumMonoBinder : EnumComponentMonoBinder<RectTransform, Vector3>
    {
        [Header("Parameter")]
        [SerializeField] private Space _space = Space.World;

        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;

        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetAnchoredPosition(value, _space, _converter);
    }
}