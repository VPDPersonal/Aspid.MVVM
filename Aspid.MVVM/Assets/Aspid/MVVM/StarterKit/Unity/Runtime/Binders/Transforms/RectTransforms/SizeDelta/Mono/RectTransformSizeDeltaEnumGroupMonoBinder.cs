using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupVector3MonoBinder{RectTransform}"/> that sets the <see cref="RectTransform.sizeDelta"/>
    /// on each element in the group according to the configured <see cref="SizeDeltaMode"/> based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(RectTransform), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – SizeDelta EnumGroup")]
    public sealed class RectTransformSizeDeltaEnumGroupMonoBinder : EnumGroupVector3MonoBinder<RectTransform>
    {
        [Tooltip("Determines which axes of sizeDelta are modified: Width only, Height only, or both (SizeDelta).")]
        [SerializeField] private SizeDeltaMode _sizeMode = SizeDeltaMode.SizeDelta;

        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets the <see cref="RectTransform.sizeDelta"/> of the element according to the configured <see cref="SizeDeltaMode"/>.
        /// </summary>
        protected override void SetValue(RectTransform element, Vector3 value) => 
            element.SetSizeDelta(value, _sizeMode);
    }
}