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
    /// The inset between the text and its own rect, as <c>(left, top, right, bottom)</c> — what makes room for an icon
    /// that appears next to a line, or for a scrollbar that shows up only when the text is long.
    /// <para/>
    /// A non-finite component is refused: TMP lays the text out from these four numbers and one <c>NaN</c> collapses
    /// the whole block.
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
                if (!IsFinite(value)) return;
                Target.margin = value;
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public TextMarginBinder(TMP_Text target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        private static bool IsFinite(Vector4 value) =>
            BinderMath.IsFinite(value.x) && BinderMath.IsFinite(value.y)
            && BinderMath.IsFinite(value.z) && BinderMath.IsFinite(value.w);
    }
}
#endif
