using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherVector3MonoBinder{RectTransform}"/> that switches the <see cref="RectTransform.anchoredPosition"/> or
    /// <see cref="RectTransform.anchoredPosition3D"/> between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(RectTransform), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – AnchoredPosition Switcher")]
    public sealed class RectTransformAnchoredPositionSwitcherMonoBinder : SwitcherVector3MonoBinder<RectTransform>
    {
        [Tooltip("Which property is written: Self → anchoredPosition, World → anchoredPosition3D.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Called when applying the selected value to the anchored position of the <see cref="RectTransform"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetAnchoredPosition(value, _space);
    }
}