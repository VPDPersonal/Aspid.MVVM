#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;TMP_Text, Vector4&gt;</see> that binds
    /// <see cref="TMP_Text.margin"/>.
    /// </summary>
    /// <remarks>
    /// Component order is <c>(left, top, right, bottom)</c>. A non-finite component is ignored — TMP lays the
    /// text out from these four numbers and one <see cref="float.NaN"/> collapses the whole block.
    /// </remarks>
    [Serializable]
    public class TextMarginBinder : TargetBinder<TMP_Text, Vector4>
    {
        /// <inheritdoc/>
        protected sealed override Vector4 Property
        {
            get => Target.margin;
            set
            {
                if (!this.RequireFinite(value, Target)) return;
                Target.margin = value;
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public TextMarginBinder(TMP_Text target, IConverter<Vector4, Vector4>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
#endif
