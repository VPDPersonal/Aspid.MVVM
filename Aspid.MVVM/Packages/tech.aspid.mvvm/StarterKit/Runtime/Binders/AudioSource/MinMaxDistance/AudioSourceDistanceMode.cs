// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Specifies which <see cref="UnityEngine.AudioSource"/> distances a bound <see cref="UnityEngine.Vector2"/> writes.
    /// </summary>
    public enum AudioSourceDistanceMode
    {
        /// <summary>
        /// Only <see cref="UnityEngine.AudioSource.minDistance"/>, from <c>x</c>.
        /// </summary>
        Min,

        /// <summary>
        /// Only <see cref="UnityEngine.AudioSource.maxDistance"/>, from <c>y</c>.
        /// </summary>
        Max,

        /// <summary>
        /// Both distances.
        /// </summary>
        Range
    }
}
