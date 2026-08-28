#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{HorizontalOrVerticalLayoutGroup}"/> that sets the <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> property.
    /// </summary>
    /// <remarks>
    /// A non-finite value is rejected instead of being written. Also implements <see cref="IFloatBinder"/>:
    /// numeric ViewModel values (int, long, float, double) are forwarded directly to
    /// <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-HorizontalOrVerticalLayout-Spacing-1.1.0.xml" path="doc//member[@name='HorizontalOrVerticalLayoutSpacingBinder']/*" />
    [Serializable]
    public class HorizontalOrVerticalLayoutSpacingBinder : TargetFloatBinder<HorizontalOrVerticalLayoutGroup>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.spacing;
            set
            {
                if (!this.RequireFinite(value, Target)) return;
                Target.spacing = value;
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public HorizontalOrVerticalLayoutSpacingBinder(
            HorizontalOrVerticalLayoutGroup target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}