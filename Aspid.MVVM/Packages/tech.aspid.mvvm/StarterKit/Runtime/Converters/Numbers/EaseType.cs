#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The easing curve <see cref="EasingConverter"/> applies.
    /// </summary>
    /// <remarks>
    /// The standard Penner set: <c>In</c> starts slowly, <c>Out</c> ends slowly, <c>InOut</c> does both
    /// around a midpoint. Families are ordered by how hard they pull — Sine, Quad, Cubic, Quart, Quint,
    /// Expo, Circ — then Back, Elastic and Bounce. Only Back and Elastic leave the 0..1 range.
    /// </remarks>
    public enum EaseType
    {
        /// <summary>
        /// No easing: the value passes through unchanged.
        /// </summary>
        Linear,

        /// <summary>
        /// A quarter sine wave, starting slowly. The gentlest of the set.
        /// </summary>
        SineIn,

        /// <summary>
        /// A quarter sine wave, ending slowly.
        /// </summary>
        SineOut,

        /// <summary>
        /// A half sine wave: slow at both ends, fastest in the middle.
        /// </summary>
        SineInOut,

        /// <summary>
        /// The square, starting slowly.
        /// </summary>
        QuadIn,

        /// <summary>
        /// The square, ending slowly.
        /// </summary>
        QuadOut,

        /// <summary>
        /// The square, slow at both ends.
        /// </summary>
        QuadInOut,

        /// <summary>
        /// The cube, starting slowly.
        /// </summary>
        CubicIn,

        /// <summary>
        /// The cube, ending slowly.
        /// </summary>
        CubicOut,

        /// <summary>
        /// The cube, slow at both ends.
        /// </summary>
        CubicInOut,

        /// <summary>
        /// The fourth power, starting slowly.
        /// </summary>
        QuartIn,

        /// <summary>
        /// The fourth power, ending slowly.
        /// </summary>
        QuartOut,

        /// <summary>
        /// The fourth power, slow at both ends.
        /// </summary>
        QuartInOut,

        /// <summary>
        /// The fifth power, starting slowly. The hardest of the polynomials.
        /// </summary>
        QuintIn,

        /// <summary>
        /// The fifth power, ending slowly.
        /// </summary>
        QuintOut,

        /// <summary>
        /// The fifth power, slow at both ends.
        /// </summary>
        QuintInOut,

        /// <summary>
        /// A doubling curve, starting almost flat.
        /// </summary>
        ExpoIn,

        /// <summary>
        /// A doubling curve, ending almost flat.
        /// </summary>
        ExpoOut,

        /// <summary>
        /// A doubling curve, almost flat at both ends.
        /// </summary>
        ExpoInOut,

        /// <summary>
        /// A quarter circle, starting slowly and ending vertically.
        /// </summary>
        CircIn,

        /// <summary>
        /// A quarter circle, starting vertically and ending slowly.
        /// </summary>
        CircOut,

        /// <summary>
        /// A half circle, vertical through the middle.
        /// </summary>
        CircInOut,

        /// <summary>
        /// Pulls back below 0 before moving forward.
        /// </summary>
        BackIn,

        /// <summary>
        /// Overshoots past 1 before settling.
        /// </summary>
        BackOut,

        /// <summary>
        /// Pulls back at the start and overshoots at the end.
        /// </summary>
        BackInOut,

        /// <summary>
        /// Oscillates with a growing amplitude, then snaps to 1.
        /// </summary>
        ElasticIn,

        /// <summary>
        /// Snaps past 1 and oscillates to a stop.
        /// </summary>
        ElasticOut,

        /// <summary>
        /// Oscillates at both ends.
        /// </summary>
        ElasticInOut,

        /// <summary>
        /// Bounces towards the start.
        /// </summary>
        BounceIn,

        /// <summary>
        /// Lands on 1 and bounces to a stop. The one most animations want.
        /// </summary>
        BounceOut,

        /// <summary>
        /// Bounces at both ends.
        /// </summary>
        BounceInOut,
    }
}
