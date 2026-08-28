#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}">ComponentMonoBinder&lt;TMP_Text, Vector4&gt;</see> that binds
    /// <see cref="TMP_Text.margin"/>.
    /// </summary>
    /// <remarks>
    /// Component order is <c>(left, top, right, bottom)</c>. A non-finite component is ignored — TMP lays the
    /// text out from these four numbers and one <see cref="float.NaN"/> collapses the whole block.
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
                if (!this.RequireFinite(value)) return;
                CachedComponent.margin = value;
            }
        }
    }
}
#endif
