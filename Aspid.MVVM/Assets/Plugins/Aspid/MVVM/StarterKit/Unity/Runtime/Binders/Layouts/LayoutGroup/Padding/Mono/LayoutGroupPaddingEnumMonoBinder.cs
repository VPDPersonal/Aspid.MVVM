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
    /// <see cref="EnumMonoBinder{LayoutGroup, RectOffset, Converter}"/> that sets the
    /// <see cref="UnityEngine.UI.LayoutGroup.padding"/> property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The affected padding sides are determined by the configured <see cref="PaddingMode"/>.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup Binder – Padding Enum")]
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding", SubPath = "Enum")]
    public sealed class LayoutGroupPaddingEnumMonoBinder : EnumMonoBinder<LayoutGroup, RectOffset, Converter>
    {
        [Tooltip("Determines which sides of the padding are updated when a value is applied.")]
        [SerializeField] private PaddingMode _paddingMode;

        protected override void SetValue(RectOffset value) =>
            CachedComponent.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
    }
}