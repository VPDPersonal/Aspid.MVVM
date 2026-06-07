using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector3MonoBinder{RectTransform}"/> that sets the <see cref="RectTransform.anchoredPosition"/> or
    /// <see cref="RectTransform.anchoredPosition3D"/> property depending on the configured <see cref="Space"/>.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current anchored position
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(RectTransform))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – AnchoredPosition")]
    public class RectTransformAnchoredPositionMonoBinder : ComponentVector3MonoBinder<RectTransform>
    {
        [Tooltip("The space that determines which anchored position property is used: Self for anchoredPosition, World for anchoredPosition3D.")]
        [SerializeField] private Space _space = Space.World;

        protected sealed override Vector3 Property
        {
            get => CachedComponent.GetAnchoredPosition(_space);
            set => CachedComponent.SetAnchoredPosition(value, _space);
        }
    }
}