// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Specifies which distance component of an <see cref="UnityEngine.AudioSource"/> is updated
    /// when applying a bound value.
    /// </summary>
    public enum AudioSourceDistanceMode
    {
        /// <summary>
        /// Sets only the minimum distance; the maximum distance is unchanged.
        /// </summary>
        Min,

        /// <summary>
        /// Sets only the maximum distance; the minimum distance is unchanged.
        /// </summary>
        Max,

        /// <summary>
        /// Sets both the minimum and maximum distance.
        /// </summary>
        Range
    }
}