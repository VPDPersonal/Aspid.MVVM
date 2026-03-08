// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Specifies which distance component(s) of an <see cref="UnityEngine.AudioSource"/> are
    /// controlled by a distance binder.
    /// </summary>
    public enum AudioSourceDistanceMode
    {
        /// <summary>
        /// Sets only the minimum distance of the <see cref="UnityEngine.AudioSource"/>.
        /// </summary>
        Min,

        /// <summary>
        /// Sets only the maximum distance of the <see cref="UnityEngine.AudioSource"/>.
        /// </summary>
        Max,

        /// <summary>
        /// Sets both the minimum and maximum distances of the <see cref="UnityEngine.AudioSource"/> from a <see cref="UnityEngine.Vector2"/> (x = min, y = max).
        /// </summary>
        Range
    }
}