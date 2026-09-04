using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="SpriteRenderer.flipX"/> on each
    /// element.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_FlipX", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Flip X EnumGroup")]
    public sealed class SpriteRendererFlipXEnumGroupMonoBinder : EnumGroupMonoBinder<SpriteRenderer, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(SpriteRenderer element, bool value) =>
            element.flipX = value;
    }
}
