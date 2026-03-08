using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="Image.fillAmount"/> property on an <see cref="Image"/> component
    /// to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill Enum")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillAmount", SubPath = "Enum")]
    public sealed class ImageFillEnumMonoBinder : EnumMonoBinder<Image, float, Converter>
    {
        protected override void SetValue(float value) =>
            CachedComponent.fillAmount = Mathf.Clamp01(value);
    }
}