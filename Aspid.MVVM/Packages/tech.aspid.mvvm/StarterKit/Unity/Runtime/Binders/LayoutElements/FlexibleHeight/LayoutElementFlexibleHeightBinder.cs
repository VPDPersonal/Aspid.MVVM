#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{LayoutElement}"/> that binds <see cref="LayoutElement.flexibleHeight"/>.
    /// </summary>
    /// <remarks>A non-finite value is rejected instead of being written.</remarks>
    [Serializable]
    public class LayoutElementFlexibleHeightBinder : TargetFloatBinder<LayoutElement>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.flexibleHeight;
            set
            {
                if (!this.RequireFinite(value, Target)) return;
                Target.flexibleHeight = value;
            }
        }

        /// <inheritdoc/>
        public LayoutElementFlexibleHeightBinder(
            LayoutElement target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
