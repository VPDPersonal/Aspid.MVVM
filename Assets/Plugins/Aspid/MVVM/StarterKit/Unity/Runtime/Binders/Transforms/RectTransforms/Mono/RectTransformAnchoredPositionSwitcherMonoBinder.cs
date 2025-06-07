using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder - AnchoredPosition Switcher")]
    [AddComponentContextMenu(typeof(RectTransform),"Add RectTransform Binder/RectTransform Binder - AnchoredPosition Switcher")]
    public sealed class RectTransformAnchoredPositionSwitcherMonoBinder : SwitcherMonoBinder<RectTransform, Vector3>
    {
        [SerializeField] private Space _space = Space.World;       
        
        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;

        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetAnchoredPosition(value, _space, _converter);
    }
}