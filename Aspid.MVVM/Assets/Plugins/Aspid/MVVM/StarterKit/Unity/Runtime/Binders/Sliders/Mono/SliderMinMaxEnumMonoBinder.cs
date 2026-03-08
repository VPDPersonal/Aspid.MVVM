using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector2;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the minimum and maximum values of a <see cref="UnityEngine.UI.Slider"/>
    /// to a range resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – MinMax Enum")]
    [AddBinderContextMenu(typeof(Slider), "m_MinValue", "m_MaxValue", SubPath = "Enum")]
    public sealed class SliderMinMaxEnumMonoBinder : EnumMonoBinder<Slider, Vector2, Converter>
    {
        [SerializeField] private SliderValueMode _valueMode = SliderValueMode.Range;

        protected override void SetValue(Vector2 value) =>
            CachedComponent.SetMinMax(value, _valueMode);
    }
}