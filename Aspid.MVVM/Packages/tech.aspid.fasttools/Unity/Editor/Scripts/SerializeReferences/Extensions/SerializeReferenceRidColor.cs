using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Deterministic colour helper for shared-reference visuals. Two entry points, both mapping into the same
    /// green→magenta palette: <see cref="ForRid"/> keys the colour to the managed-reference id (a hash of the rid) —
    /// used by the Managed References window's SHARED chip, where identity is global and there is no per-object badge.
    /// <see cref="ForIndex"/> keys it to the field's 1-based shared-reference badge number instead — used by the
    /// inspector field stripe/notice, so consecutive badges ("(1)", "(2)", "(3)") always land on maximally separated
    /// colours rather than risking two unrelated rids hashing to a similar hue.
    /// </summary>
    internal static class SerializeReferenceRidColor
    {
        // Golden-ratio hue rotation: a Knuth multiplicative hash spreads the integer rid across the full
        // hue circle, and adding the golden-ratio conjugate fraction of its low byte pushes consecutive
        // ids further apart.
        private const float GoldenRatioConjugate = 0.618033988749895f;

        // Rid colours must never look like the inspector's error (red) / warning (yellow) semantics, so the uniform
        // hue is remapped into a green→magenta band; the linear remap keeps the golden-ratio spread.
        private const float SafeHueMin = 0.32f; // green — past yellow-green/lime
        private const float SafeHueMax = 0.90f; // magenta/pink — before it reddens

        private const float Saturation = 0.55f;

        // HSV is not perceptually uniform — the eye weights green ~10× more than blue (Rec. 709), so a fixed value
        // makes greens glow "acid". Each hue's value is instead scaled to hit a common perceived luminance.
        private const float TargetLuminance = 0.6f;

        // The value ceiling: a hue the eye sees as dark (deep blue) is lifted toward — but never past — a clean
        // mid-brightness rather than blown out chasing the target.
        private const float MaxValue = 0.92f;

        /// <summary>
        /// Returns a deterministic, visually distinct colour for <paramref name="rid"/>. The same rid
        /// always maps to the same colour; nearby rids are separated by the golden-ratio hue rotation.
        /// The hue is confined to a green→magenta band (never the inspector's red error or yellow warning),
        /// and every hue is normalised to a common perceived luminance so no colour reads brighter or more
        /// "acid" than the rest.
        /// </summary>
        public static Color ForRid(long rid)
        {
            var hash = unchecked((uint)(rid * 2654435761));
            var fraction = (hash / (float)uint.MaxValue + GoldenRatioConjugate * (hash & 0xFF)) % 1f;
            return FromFraction(fraction);
        }

        /// <summary>
        /// Returns the palette colour for the 1-based shared-reference badge <paramref name="index"/> within an object
        /// ("(1)", "(2)", "(3)", …). Unlike <see cref="ForRid"/> — a hash, where two unrelated groups can collide on a
        /// similar hue — consecutive indices are walked around the hue band by the golden-ratio sequence, which drops
        /// each next value into the largest remaining gap, so small consecutive badge numbers are maximally separated.
        /// The same index always maps to the same colour, so the badge number and its colour stay in lock-step.
        /// </summary>
        public static Color ForIndex(int index)
        {
            var fraction = (index * GoldenRatioConjugate) % 1f;
            return FromFraction(fraction);
        }

        // Hue into the safe green→magenta band, value normalised to the common perceived luminance.
        private static Color FromFraction(float fraction)
        {
            var hue = SafeHueMin + fraction * (SafeHueMax - SafeHueMin);

            // Scale value to a common perceived luminance: luminance is linear in HSV's value for a fixed hue/saturation,
            // so measure this hue's Rec. 709 luminance at full value and take the value that lands it on TargetLuminance.
            var full = Color.HSVToRGB(hue, Saturation, 1f);
            var luminance = 0.2126f * full.r + 0.7152f * full.g + 0.0722f * full.b;
            var value = Mathf.Min(TargetLuminance / luminance, MaxValue);

            return Color.HSVToRGB(hue, Saturation, value);
        }
    }
}
