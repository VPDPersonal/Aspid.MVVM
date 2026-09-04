using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="SpriteRenderer.color"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_Color", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Color Enum")]
    public sealed class SpriteRendererColorEnumMonoBinder : EnumMonoBinder<SpriteRenderer, Color>
    {
        /// <inheritdoc/>
        protected override void SetValue(Color value) =>
            CachedComponent.color = value;
    }
}
