using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{Image}"/> that binds <see cref="Image.preserveAspect"/>.
    /// </summary>
    /// <remarks>
    /// Whether the sprite keeps its proportions inside the rect. It matters exactly when the sprite is not known
    /// in advance — an avatar, a downloaded banner, a card art of any shape.
    /// </remarks>
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_PreserveAspect")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Preserve Aspect")]
    public class ImagePreserveAspectMonoBinder : ComponentBoolMonoBinder<Image>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.preserveAspect;
            set => CachedComponent.preserveAspect = value;
        }
    }
}
