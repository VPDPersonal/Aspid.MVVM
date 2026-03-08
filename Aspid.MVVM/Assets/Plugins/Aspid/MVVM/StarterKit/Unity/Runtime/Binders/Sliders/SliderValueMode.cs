// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Specifies which endpoint of a <see cref="UnityEngine.UI.Slider"/> range is updated when setting a min/max value.
    /// </summary>
    public enum SliderValueMode
    {
        /// <summary>
        /// Only the minimum value of the slider range is set.
        /// </summary>
        Min,
        
        /// <summary>
        /// Only the maximum value of the slider range is set.
        /// </summary>
        Max,
        
        /// <summary>
        /// Both the minimum and maximum values of the slider range are set simultaneously.
        /// </summary>
        Range
    }
}