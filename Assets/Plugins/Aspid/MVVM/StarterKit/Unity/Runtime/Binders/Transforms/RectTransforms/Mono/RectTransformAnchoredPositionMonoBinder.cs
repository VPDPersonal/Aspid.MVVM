using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder - AnchoredPosition")]
    [AddComponentContextMenu(typeof(RectTransform),"Add RectTransform Binder/RectTransform Binder - AnchoredPosition")]
    public partial class RectTransformAnchoredPositionMonoBinder : ComponentMonoBinder<RectTransform>, IVectorBinder, INumberBinder
    {
        [Header("Parameter")]
        [SerializeField] private Space _space = Space.World;

        [Header("Converter")]
        [SerializeField] private Vector3CombineConverter _converter = Vector3CombineConverter.Default;
        
        [BinderLog]
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);

        [BinderLog]
        public void SetValue(Vector3 value) =>
            CachedComponent.SetAnchoredPosition(value, _space, _converter);
        
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);
        
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
        
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(new Vector3(value, value, value));
    }
}