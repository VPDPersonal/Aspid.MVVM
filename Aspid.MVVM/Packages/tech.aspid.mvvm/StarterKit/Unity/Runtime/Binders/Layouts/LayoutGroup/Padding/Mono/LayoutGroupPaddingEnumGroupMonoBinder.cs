using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinderWithConverter{T1, T2}"/> that sets the
    /// <see cref="UnityEngine.UI.LayoutGroup.padding"/> property on each element based on the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The affected padding sides are determined by the configured <see cref="PaddingMode"/>.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup Binder – Padding EnumGroup")]
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding", SubPath = "EnumGroup")]
    public sealed class LayoutGroupPaddingEnumGroupMonoBinder : EnumGroupMonoBinderWithConverter<LayoutGroup, RectOffset>
    {
        [Tooltip("Which sides of the padding are updated per element.")]
        [SerializeField] private PaddingMode _paddingMode;
        
        protected override void SetValue(LayoutGroup element, RectOffset value) =>
            element.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
    }
}