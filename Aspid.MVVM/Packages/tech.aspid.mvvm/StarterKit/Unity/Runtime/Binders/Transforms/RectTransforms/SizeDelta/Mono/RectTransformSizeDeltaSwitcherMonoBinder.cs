using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherVector3MonoBinder{RectTransform}"/> that switches the <see cref="RectTransform.sizeDelta"/>
    /// between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(RectTransform), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – SizeDelta Switcher")]
    public sealed class RectTransformSizeDeltaSwitcherMonoBinder : SwitcherVector3MonoBinder<RectTransform>
    {
        [Tooltip("Which axes of sizeDelta are modified.")]
        [SerializeField] private SizeDeltaMode _sizeMode = SizeDeltaMode.SizeDelta;

        /// <summary>
        /// Called when applying the selected value to the <see cref="RectTransform.sizeDelta"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetSizeDelta(value, _sizeMode);
    }
}