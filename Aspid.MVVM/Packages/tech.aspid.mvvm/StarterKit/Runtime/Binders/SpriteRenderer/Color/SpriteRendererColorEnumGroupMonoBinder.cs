using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="SpriteRenderer.color"/> on each
    /// element.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_Color", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Color EnumGroup")]
    public sealed class SpriteRendererColorEnumGroupMonoBinder : EnumGroupMonoBinder<SpriteRenderer, Color>
    {
        /// <inheritdoc/>
        protected override void SetValue(SpriteRenderer element, Color value) =>
            element.color = value;
    }
}
