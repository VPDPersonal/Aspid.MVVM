using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="LayoutGroup.padding"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup Binder – Padding Switcher")]
    public sealed class LayoutGroupPaddingSwitcherMonoBinder : SwitcherMonoBinder<LayoutGroup, RectOffset>
    {
        [Tooltip("Padding sides the value writes.")]
        [SerializeField] private RectSides _sides = RectSides.All;

        /// <inheritdoc/>
        protected override void SetValue(RectOffset value) =>
            CachedComponent.SetPadding(value, _sides);
    }
}
