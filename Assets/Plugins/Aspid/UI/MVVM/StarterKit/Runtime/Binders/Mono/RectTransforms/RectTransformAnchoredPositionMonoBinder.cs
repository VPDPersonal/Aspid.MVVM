using UnityEngine;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Transform/Rect Transform Binder - Anchored Position")]
    public partial class RectTransformAnchoredPositionMonoBinder : ComponentMonoBinder<RectTransform>, IVectorBinder, INumberBinder
    {
        [Header("Parameters")]
        [SerializeField] private Space _space = Space.World;
        [SerializeField] private VectorMode _mode = VectorMode.XYZ;
        
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<Vector3, Vector3> _converter;
#else
        [SerializeReference] private IConverterVector3ToVector3 _converter;
#endif
        
        [BinderLog]
        public void SetValue(Vector2 value) =>
            SetValue((Vector3)value);

        [BinderLog]
        public void SetValue(Vector3 value) =>
            CachedComponent.SetAnchoredPosition(value, _mode, _space);
        
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