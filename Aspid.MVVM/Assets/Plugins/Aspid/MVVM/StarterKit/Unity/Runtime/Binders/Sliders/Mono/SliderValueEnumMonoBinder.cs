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
    /// MonoBehaviour binder that sets the <see cref="Slider.value"/> property on a <see cref="Slider"/>
    /// to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value Enum")]
    [AddBinderContextMenu(typeof(Slider), serializePropertyNames: "m_Value", SubPath = "Enum")]
    public sealed class SliderValueEnumMonoBinder : EnumMonoBinder<Slider, float, Converter>
    {
        protected override void SetValue(float value) =>
            CachedComponent.value = value;
    }
}