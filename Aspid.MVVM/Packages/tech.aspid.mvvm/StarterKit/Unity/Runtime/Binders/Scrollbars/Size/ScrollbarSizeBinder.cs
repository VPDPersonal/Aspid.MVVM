#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{Scrollbar}"/> that binds <see cref="Scrollbar.size"/>.
    /// </summary>
    /// <remarks>
    /// The fraction of the content the handle covers — how much of a list is on screen at once. A custom
    /// scrollbar that is not driven by a <see cref="ScrollRect"/> has to set it itself, and nothing could.
    /// Clamped to 0..1 before it is written: Unity clamps it silently anyway, and a non-finite value would
    /// leave the handle with no size at all.
    /// </remarks>
    [Serializable]
    public class ScrollbarSizeBinder : TargetFloatBinder<Scrollbar>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.size;
            set => Target.size = BinderMath.SafeClamp01(value);
        }

        /// <inheritdoc/>
        public ScrollbarSizeBinder(
            Scrollbar target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
