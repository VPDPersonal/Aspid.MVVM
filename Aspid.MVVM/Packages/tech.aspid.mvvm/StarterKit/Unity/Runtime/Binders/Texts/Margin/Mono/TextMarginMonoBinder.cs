#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;TMP_Text, Vector4&gt;</see> that binds
    /// <see cref="TMP_Text.margin"/>.
    /// </summary>
    /// <remarks>
    /// The inset between the text and its own rect, as <c>(left, top, right, bottom)</c> — what makes room for an icon
    /// that appears next to a line, or for a scrollbar that shows up only when the text is long.
    /// <para/>
    /// A non-finite component is refused: TMP lays the text out from these four numbers and one <c>NaN</c> collapses
    /// the whole block.
    /// <para/>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current margin is sent back to
    /// the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_margin")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Margin")]
    public class TextMarginMonoBinder : ComponentMonoBinder<TMP_Text, Vector4>
    {
        /// <inheritdoc/>
        protected sealed override Vector4 Property
        {
            get => CachedComponent.margin;
            set
            {
                if (!IsFinite(value)) return;
                CachedComponent.margin = value;
            }
        }

        private static bool IsFinite(Vector4 value) =>
            BinderMath.IsFinite(value.x) && BinderMath.IsFinite(value.y)
            && BinderMath.IsFinite(value.z) && BinderMath.IsFinite(value.w);
    }
}
#endif
