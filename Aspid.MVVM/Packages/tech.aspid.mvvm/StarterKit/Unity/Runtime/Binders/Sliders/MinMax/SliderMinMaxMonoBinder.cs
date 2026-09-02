using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that sets the minimum and maximum
    /// values of a <see cref="Slider"/> when the bound ViewModel value changes.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – MinMax")]
    [AddBinderContextMenu(typeof(Slider), "m_MinValue", "m_MaxValue")]
    public partial class SliderMinMaxMonoBinder : ComponentMonoBinder<Slider, Vector2>, IVector2Binder
    {
        [Tooltip("Determines which endpoint(s) of the slider range are updated.")]
        [SerializeField] private SliderValueMode _valueMode = SliderValueMode.Range;

        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => new(CachedComponent.minValue, CachedComponent.maxValue);
            set => CachedComponent.SetMinMax(value, _valueMode);
        }
    }
}
