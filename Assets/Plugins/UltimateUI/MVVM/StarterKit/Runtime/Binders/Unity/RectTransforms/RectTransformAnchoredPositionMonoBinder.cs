using UnityEngine;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.RectTransforms
{
    [AddComponentMenu("UI/Binders/Transform/Rect Transform Binder - Anchored Position")]
    public partial class RectTransformAnchoredPositionMonoBinder : ComponentMonoBinder<RectTransform>, IVectorBinder
    {
        [field: Header("Converter")]
        [field: SerializeReference]
        protected IConverterVector3ToVector3 Converter { get; private set; }
        
        [BinderLog]
        public void SetValue(Vector2 value) =>
            CachedComponent.anchoredPosition = Converter?.Convert(value) ?? value;

        [BinderLog]
        public void SetValue(Vector3 value) =>
            CachedComponent.anchoredPosition3D = Converter?.Convert(value) ?? value;
    }
}