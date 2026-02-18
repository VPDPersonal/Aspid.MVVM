using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(RectTransform))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – SizeDelta")]
    public class RectTransformSizeDeltaMonoBinder : ComponentVector3MonoBinder<RectTransform>
    {
        [SerializeField] private SizeDeltaMode _sizeMode = SizeDeltaMode.SizeDelta;

        protected sealed override Vector3 Property
        {
            get => CachedComponent.sizeDelta;
            set => CachedComponent.SetSizeDelta(value, _sizeMode);
        }
    }
}