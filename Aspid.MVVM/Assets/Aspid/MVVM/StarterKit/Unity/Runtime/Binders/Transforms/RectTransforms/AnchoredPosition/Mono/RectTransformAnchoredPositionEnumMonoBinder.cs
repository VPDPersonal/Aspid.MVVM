using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumVector3MonoBinder{RectTransform}"/> that sets the <see cref="RectTransform.anchoredPosition"/> or
    /// <see cref="RectTransform.anchoredPosition3D"/> property based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(RectTransform), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – AnchoredPosition Enum")]
    public sealed class RectTransformAnchoredPositionEnumMonoBinder : EnumVector3MonoBinder<RectTransform>
    {
        [Tooltip("The space that determines which anchored position property is used: Self for anchoredPosition, World for anchoredPosition3D.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets the anchored position of the <see cref="RectTransform"/> in the configured <see cref="Space"/>.
        /// </summary>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetAnchoredPosition(value, _space);
    }
}