using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder - AnchoredPosition Enum")]
    [AddComponentContextMenu(typeof(RectTransform),"Add RectTransform Binder/RectTransform Binder - AnchoredPosition Enum")]
    public sealed class RectTransformAnchoredPositionEnumMonoBinder : EnumMonoBinder<RectTransform, Vector3>
    {
        [SerializeField] private Space _space = Space.World;
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;

        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetAnchoredPosition(value, _space, _converter);
    }
}