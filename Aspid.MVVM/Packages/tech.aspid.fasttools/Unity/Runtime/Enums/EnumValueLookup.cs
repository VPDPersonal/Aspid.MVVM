#nullable enable
// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Enums
{
    /// <summary>
    /// The lookup and <c>[Flags]</c>-equality core shared by <see cref="EnumValues{TValue}"/>
    /// and <see cref="EnumValues{TEnum,TValue}"/> — the single place the matching semantics
    /// live, so the two variants can never diverge.
    /// </summary>
    internal static class EnumValueLookup
    {
        /// <summary>
        /// Flag-containment equality: <paramref name="value1"/> has all bits of
        /// <paramref name="value2"/> set, with the additional rule that the zero
        /// (<c>None</c>) value is only equal to another zero value.
        /// </summary>
        public static bool FlagsEquals(long value1, long value2) =>
            (value1 & value2) == value2 && (value1 == 0L) == (value2 == 0L);

        /// <summary>
        /// Finds the value mapped to <paramref name="lookup"/>: an exact numeric match always
        /// wins; for <c>[Flags]</c> enums, otherwise the first entry (in serialized order)
        /// contained in the lookup value (see <see cref="FlagsEquals"/>) wins.
        /// Unresolved entries never match.
        /// </summary>
        public static TValue Find<TValue>(EnumValue<TValue>[] values, long lookup, bool isFlags, TValue defaultValue)
        {
            foreach (var value in values)
            {
                if (value.IsResolved && value.NumericKey == lookup)
                    return value.Value;
            }

            if (isFlags)
            {
                foreach (var value in values)
                {
                    if (value.IsResolved && FlagsEquals(lookup, value.NumericKey))
                        return value.Value;
                }
            }

            return defaultValue;
        }
    }
}
