using UnityEngine;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/RectTransform/RectTransform Binder - SizeDelta")]
    public partial class RectTransformSizeDeltaMonoBinder : ComponentMonoBinder<RectTransform>, IBinder<Vector2>, INumberBinder
    {
        [Header("Parameters")]
        [SerializeField] private SizeDeltaMode _mode = SizeDeltaMode.SizeDelta;
        
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Vector2, Vector2> _converter;
#else
        private IConverterVector2 _converter;
#endif

        [BinderLog]
        public void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.SetSizeDelta(value, _mode);
        }

        [BinderLog]
        public void SetValue(int value) =>
            SetValue(new Vector2(value, value));

        [BinderLog]
        public void SetValue(long value) =>
            SetValue(new Vector2(value, value));

        [BinderLog]
        public void SetValue(float value) =>
            SetValue(new Vector2(value, value));

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}