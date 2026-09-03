using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="RectTransform.sizeDelta"/>.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_SizeDelta", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – SizeDelta Enum")]
    public sealed class RectTransformSizeDeltaEnumMonoBinder : EnumMonoBinder<RectTransform, Vector3>
    {
        [Tooltip("Which axes of sizeDelta are written.")]
        [SerializeField] private SizeDeltaMode _sizeMode = SizeDeltaMode.SizeDelta;

        /// <inheritdoc/>
        protected override void SetValue(Vector3 value) =>
            CachedComponent.SetSizeDelta(value, _sizeMode);
    }
}
