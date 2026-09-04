using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{TComponent}"/> that binds <see cref="Image.fillOrigin"/>.
    /// </summary>
    /// <remarks>
    /// The value indexes the origin enum of the current <see cref="Image.fillMethod"/>.
    /// </remarks>
    [GenerateSerializableBinder]
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
