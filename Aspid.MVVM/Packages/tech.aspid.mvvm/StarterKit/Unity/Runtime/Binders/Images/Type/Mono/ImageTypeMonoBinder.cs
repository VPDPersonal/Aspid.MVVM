using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;Image, Image.Type&gt;</see> that binds
    /// <see cref="Image.type"/>.
    /// </summary>
    /// <remarks>
    /// Whether the sprite is drawn simple, sliced, tiled or filled. A bar that switches between a fill and a
    /// plain icon changes this, and so does a panel that must stop stretching its border.
    /// </remarks>
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_Type")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Type")]
    public class ImageTypeMonoBinder : ComponentMonoBinder<Image, Image.Type>
    {
        /// <inheritdoc/>
        protected sealed override Image.Type Property
        {
            get => CachedComponent.type;
            set => CachedComponent.type = value;
        }
    }
}
