#nullable enable
using System;
using UnityEngine;
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
        protected sealed override AspectRatioFitter.AspectMode Property
        {
            get => Target.aspectMode;
            set => Target.aspectMode = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public AspectRatioFitterAspectModeBinder(AspectRatioFitter target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
