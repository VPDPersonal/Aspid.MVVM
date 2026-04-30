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
    /// <see cref="SwitcherBinder{LayoutGroup, RectOffset, Converter}"/> that switches the <see cref="UnityEngine.UI.LayoutGroup.padding"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The affected padding sides are determined by the configured <see cref="PaddingMode"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-LayoutGroup-Padding-1.1.0.xml" path="doc//member[@name='LayoutGroupPaddingSwitcherBinder']/*" />
    [Serializable]
    public sealed class LayoutGroupPaddingSwitcherBinder : SwitcherBinder<LayoutGroup, RectOffset, Converter>
    {
        [Tooltip("Determines which sides of the padding are updated when a value is applied.")]
        [SerializeField] private PaddingMode _paddingMode;

        /// <summary>
        /// Initializes a new instance of <see cref="LayoutGroupPaddingSwitcherBinder"/> targeting the specified <see cref="LayoutGroup"/>.
        /// </summary>
        /// <param name="target">The <see cref="LayoutGroup"/> whose <see cref="UnityEngine.UI.LayoutGroup.padding"/> property is switched.</param>
        /// <param name="trueValue">The padding assigned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">The padding assigned when the bound value is <see langword="false"/>.</param>
        /// <param name="paddingMode">Determines which sides of the padding are updated.</param>
        /// <param name="converter">The converter used to transform the bound value, or <see langword="null"/> to use the default.</param>
        /// <param name="mode">The binding mode to use.</param>
        public LayoutGroupPaddingSwitcherBinder(
            LayoutGroup target,
            RectOffset trueValue,
            RectOffset falseValue,
            PaddingMode paddingMode,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode)
        {
            _paddingMode = paddingMode;
        }
        
        protected override void SetValue(RectOffset value) =>
            Target.SetPadding(value.top, value.right, value.bottom, value.left, _paddingMode);
    }
}