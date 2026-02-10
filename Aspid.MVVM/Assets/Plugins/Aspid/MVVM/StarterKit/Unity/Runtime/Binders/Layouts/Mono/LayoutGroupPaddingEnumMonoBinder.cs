using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup Binder – Padding Enum")]
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding", SubPath = "Enum")]
    public sealed class LayoutGroupPaddingEnumMonoBinder : EnumMonoBinder<LayoutGroup, RectOffset>
    {
        [SerializeField] private PaddingMode _paddingMode;
        
        protected override void SetValue(RectOffset value) =>
            CachedComponent.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
    }
}