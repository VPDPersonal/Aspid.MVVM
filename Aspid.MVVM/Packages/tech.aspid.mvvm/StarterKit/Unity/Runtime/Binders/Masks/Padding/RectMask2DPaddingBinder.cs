#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{RectMask2D, Vector3}"/> that binds <see cref="RectMask2D.padding"/>.
    /// </summary>
    /// <remarks>
    /// The property is a <see cref="Vector4"/>; the fourth component keeps its previous value since only
    /// <see cref="Vector3"/> is bound. Non-finite components are refused.
    /// </remarks>
    [Serializable]
    public class RectMask2DPaddingBinder : TargetBinder<RectMask2D, Vector3>, IVector3Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector3 Property
        {
            get => Target.padding;
            set
            {
                if (!this.RequireFinite(value, Target)) return;
                var padding = Target.padding;
                Target.padding = new Vector4(value.x, value.y, value.z, padding.w);
            }
        }

        /// <inheritdoc/>
        public RectMask2DPaddingBinder(
            RectMask2D target,
            IConverter<Vector3, Vector3>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
