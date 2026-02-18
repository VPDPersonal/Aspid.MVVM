using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(RectTransform))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – AnchoredPosition")]
    public class RectTransformAnchoredPositionMonoBinder : ComponentVector3MonoBinder<RectTransform>
    {
        [SerializeField] private Space _space = Space.World;

        protected sealed override Vector3 Property
        {
            get => CachedComponent.GetAnchoredPosition(_space);
            set => CachedComponent.SetAnchoredPosition(value, _space);
        }
    }
}