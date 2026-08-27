#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;RawImage, Rect&gt;</see> that binds
    /// <see cref="RawImage.uvRect"/>.
    /// </summary>
    /// <remarks>A non-finite component is refused, since a <c>NaN</c> in any of the four values makes the image vanish.</remarks>
    [Serializable]
    public class RawImageUvRectBinder : TargetBinder<RawImage, Rect>
    {
        /// <inheritdoc/>
        protected sealed override Rect Property
        {
            get => Target.uvRect;
            set
            {
                if (!IsFinite(value)) return;
                Target.uvRect = value;
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public RawImageUvRectBinder(RawImage target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        private static bool IsFinite(Rect value) =>
            BinderMath.IsFinite(value.x) && BinderMath.IsFinite(value.y)
            && BinderMath.IsFinite(value.width) && BinderMath.IsFinite(value.height);
    }
}
