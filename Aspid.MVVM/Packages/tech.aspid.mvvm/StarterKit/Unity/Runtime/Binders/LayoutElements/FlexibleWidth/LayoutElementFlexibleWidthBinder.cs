#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder<LayoutElement>"/> that binds <see cref="LayoutElement.flexibleWidth"/>.
    /// </summary>
    /// <remarks>
    /// How much of the leftover space this child claims, relative to its siblings.
    /// </remarks>
    [Serializable]
    public class LayoutElementFlexibleWidthBinder : TargetFloatBinder<LayoutElement>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.flexibleWidth;
            set => Target.flexibleWidth = value;
        }

        /// <inheritdoc/>
        public LayoutElementFlexibleWidthBinder(
            LayoutElement target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
