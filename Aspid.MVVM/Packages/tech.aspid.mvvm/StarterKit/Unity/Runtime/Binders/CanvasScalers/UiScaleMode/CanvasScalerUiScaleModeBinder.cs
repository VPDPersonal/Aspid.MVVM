#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;CanvasScaler, CanvasScaler.ScaleMode&gt;</see> that binds
    /// <see cref="CanvasScaler.uiScaleMode"/>.
    /// </summary>
    [Serializable]
    public class CanvasScalerUiScaleModeBinder : TargetBinder<CanvasScaler, CanvasScaler.ScaleMode>
    {
        /// <inheritdoc/>
        protected sealed override CanvasScaler.ScaleMode Property
        {
            get => Target.uiScaleMode;
            set => Target.uiScaleMode = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public CanvasScalerUiScaleModeBinder(CanvasScaler target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
