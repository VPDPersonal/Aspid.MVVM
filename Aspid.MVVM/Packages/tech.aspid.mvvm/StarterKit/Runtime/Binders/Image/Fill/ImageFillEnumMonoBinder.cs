using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Image.fillAmount"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to [0, 1].
    /// </remarks>
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillAmount", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill Enum")]
    public sealed class ImageFillEnumMonoBinder : EnumMonoBinder<Image, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.fillAmount = this.SafeClamp01(value);
    }
}
