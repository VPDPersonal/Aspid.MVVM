#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatBinder{HorizontalOrVerticalLayoutGroup}"/> that switches the
    /// <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> property between two values
    /// based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-HorizontalOrVerticalLayout-Spacing-1.1.0.xml" path="doc//member[@name='HorizontalOrVerticalLayoutSpacingSwitcherBinder']/*" />
    [Serializable]
    public sealed class HorizontalOrVerticalLayoutSpacingSwitcherBinder : SwitcherFloatBinder<HorizontalOrVerticalLayoutGroup>
    {
        /// <inheritdoc/>
        public HorizontalOrVerticalLayoutSpacingSwitcherBinder(
            HorizontalOrVerticalLayoutGroup target,
            float trueValue,
            float falseValue,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <summary>
        /// Called when applying the selected spacing value to the <see cref="HorizontalOrVerticalLayoutGroup"/>.
        /// Sets <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> directly.
        /// </summary>
        protected override void SetValue(float value) =>
            Target.spacing = value;
    }
}