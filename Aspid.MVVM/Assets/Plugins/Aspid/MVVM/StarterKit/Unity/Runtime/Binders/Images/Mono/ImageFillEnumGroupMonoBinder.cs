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
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill EnumGroup")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillAmount", SubPath = "EnumGroup")]
    public sealed class ImageFillEnumGroupMonoBinder : EnumGroupMonoBinder<Image, float, Converter>
    {
        protected override void SetValue(Image element, float value) =>
            element.fillAmount = Mathf.Clamp01(value);
    }
}