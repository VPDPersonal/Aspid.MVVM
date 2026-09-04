using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="LayoutGroup.padding"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup Binder – Padding Enum")]
    public sealed class LayoutGroupPaddingEnumMonoBinder : EnumMonoBinder<LayoutGroup, RectOffset>
    {
        [Tooltip("Padding sides the value writes.")]
        [SerializeField] private RectSides _sides = RectSides.All;

        /// <inheritdoc/>
        protected override void SetValue(RectOffset value) =>
            CachedComponent.SetPadding(value, _sides);
    }
}
