using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Image.preserveAspect"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_PreserveAspect")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Preserve Aspect")]
    public class ImagePreserveAspectMonoBinder : ComponentMonoBinder<Image, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.preserveAspect;
            set => CachedComponent.preserveAspect = value;
        }
    }
}
