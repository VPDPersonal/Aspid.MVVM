using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector2;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(RectTransform), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – SizeDelta Enum")]
    public sealed class RectTransformSizeDeltaEnumMonoBinder : EnumMonoBinder<RectTransform, Vector2>
    {
        [SerializeField] private SizeDeltaMode _sizeMode = SizeDeltaMode.SizeDelta;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.SetSizeDelta(value, _sizeMode);
        }
    }
}