using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="TMP_Text.margin"/>.
    /// </summary>
    /// <remarks>
    /// Components are left, top, right, bottom. Only a finite value is applied.
    /// </remarks>
    [GenerateSerializableBinder]
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
                if (this.RequireFinite(value))
                    CachedComponent.margin = value;
            }
        }
    }
}
