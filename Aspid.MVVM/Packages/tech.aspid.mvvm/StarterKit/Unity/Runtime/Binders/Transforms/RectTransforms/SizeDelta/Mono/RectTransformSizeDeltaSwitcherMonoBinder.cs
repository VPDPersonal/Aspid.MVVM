using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{RectTransform, Vector3}"/> that switches the <see cref="RectTransform.sizeDelta"/>
    /// between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(RectTransform), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – SizeDelta Switcher")]
    public sealed class RectTransformSizeDeltaSwitcherMonoBinder : SwitcherMonoBinder<RectTransform, Vector3>
    {
        [Tooltip("Determines which axes of sizeDelta are modified: Width only, Height only, or both (SizeDelta).")]
        [SerializeField] private SizeDeltaMode _sizeMode = SizeDeltaMode.SizeDelta;

        /// <summary>
        /// Called when applying the selected value to the <see cref="RectTransform.sizeDelta"/>.
        /// </summary>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetSizeDelta(value, _sizeMode);
    }
}