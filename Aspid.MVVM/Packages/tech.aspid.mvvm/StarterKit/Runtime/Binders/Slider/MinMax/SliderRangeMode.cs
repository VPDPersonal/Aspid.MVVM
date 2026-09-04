// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Specifies which <see cref="UnityEngine.UI.Slider"/> endpoints a bound <see cref="UnityEngine.Vector2"/> writes.
    /// </summary>
    public enum SliderRangeMode
    {
        /// <summary>
        /// Only <see cref="UnityEngine.UI.Slider.minValue"/>, from <c>x</c>.
        /// </summary>
        Min,

        /// <summary>
        /// Only <see cref="UnityEngine.UI.Slider.maxValue"/>, from <c>y</c>.
        /// </summary>
        Max,

        /// <summary>
        /// Both endpoints.
        /// </summary>
        Range
    }
}
