using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Compares a <see cref="DateTime"/> with a reference moment.
    /// </summary>
    /// <remarks>
    /// The comparison is made in UTC only when both kinds are known; otherwise raw ticks are compared.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Time/To Bool",
        Name = "Compare",
        Tooltip = "Compares a DateTime with a reference moment")]
    public sealed class DateTimeCompareConverter : IConverter<DateTime, bool>
    {
        [Tooltip("How the bound moment is compared with the reference.")]
        [SerializeField] private ComparisonMode _comparison = ComparisonMode.GreaterThan;

        [Tooltip("What the bound moment is compared against: " +
            "the fixed moment below, the current local time, or the current UTC time. " +
            "Match this to the bound moment's kind, or the comparison is out by the time zone.")]
        [SerializeField] private ReferenceSource _referenceSource = ReferenceSource.Now;

        [Tooltip("Ticks of the fixed moment compared against. " +
            "A value outside the representable range is reported and the comparison answers false.")]
        [SerializeField] private long _referenceTicks;

        [Tooltip("The kind of the fixed moment. The comparison is made in UTC only when this and" +
            " the bound moment's kind are both known; otherwise raw ticks are compared.")]
        [SerializeField] private DateTimeKind _referenceKind = DateTimeKind.Unspecified;

        /// <remarks>Default: comparing whether the bound moment is later than now.</remarks>
        public DateTimeCompareConverter() { }

        /// <param name="comparison">How the bound moment is compared with the reference.</param>
        /// <param name="referenceSource">
        /// What the bound moment is compared against; match it to the bound moment's kind, or the
        /// comparison is out by the time zone. With <see cref="ReferenceSource.FixedMoment"/> the
        /// reference is <see cref="DateTime.MinValue"/>; use the
        /// <see cref="DateTimeCompareConverter(ComparisonMode, DateTime)"/> overload to set one.
        /// </param>
        public DateTimeCompareConverter(ComparisonMode comparison, ReferenceSource referenceSource = ReferenceSource.Now)
        {
            _comparison = comparison;
            _referenceSource = referenceSource;
        }

        /// <param name="comparison">How the bound moment is compared with the reference.</param>
        /// <param name="reference">
        /// The fixed moment compared against. The comparison is made in UTC only when its
        /// <see cref="DateTime.Kind"/> and the bound moment's kind are both known; otherwise raw
        /// ticks are compared.
        /// </param>
        public DateTimeCompareConverter(ComparisonMode comparison, DateTime reference)
        {
            _comparison = comparison;
            _referenceKind = reference.Kind;
            _referenceTicks = reference.Ticks;
            _referenceSource = ReferenceSource.FixedMoment;
        }

        /// <summary>
        /// Compares the specified moment with the reference.
        /// </summary>
        /// <param name="value">The moment to compare.</param>
        /// <returns>
        /// The result of the comparison. Reports an error and answers <see langword="false"/> when
        /// the fixed moment's ticks are outside the representable range, or the reference source or
        /// comparison is not a declared value.
        /// </returns>
        public bool Convert(DateTime value)
        {
            DateTime reference;
            
            switch (_referenceSource)
            {
                case ReferenceSource.FixedMoment:
                    if (!TryFixedMoment(out reference))
                        return InvalidFixedMoment();
                    break;

                case ReferenceSource.Now: reference = DateTime.Now; break;
                case ReferenceSource.UtcNow: reference = DateTime.UtcNow; break;
                default: return UndeclaredReferenceSource();
            }

            var order = Compare(value, reference);

            return _comparison switch
            {
                ComparisonMode.Equal => order == 0,
                ComparisonMode.NotEqual => order != 0,
                ComparisonMode.LessThan => order < 0,
                ComparisonMode.GreaterThan => order > 0,
                ComparisonMode.LessThanOrEqual => order <= 0,
                ComparisonMode.GreaterThanOrEqual => order >= 0,
                _ => UndeclaredComparison()
            };
        }

        // _referenceTicks is inspector-editable as a raw long, so a value outside DateTime's range
        // must be caught rather than throwing out of the binder.
        private bool TryFixedMoment(out DateTime moment)
        {
            try
            {
                moment = new DateTime(_referenceTicks, _referenceKind);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                moment = default;
                return false;
            }
        }

        private bool InvalidFixedMoment()
        {
            this.LogError(
                problem: $"the fixed moment's ticks ({_referenceTicks}) are outside the representable range",
                consequence: "Reporting false.");

            return false;
        }

        private bool UndeclaredReferenceSource()
        {
            this.LogError(
                problem: $"the reference source {_referenceSource.Describe()} is not a declared {nameof(ReferenceSource)}",
                consequence: "Reporting false.");

            return false;
        }

        private bool UndeclaredComparison()
        {
            this.LogError(
                problem: $"the comparison {_comparison.Describe()} is not a declared {nameof(ComparisonMode)}",
                consequence: "Reporting false.");

            return false;
        }

        // Two moments with known kinds name absolute instants and can be compared in UTC; as soon
        // as either kind is Unspecified the instant is unknowable, so the ticks are compared as-is.
        private static int Compare(DateTime value, DateTime reference) =>
            value.Kind != DateTimeKind.Unspecified && reference.Kind != DateTimeKind.Unspecified
                ? value.ToUniversalTime().CompareTo(reference.ToUniversalTime())
                : value.CompareTo(reference);
    }
}
