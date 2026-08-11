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
    /// <remarks>
    /// Which part of the texture is shown, and how many times it repeats — a scrolling background, a sprite sheet
    /// frame, a minimap window.
    /// <para/>
    /// A non-finite component is refused: the quad's UVs are computed from these four numbers and one <c>NaN</c> makes
    /// the image vanish.
    /// </remarks>
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
