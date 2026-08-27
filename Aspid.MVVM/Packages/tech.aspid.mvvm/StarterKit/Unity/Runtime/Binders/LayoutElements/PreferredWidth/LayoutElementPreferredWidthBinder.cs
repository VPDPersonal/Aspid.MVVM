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
    /// A negative value means "no preference", which is why it is passed through rather than clamped. A
    /// non-finite value is rejected instead of being written.
    /// </remarks>
    [Serializable]
    public class LayoutElementPreferredWidthBinder : TargetFloatBinder<LayoutElement>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.preferredWidth;
            set
            {
                if (!BinderMath.IsFinite(value)) return;
                Target.preferredWidth = value;
            }
        }

        /// <inheritdoc/>
        public LayoutElementPreferredWidthBinder(
            LayoutElement target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
