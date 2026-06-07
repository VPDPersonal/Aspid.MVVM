using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupVector3MonoBinder{RectTransform}"/> that sets the <see cref="RectTransform.anchoredPosition"/> or
    /// <see cref="RectTransform.anchoredPosition3D"/> on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(RectTransform), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – AnchoredPosition EnumGroup")]
    public sealed class RectTransformAnchoredPositionEnumGroupMonoBinder : EnumGroupVector3MonoBinder<RectTransform>
    {
        [Tooltip("The space that determines which anchored position property is used: Self for anchoredPosition, World for anchoredPosition3D.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets the anchored position of the element in the configured <see cref="Space"/>.
        /// </summary>
        protected override void SetValue(RectTransform element, Vector3 value) =>
            element.SetAnchoredPosition(value, _space);
    }
}