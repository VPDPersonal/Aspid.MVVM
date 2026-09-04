using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="SpriteRenderer.sprite"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_Sprite", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Sprite Enum")]
    public sealed class SpriteRendererSpriteEnumMonoBinder : EnumMonoBinder<SpriteRenderer, Sprite>
    {
        /// <inheritdoc/>
        protected override void SetValue(Sprite value) =>
            CachedComponent.sprite = value;
    }
}
