using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector3MonoBinder{RectTransform}"/> that sets the <see cref="RectTransform.anchoredPosition"/> or
    /// <see cref="RectTransform.anchoredPosition3D"/> property depending on the configured <see cref="Space"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(RectTransform))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – AnchoredPosition")]
    public class RectTransformAnchoredPositionMonoBinder : ComponentVector3MonoBinder<RectTransform>
    {
        [Tooltip("Which property is written: Self → anchoredPosition, World → anchoredPosition3D.")]
        [SerializeField] private Space _space = Space.World;

        protected sealed override Vector3 Property
        {
            get => CachedComponent.GetAnchoredPosition(_space);
            set => CachedComponent.SetAnchoredPosition(value, _space);
        }
    }
}