using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Layout/LayoutGroup Binder – Padding")]
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding")]
    public partial class LayoutGroupPaddingMonoBinder : ComponentMonoBinder<LayoutGroup>, IBinder<RectOffset>, INumberBinder
    {
        [SerializeField] private PaddingMode _paddingMode;
        
        [BinderLog]
        public void SetValue(RectOffset value) =>
            CachedComponent.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);

        [BinderLog]
        public void SetValue(int value) =>
            CachedComponent.SetPadding(top: value, right: value, bottom: value, left: value, _paddingMode);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((int)value);

        [BinderLog]
        public void SetValue(float value) =>
            SetValue((int)value);

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((int)value);
    }
}