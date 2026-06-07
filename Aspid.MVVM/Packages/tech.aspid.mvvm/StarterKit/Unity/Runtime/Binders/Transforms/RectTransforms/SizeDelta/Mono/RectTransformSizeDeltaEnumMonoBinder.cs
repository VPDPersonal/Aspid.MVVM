using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumVector3MonoBinder{RectTransform}"/> that sets the <see cref="RectTransform.sizeDelta"/>
    /// according to the configured <see cref="SizeDeltaMode"/> based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(RectTransform), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – SizeDelta Enum")]
    public sealed class RectTransformSizeDeltaEnumMonoBinder : EnumVector3MonoBinder<RectTransform>
    {
        [Tooltip("Determines which axes of sizeDelta are modified: Width only, Height only, or both (SizeDelta).")]
        [SerializeField] private SizeDeltaMode _sizeMode = SizeDeltaMode.SizeDelta;

        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets the <see cref="RectTransform.sizeDelta"/> according to the configured <see cref="SizeDeltaMode"/>.
        /// </summary>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetSizeDelta(value, _sizeMode);
    }
}