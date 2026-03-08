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
    /// MonoBehaviour binder that sets the <see cref="Image.fillAmount"/> property on a group of <see cref="Image"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill EnumGroup")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillAmount", SubPath = "EnumGroup")]
    public sealed class ImageFillEnumGroupMonoBinder : EnumGroupMonoBinder<Image, float, Converter>
    {
        protected override void SetValue(Image element, float value) =>
            element.fillAmount = Mathf.Clamp01(value);
    }
}