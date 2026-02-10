using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/HorizontalOrVertical/HorizontalOrVerticalLayoutGroup Binder – Spacing")]
    public partial class HorizontalOrVerticalLayoutSpacingMonoBinder : ComponentMonoBinder<HorizontalOrVerticalLayoutGroup>, INumberBinder
    {
        [BinderLog]
        public void SetValue(int value) =>
            CachedComponent.spacing = value;

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((int)value);

        [BinderLog]
        public void SetValue(float value) =>
            CachedComponent.spacing = value;

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}