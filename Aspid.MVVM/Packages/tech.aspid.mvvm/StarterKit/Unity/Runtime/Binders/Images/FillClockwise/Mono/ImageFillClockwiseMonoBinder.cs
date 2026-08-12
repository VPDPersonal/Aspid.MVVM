using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{Image}"/> that binds <see cref="Image.fillClockwise"/>.
    /// </summary>
    /// <remarks>
    /// Which way a radial fill turns. Paired with <see cref="Image.fillAmount"/>, which the package already
    /// bound, it is the difference between a timer that winds down and one that winds up.
    /// </remarks>
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillClockwise")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill Clockwise")]
    public class ImageFillClockwiseMonoBinder : ComponentBoolMonoBinder<Image>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.fillClockwise;
            set => CachedComponent.fillClockwise = value;
        }
    }
}
