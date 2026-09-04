using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="RawImage.texture"/>, also from a
    /// <see cref="Sprite"/>.
    /// </summary>
    /// <remarks>
    /// Optionally disables the <see cref="RawImage"/> while the texture is <see langword="null"/>.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(RawImage), serializePropertyNames: "m_Texture")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RawImage/RawImage Binder – Texture")]
    public partial class RawImageTextureMonoBinder : ComponentMonoBinder<RawImage, Texture>, IBinder<Sprite>
    {
        [Tooltip("Disable the RawImage while the texture is null.")]
        [SerializeField] private bool _disabledWhenNull = true;

        /// <inheritdoc/>
        protected sealed override Texture Property
        {
            get => CachedComponent.texture;
            set => CachedComponent.SetTexture(value, _disabledWhenNull);
        }

        /// <summary>
        /// Shows the sprite's texture; <see langword="null"/> clears the texture.
        /// </summary>
        /// <param name="value">The sprite received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Sprite value) =>
            SetValue(value?.texture);
    }
}
