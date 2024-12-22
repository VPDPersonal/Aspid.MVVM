using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/RectTransform/RectTransform Binder - SizeDelta Enum")]
    public sealed class RectTransformSizeDeltaEnumMonoBinder : EnumComponentMonoBinder<RectTransform, Vector2>
    {
        [SerializeField] private SizeDeltaMode _mode = SizeDeltaMode.SizeDelta;
        
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Vector2, Vector2> _converter;
#else
        private IConverterVector2 _converter;
#endif
        
        protected override void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.SetSizeDelta(value, _mode);
        }
    }
}