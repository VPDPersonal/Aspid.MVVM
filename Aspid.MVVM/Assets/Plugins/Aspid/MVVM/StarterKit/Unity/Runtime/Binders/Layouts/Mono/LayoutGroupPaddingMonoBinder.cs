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
    /// MonoBehaviour binder that sets the <see cref="UnityEngine.UI.LayoutGroup.padding"/> on a <see cref="UnityEngine.UI.LayoutGroup"/>
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup Binder – Padding")]
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding")]
    public partial class LayoutGroupPaddingMonoBinder : ComponentMonoBinder<LayoutGroup, RectOffset, Converter>, INumberBinder
    {
        [SerializeField] private PaddingMode _paddingMode;
        private RectOffset _cachedRectOffset;

        protected sealed override RectOffset Property
        {
            get => CachedComponent.padding;
            set => CachedComponent.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
        }

        [BinderLog]
        public void SetValue(int value)
        {
            _cachedRectOffset ??= new RectOffset();
            _cachedRectOffset.left = value;
            _cachedRectOffset.right = value;
            _cachedRectOffset.top = value;
            _cachedRectOffset.bottom = value;

            base.SetValue(_cachedRectOffset);
        }

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