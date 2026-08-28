#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TTarget,T}">SwitcherBinder&lt;HorizontalOrVerticalLayoutGroup, float&gt;</see> that switches the
    /// <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> property between two values
    /// based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-HorizontalOrVerticalLayout-Spacing-1.1.0.xml" path="doc//member[@name='HorizontalOrVerticalLayoutSpacingSwitcherBinder']/*" />
    [Serializable]
    public sealed class HorizontalOrVerticalLayoutSpacingSwitcherBinder : SwitcherBinder<HorizontalOrVerticalLayoutGroup, float>
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
        /// Sets <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> to <paramref name="value"/> if it is finite.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value)
        {
            if (!this.RequireFinite(value, Target)) return;
            Target.spacing = value;
        }
    }
}