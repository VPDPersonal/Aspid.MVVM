using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Image.fillAmount"/> on each element.
    /// </summary>
    /// <remarks>
    /// The value is clamped to [0, 1].
    /// </remarks>
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillAmount", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill EnumGroup")]
    public sealed class ImageFillEnumGroupMonoBinder : EnumGroupMonoBinder<Image, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(Image element, float value) =>
            element.fillAmount = this.SafeClamp01(value);
    }
}
