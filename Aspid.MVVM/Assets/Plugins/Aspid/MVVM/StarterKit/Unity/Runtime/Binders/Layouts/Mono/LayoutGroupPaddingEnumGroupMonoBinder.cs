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
    /// MonoBehaviour binder that sets the <see cref="UnityEngine.UI.LayoutGroup.padding"/> property on a group of <see cref="UnityEngine.UI.LayoutGroup"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup Binder – Padding EnumGroup")]
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding", SubPath = "EnumGroup")]
    public sealed class LayoutGroupPaddingEnumGroupMonoBinder : EnumGroupMonoBinder<LayoutGroup, RectOffset, Converter>
    {
        [SerializeField] private PaddingMode _paddingMode;

        protected override void SetValue(LayoutGroup element, RectOffset value) =>
            element.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
    }
}