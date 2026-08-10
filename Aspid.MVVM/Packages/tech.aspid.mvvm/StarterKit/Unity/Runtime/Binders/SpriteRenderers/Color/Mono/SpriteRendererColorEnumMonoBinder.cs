using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{SpriteRenderer, Color}"/> that sets <see cref="SpriteRenderer.color"/>
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_Color", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Color Enum")]
    public sealed class SpriteRendererColorEnumMonoBinder : EnumMonoBinder<SpriteRenderer, Color>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// </summary>
        protected override void SetValue(Color value) =>
            CachedComponent.color = value;
    }
}
