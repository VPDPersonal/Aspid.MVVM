using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="LayoutGroup.padding"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup Binder – Padding EnumGroup")]
    public sealed class LayoutGroupPaddingEnumGroupMonoBinder : EnumGroupMonoBinder<LayoutGroup, RectOffset>
    {
        [Tooltip("Padding sides the value writes.")]
        [SerializeField] private RectSides _sides = RectSides.All;

        /// <inheritdoc/>
        protected override void SetValue(LayoutGroup element, RectOffset value) =>
            element.SetPadding(value, _sides);
    }
}
