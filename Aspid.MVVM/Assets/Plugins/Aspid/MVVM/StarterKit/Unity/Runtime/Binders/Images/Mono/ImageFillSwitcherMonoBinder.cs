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
    /// MonoBehaviour binder that switches the <see cref="Image.fillAmount"/> property on an <see cref="Image"/> component
    /// between two values based on a bound boolean ViewModel property.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill Switcher")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillAmount", SubPath = "Switcher")]
    public sealed class ImageFillSwitcherMonoBinder : SwitcherMonoBinder<Image, float, Converter>
    {
        protected override void SetValue(float value) =>
            CachedComponent.fillAmount = Mathf.Clamp01(value);
    }
}