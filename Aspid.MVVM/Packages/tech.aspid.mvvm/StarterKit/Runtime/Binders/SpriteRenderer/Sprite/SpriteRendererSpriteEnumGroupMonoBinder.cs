using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="SpriteRenderer.sprite"/> on each
    /// element.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_Sprite", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Sprite EnumGroup")]
    public sealed class SpriteRendererSpriteEnumGroupMonoBinder : EnumGroupMonoBinder<SpriteRenderer, Sprite>
    {
        /// <inheritdoc/>
        protected override void SetValue(SpriteRenderer element, Sprite value) =>
            element.sprite = value;
    }
}
