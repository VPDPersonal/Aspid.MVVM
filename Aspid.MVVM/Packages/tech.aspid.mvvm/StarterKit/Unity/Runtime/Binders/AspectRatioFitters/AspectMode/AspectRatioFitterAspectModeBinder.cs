#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;AspectRatioFitter, AspectRatioFitter.AspectMode&gt;</see> that binds
    /// <see cref="AspectRatioFitter.aspectMode"/>.
    /// </summary>
    [Serializable]
    public class AspectRatioFitterAspectModeBinder : TargetBinder<AspectRatioFitter, AspectRatioFitter.AspectMode>
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public AspectRatioFitterAspectModeBinder(AspectRatioFitter target, IConverter<AspectRatioFitter.AspectMode, AspectRatioFitter.AspectMode>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override AspectRatioFitter.AspectMode Property
        {
            get => Target.aspectMode;
            set => Target.aspectMode = value;
        }
    }
}
