using UnityEngine;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.RectTransforms
{
    [AddComponentMenu("UI/Binders/Transform/Rect Transform Binder - Anchored Position")]
    public partial class RectTransformAnchoredPositionMonoBinder : ComponentMonoBinder<RectTransform>, IVectorBinder
    {
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterVector3ToVector3 _converter;
        
        [BinderLog]
        public void SetValue(Vector2 value) =>
            CachedComponent.anchoredPosition = _converter?.Convert(value) ?? value;

        [BinderLog]
        public void SetValue(Vector3 value) =>
            CachedComponent.anchoredPosition3D = _converter?.Convert(value) ?? value;
    }
}