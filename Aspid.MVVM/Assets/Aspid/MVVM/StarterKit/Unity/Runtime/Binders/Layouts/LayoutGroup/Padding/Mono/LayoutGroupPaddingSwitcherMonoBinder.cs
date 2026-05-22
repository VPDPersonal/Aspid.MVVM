using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.RectOffset, UnityEngine.RectOffset>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterRectOffset;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{LayoutGroup, RectOffset, Converter}"/> that switches the
    /// <see cref="UnityEngine.UI.LayoutGroup.padding"/> property between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The affected padding sides are determined by the configured <see cref="PaddingMode"/>.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup Binder – Padding Switcher")]
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding", SubPath = "Switcher")]
    public sealed class LayoutGroupPaddingSwitcherMonoBinder : SwitcherMonoBinder<LayoutGroup, RectOffset, Converter>
    {
        [Tooltip("Determines which sides of the padding are updated when a value is applied.")]
        [SerializeField] private PaddingMode _paddingMode;

        protected override void SetValue(RectOffset value) =>
            CachedComponent.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
    }
}