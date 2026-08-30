using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}">ComponentMonoBinder&lt;Image, Image.Type&gt;</see> that binds
    /// <see cref="Image.type"/>.
    /// </summary>
    [GenerateSerializableBinder]
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
