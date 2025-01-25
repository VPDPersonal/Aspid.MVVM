using UnityEngine;
using Aspid.MVVM.Mono.Generation;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterVector2;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder - SizeDelta")]
    public partial class RectTransformSizeDeltaMonoBinder : ComponentMonoBinder<RectTransform>, IBinder<Vector2>, INumberBinder
    {
        [Header("Parameters")]
        [SerializeField] private SizeDeltaMode _mode = SizeDeltaMode.SizeDelta;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

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