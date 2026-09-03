using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="RectTransform.anchoredPosition"/> or
    /// <see cref="RectTransform.anchoredPosition3D"/> on each element.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_AnchoredPosition", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – AnchoredPosition EnumGroup")]
    public sealed class RectTransformAnchoredPositionEnumGroupMonoBinder : EnumGroupMonoBinder<RectTransform, Vector3>
    {
        [Tooltip("Self: anchoredPosition, World: anchoredPosition3D.")]
        [SerializeField] private Space _space = Space.World;

        /// <inheritdoc/>
        protected override void SetValue(RectTransform element, Vector3 value) =>
            element.SetAnchoredPosition(value, _space);
    }
}
