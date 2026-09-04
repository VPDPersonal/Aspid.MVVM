using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="Image.fillClockwise"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillClockwise")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill Clockwise")]
    public class ImageFillClockwiseMonoBinder : ComponentMonoBinder<Image, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.fillClockwise;
            set => CachedComponent.fillClockwise = value;
        }
    }
}
