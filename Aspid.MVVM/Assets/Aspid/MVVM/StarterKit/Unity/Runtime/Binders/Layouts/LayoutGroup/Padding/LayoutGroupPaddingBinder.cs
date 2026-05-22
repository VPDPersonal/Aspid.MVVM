#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.RectOffset?, UnityEngine.RectOffset?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterRectOffset;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{LayoutGroup, RectOffset, Converter}"/> that sets the <see cref="UnityEngine.UI.LayoutGroup.padding"/> property.
    /// </summary>
    /// <remarks>
    /// The affected padding sides are determined by the configured <see cref="PaddingMode"/>.
    /// Also implements <see cref="INumberBinder"/>: numeric ViewModel values (int, long, float, double)
    /// are applied uniformly to all four padding sides before being forwarded to the layout group.
    /// </remarks>
    /// <include file="XmlExampleDoc-LayoutGroup-Padding-1.1.0.xml" path="doc//member[@name='LayoutGroupPaddingBinder']/*" />
    [Serializable]
    public class LayoutGroupPaddingBinder : TargetBinder<LayoutGroup, RectOffset, Converter>, INumberBinder
    {
        [Tooltip("Determines which sides of the padding are updated when a value is received from the ViewModel.")]
        [SerializeField] private PaddingMode _paddingMode;
        [NonSerialized] private RectOffset? _cachedRectOffset;

        /// <inheritdoc/>
        protected sealed override RectOffset Property
        {
            get => Target.padding;
            set => Target.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="LayoutGroupPaddingBinder"/> targeting the specified <see cref="LayoutGroup"/>.
        /// </summary>
        /// <param name="target">The <see cref="LayoutGroup"/> whose <see cref="UnityEngine.UI.LayoutGroup.padding"/> property is bound.</param>
        /// <param name="paddingMode">Determines which sides of the padding are updated.</param>
        /// <param name="converter">The converter used to transform the bound value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public LayoutGroupPaddingBinder(
            LayoutGroup target,
            PaddingMode paddingMode,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            _paddingMode = paddingMode;
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Sets all four padding sides to <paramref name="value"/> and applies them to the <see cref="LayoutGroup"/>.
        /// </summary>
        /// <param name="value">The uniform padding value to apply to all sides.</param>
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
        public void SetValue(long value) =>
            SetValue((int)value);

        /// <inheritdoc cref="SetValue(int)"/>
        public void SetValue(float value) =>
            SetValue((int)value);

        /// <inheritdoc cref="SetValue(int)"/>
        public void SetValue(double value) =>
            SetValue((int)value);
    }
}