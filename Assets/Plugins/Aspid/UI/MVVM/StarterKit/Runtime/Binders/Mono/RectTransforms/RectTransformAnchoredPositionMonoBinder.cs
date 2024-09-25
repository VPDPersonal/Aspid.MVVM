using UnityEngine;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.RectTransforms
{
    [AddComponentMenu("UI/Binders/Transform/Rect Transform Binder - Anchored Position")]
    public partial class RectTransformAnchoredPositionMonoBinder : ComponentMonoBinder<RectTransform>, IVectorBinder
    {
        [field: Header("Converter")]
        [field: SerializeReference]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [field: SerializeReferenceDropdown]
#endif
        protected IConverterVector3ToVector3 Converter { get; private set; }
        
        [BinderLog]
        public void SetValue(Vector2 value) =>
            CachedComponent.anchoredPosition = Converter?.Convert(value) ?? value;

        [BinderLog]
        public void SetValue(Vector3 value) =>
            CachedComponent.anchoredPosition3D = Converter?.Convert(value) ?? value;
    }
}