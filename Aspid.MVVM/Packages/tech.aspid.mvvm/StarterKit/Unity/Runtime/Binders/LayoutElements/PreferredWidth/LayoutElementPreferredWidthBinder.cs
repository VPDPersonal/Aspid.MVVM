#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{LayoutElement}"/> that binds <see cref="LayoutElement.preferredWidth"/>.
    /// </summary>
    /// <remarks>
    /// A LayoutElement is how a single child overrides what its layout group would otherwise decide, and none of its numbers could be bound. A negative value means "no preference", which is why it is passed through rather than clamped.
    /// </remarks>
    [Serializable]
    public class LayoutElementPreferredWidthBinder : TargetFloatBinder<LayoutElement>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.preferredWidth;
            set => Target.preferredWidth = value;
        }

        /// <inheritdoc/>
        public LayoutElementPreferredWidthBinder(
            LayoutElement target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
