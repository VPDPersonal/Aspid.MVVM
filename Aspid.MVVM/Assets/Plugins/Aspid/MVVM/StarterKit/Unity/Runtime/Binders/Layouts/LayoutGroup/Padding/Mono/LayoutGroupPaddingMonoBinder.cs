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
    /// <see cref="ComponentMonoBinder{LayoutGroup, RectOffset, Converter}"/> that binds the <see cref="UnityEngine.UI.LayoutGroup.padding"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current padding value
    /// is sent back to the ViewModel.
    /// The affected padding sides are determined by the configured <see cref="PaddingMode"/>.
    /// Also implements <see cref="INumberBinder"/>: numeric ViewModel values (int, long, float, double)
    /// are applied uniformly to all four padding sides before being forwarded to the layout group.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/LayoutGroup Binder – Padding")]
    [AddBinderContextMenu(typeof(LayoutGroup), serializePropertyNames: "m_Padding")]
    public partial class LayoutGroupPaddingMonoBinder : ComponentMonoBinder<LayoutGroup, RectOffset, Converter>, INumberBinder
    {
        [Tooltip("Determines which sides of the padding are updated when a value is received from the ViewModel.")]
        [SerializeField] private PaddingMode _paddingMode;
        private RectOffset _cachedRectOffset;

        /// <inheritdoc/>
        protected sealed override RectOffset Property
        {
            get => CachedComponent.padding;
            set => CachedComponent.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
        }

        /// <summary>
        /// Sets all four padding sides to <paramref name="value"/> and applies them to the <see cref="LayoutGroup"/>.
        /// </summary>
        /// <param name="value">The uniform padding value to apply to all sides.</param>
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

        /// <inheritdoc cref="SetValue(int)"/>
        [BinderLog]
        public void SetValue(long value) =>
            SetValue((int)value);

        /// <inheritdoc cref="SetValue(int)"/>
        [BinderLog]
        public void SetValue(float value) =>
            SetValue((int)value);

        /// <inheritdoc cref="SetValue(int)"/>
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((int)value);
    }
}