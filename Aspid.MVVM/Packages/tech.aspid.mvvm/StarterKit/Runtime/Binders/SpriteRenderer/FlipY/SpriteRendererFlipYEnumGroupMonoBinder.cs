using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="SpriteRenderer.flipY"/> on each
    /// element.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_FlipY", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Flip Y EnumGroup")]
    public sealed class SpriteRendererFlipYEnumGroupMonoBinder : EnumGroupMonoBinder<SpriteRenderer, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(SpriteRenderer element, bool value) =>
            element.flipY = value;
    }
}
