using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="Image.fillAmount"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to [0, 1].
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillAmount")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill")]
    public class ImageFillMonoBinder : ComponentFloatMonoBinder<Image>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.fillAmount;
            set => CachedComponent.fillAmount = this.SafeClamp01(value);
        }
    }
}
