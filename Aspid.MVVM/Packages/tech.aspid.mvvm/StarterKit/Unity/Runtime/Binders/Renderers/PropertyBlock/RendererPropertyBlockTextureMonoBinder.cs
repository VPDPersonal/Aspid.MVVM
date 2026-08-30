using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="RendererPropertyBlockMonoBinder{T}">RendererPropertyBlockMonoBinder&lt;Texture&gt;</see> that
    /// writes a texture shader property, and accepts a <see cref="Sprite"/> as well.
    /// </summary>
    /// <remarks>
    /// A destroyed or <see langword="null"/> texture is not written: a property block cannot clear an override, so
    /// the previous texture would stay instead of the shader falling back to the material's own.
    /// </remarks>
    [AddBinderContextMenu(typeof(Renderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/PropertyBlock Binder – Texture")]
    public partial class RendererPropertyBlockTextureMonoBinder : RendererPropertyBlockMonoBinder<Texture>, IBinder<Sprite>
    {
        /// <summary>
        /// Writes the texture backing <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Sprite value) =>
            SetValue(value ? value.texture : null);

        /// <inheritdoc/>
        protected override void Write(Texture value)
        {
            if (!value) return;
            Block.SetTexture(PropertyId, value);
        }
    }
}
