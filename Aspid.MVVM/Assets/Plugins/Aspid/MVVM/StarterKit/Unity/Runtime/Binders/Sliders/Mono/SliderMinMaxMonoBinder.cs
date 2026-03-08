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
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – MinMax")]
    [AddBinderContextMenu(typeof(Slider), "m_MinValue", "m_MaxValue")]
    public partial class SliderMinMaxMonoBinder : ComponentMonoBinder<Slider, Vector2, Converter>, INumberBinder
    {
        [SerializeField] private SliderValueMode _valueMode = SliderValueMode.Range;

        protected sealed override Vector2 Property
        {
            get => new(CachedComponent.minValue, CachedComponent.maxValue);
            set => CachedComponent.SetMinMax(value, _valueMode);
        }

        [BinderLog]
        public void SetValue(float value) =>
            SetValue(new Vector2(value, value));

        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}