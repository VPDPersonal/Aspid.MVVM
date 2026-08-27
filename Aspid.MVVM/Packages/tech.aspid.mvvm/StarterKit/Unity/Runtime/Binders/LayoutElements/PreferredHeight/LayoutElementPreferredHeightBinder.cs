#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{LayoutElement}"/> that binds <see cref="LayoutElement.preferredHeight"/>.
    /// </summary>
    /// <remarks>
    /// A negative value means "no preference". A non-finite value is rejected instead of being written.
    /// </remarks>
    [Serializable]
    public class LayoutElementPreferredHeightBinder : TargetFloatBinder<LayoutElement>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.preferredHeight;
            set
            {
                if (!BinderMath.IsFinite(value)) return;
                Target.preferredHeight = value;
            }
        }

        /// <inheritdoc/>
        public LayoutElementPreferredHeightBinder(
            LayoutElement target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
