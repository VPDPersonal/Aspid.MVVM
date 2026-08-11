using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{Image}"/> that binds <see cref="Image.fillOrigin"/>.
    /// </summary>
    /// <remarks>
    /// Where a filled image starts filling from, as an index into the origin enum of the current
    /// <see cref="Image.fillMethod"/>. A cooldown that runs from the top and a cast bar that runs from the left
    /// differ only in this number.
    /// </remarks>
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillOrigin")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill Origin")]
    public class ImageFillOriginMonoBinder : ComponentIntMonoBinder<Image>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.fillOrigin;
            set => CachedComponent.fillOrigin = value;
        }
    }
}
