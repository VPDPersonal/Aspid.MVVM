using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class ImageExtensions
    {
        #region Image
        /// <summary>
        /// Sets <see cref="Image.image"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The texture to display in this image. If you assign a Texture, the Image element will resize and show the assigned texture.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The texture to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetImage<T>(this T element, Texture value)
            where T : Image
        {
            element.image = value;
            return element;
        }

        /// <summary>
        /// Loads a <see cref="Texture"/> from Resources and sets the <see cref="Image.image"/> property.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="path">The Resources path of the texture to load.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetImageFromResource<T>(this T element, string path)
            where T : Image
        {
            return element.SetImage(Resources.Load<Texture>(path));
        }
        #endregion

        #region Sprite
        /// <summary>
        /// Sets <see cref="Image.sprite"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The sprite to display in this image.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The sprite to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetSprite<T>(this T element, Sprite value)
            where T : Image
        {
            element.sprite = value;
            return element;
        }

        /// <summary>
        /// Loads a <see cref="Sprite"/> from Resources and sets the <see cref="Image.sprite"/> property.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="path">The Resources path of the sprite to load.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetSpriteFromResource<T>(this T element, string path)
            where T : Image
        {
            return element.SetSprite(Resources.Load<Sprite>(path));
        }
        #endregion

        #region VectorImage
        /// <summary>
        /// Sets <see cref="Image.vectorImage"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The VectorImage to display in this image.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The vector image to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetVectorImage<T>(this T element, VectorImage value)
            where T : Image
        {
            element.vectorImage = value;
            return element;
        }

        /// <summary>
        /// Loads a <see cref="VectorImage"/> from Resources and sets the <see cref="Image.vectorImage"/> property.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="path">The Resources path of the vector image to load.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetVectorImageFromResource<T>(this T element, string path)
            where T : Image
        {
            return element.SetVectorImage(Resources.Load<VectorImage>(path));
        }
        #endregion

        /// <summary>
        /// Sets <see cref="Image.uv"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The base texture coordinates of the Image relative to the bottom left corner.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The UV rect to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUv<T>(this T element, Rect value)
            where T : Image
        {
            element.uv = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="Image.sourceRect"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The source rectangle inside the texture relative to the top left corner.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The source rect to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetSourceRect<T>(this T element, Rect value)
            where T : Image
        {
            element.sourceRect = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="Image.tintColor"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Tinting color for this Image.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The tint color to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetTintColor<T>(this T element, Color value)
            where T : Image
        {
            element.tintColor = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="Image.scaleMode"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// ScaleMode used to display the Image.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The scale mode to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetScaleMode<T>(this T element, ScaleMode value)
            where T : Image
        {
            element.scaleMode = value;
            return element;
        }
    }
}
