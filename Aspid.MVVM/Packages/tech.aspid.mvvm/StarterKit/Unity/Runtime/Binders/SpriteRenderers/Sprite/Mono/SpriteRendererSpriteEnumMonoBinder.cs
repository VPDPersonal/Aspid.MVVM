using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{SpriteRenderer, Sprite}"/> that sets <see cref="SpriteRenderer.sprite"/>
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_Sprite", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Sprite Enum")]
    public sealed class SpriteRendererSpriteEnumMonoBinder : EnumMonoBinder<SpriteRenderer, Sprite>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// </summary>
        protected override void SetValue(Sprite value) =>
            CachedComponent.sprite = value;
    }
}
