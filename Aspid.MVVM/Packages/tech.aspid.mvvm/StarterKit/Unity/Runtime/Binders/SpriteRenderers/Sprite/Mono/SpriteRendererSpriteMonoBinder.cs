using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder<SpriteRenderer, Sprite>"/> that binds <see cref="SpriteRenderer.sprite"/>.
    /// </summary>
    /// <remarks>
    /// The 2D counterpart of the Image sprite binders, which only ever covered uGUI.
    /// </remarks>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_Sprite")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Sprite")]
    public class SpriteRendererSpriteMonoBinder : ComponentMonoBinder<SpriteRenderer, Sprite>
    {
        /// <inheritdoc/>
        protected sealed override Sprite Property
        {
            get => CachedComponent.sprite;
            set => CachedComponent.sprite = value;
        }
    }
}
