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
        [Tooltip("Which property is written: Self → anchoredPosition, World → anchoredPosition3D.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets the anchored position of the element in the configured <see cref="Space"/>.
        /// </summary>
        /// <param name="element">The component this entry of the group writes to.</param>
        /// <param name="value">The value the bound enum resolved to for this element.</param>
        protected override void SetValue(RectTransform element, Vector3 value) =>
            element.SetAnchoredPosition(value, _space);
    }
}