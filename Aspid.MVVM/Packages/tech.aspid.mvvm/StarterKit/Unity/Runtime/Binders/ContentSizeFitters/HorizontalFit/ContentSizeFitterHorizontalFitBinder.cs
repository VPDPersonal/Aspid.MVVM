#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;ContentSizeFitter, ContentSizeFitter.FitMode&gt;</see> that binds
    /// <see cref="ContentSizeFitter.horizontalFit"/>.
    /// </summary>
    /// <remarks>
    /// Whether the element sizes itself to its content on this axis. A dialog that grows to fit a message
    /// and then stops growing has to turn the fit off, and turning it off was not bindable —
    /// <see cref="ContentSizeFitter.FitMode.Unconstrained"/> is what hands the axis back to the layout.
    /// </remarks>
    [Serializable]
    public class ContentSizeFitterHorizontalFitBinder : TargetBinder<ContentSizeFitter, ContentSizeFitter.FitMode>
    {
        /// <inheritdoc/>
        protected sealed override ContentSizeFitter.FitMode Property
        {
            get => Target.horizontalFit;
            set => Target.horizontalFit = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public ContentSizeFitterHorizontalFitBinder(ContentSizeFitter target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
