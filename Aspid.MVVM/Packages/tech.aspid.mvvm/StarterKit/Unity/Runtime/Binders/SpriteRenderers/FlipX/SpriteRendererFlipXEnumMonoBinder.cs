using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{T1, T2}">EnumMonoBinder&lt;SpriteRenderer, bool&gt;</see> that sets <see cref="SpriteRenderer.flipX"/>
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_FlipX", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Flip X Enum")]
    public sealed class SpriteRendererFlipXEnumMonoBinder : EnumMonoBinder<SpriteRenderer, bool>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(bool value) =>
            CachedComponent.flipX = value;
    }
}
