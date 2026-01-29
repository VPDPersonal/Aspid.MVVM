using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Layout/LayoutGroup Binder – Padding Switcher")]
    public sealed class LayoutGroupPaddingSwitcherMonoBinder : SwitcherMonoBinder<LayoutGroup, RectOffset>
    {
        [SerializeField] private PaddingMode _paddingMode;
        
        protected override void SetValue(RectOffset value) =>
            CachedComponent.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
    }
}