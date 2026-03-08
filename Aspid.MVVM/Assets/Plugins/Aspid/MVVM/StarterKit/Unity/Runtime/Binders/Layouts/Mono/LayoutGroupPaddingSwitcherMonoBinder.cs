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
    /// MonoBehaviour binder that switches the <see cref="UnityEngine.UI.LayoutGroup.padding"/> on a <see cref="UnityEngine.UI.LayoutGroup"/>
    /// between two values based on a bound boolean ViewModel property.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup Binder – Padding Switcher")]
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding", SubPath = "Switcher")]
    public sealed class LayoutGroupPaddingSwitcherMonoBinder : SwitcherMonoBinder<LayoutGroup, RectOffset, Converter>
    {
        [SerializeField] private PaddingMode _paddingMode;

        protected override void SetValue(RectOffset value) =>
            CachedComponent.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
    }
}