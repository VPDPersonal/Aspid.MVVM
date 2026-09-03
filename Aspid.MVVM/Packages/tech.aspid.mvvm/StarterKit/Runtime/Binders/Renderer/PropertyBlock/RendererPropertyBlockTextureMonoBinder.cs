using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="RendererPropertyBlockMonoBinder{TValue}"/> that writes a texture shader property, also from
    /// a <see cref="Sprite"/>.
    /// </summary>
    /// <remarks>
    /// A property block cannot clear an override, so a missing texture is not written and the previous one
    /// stays.
    /// </remarks>
    [AddBinderContextMenu(typeof(Renderer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/PropertyBlock Binder – Texture")]
    public partial class RendererPropertyBlockTextureMonoBinder : RendererPropertyBlockMonoBinder<Texture>,
        IBinder<Sprite>
    {
        /// <summary>
        /// Writes the texture of <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The sprite received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Sprite value) =>
            SetValue(value ? value.texture : null);

        /// <inheritdoc/>
        protected override void Write(Texture value)
        {
            if (value)
                Block.SetTexture(PropertyId, value);
        }
    }
}
