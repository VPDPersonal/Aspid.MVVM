using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="RectTransform.sizeDelta"/>
    /// on each element.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_SizeDelta", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – SizeDelta EnumGroup")]
    public sealed class RectTransformSizeDeltaEnumGroupMonoBinder : EnumGroupMonoBinder<RectTransform, Vector3>
    {
        [Tooltip("Which axes of sizeDelta are written.")]
        [SerializeField] private SizeDeltaMode _sizeMode = SizeDeltaMode.SizeDelta;

        /// <inheritdoc/>
        protected override void SetValue(RectTransform element, Vector3 value) =>
            element.SetSizeDelta(value, _sizeMode);
    }
}
