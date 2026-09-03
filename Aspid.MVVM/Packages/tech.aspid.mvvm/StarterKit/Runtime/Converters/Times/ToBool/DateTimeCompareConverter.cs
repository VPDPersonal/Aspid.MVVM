#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Compares a <see cref="DateTime"/> with a reference moment.
    /// </summary>
    /// <remarks>Compared in UTC when both kinds are known; otherwise by raw ticks.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Time/To Bool",
        Name = "Compare",
        Tooltip = "Compares a DateTime with a reference moment")]
    public sealed class DateTimeCompareConverter : IConverter<DateTime, bool>
    {
        [Tooltip("How the bound moment is compared with the reference.")]
        [SerializeField] private ComparisonMode _comparison = ComparisonMode.GreaterThan;

        [Tooltip("What the bound moment is compared against. Match it to the bound moment's kind.")]
        [SerializeField] private ReferenceSource _referenceSource = ReferenceSource.Now;

        [Tooltip("Ticks of the fixed moment compared against.")]
        [SerializeField] private long _referenceTicks;

        [Tooltip("The kind of the fixed moment.")]
        [SerializeField] private DateTimeKind _referenceKind = DateTimeKind.Unspecified;

        /// <remarks>Default: comparing whether the bound moment is later than now.</remarks>
        public DateTimeCompareConverter() { }

        /// <param name="comparison">How the bound moment is compared with the reference.</param>
        /// <param name="referenceSource">What the bound moment is compared against. Match it to the bound moment's kind.</param>
        public DateTimeCompareConverter(
            ComparisonMode comparison,
            ReferenceSource referenceSource = ReferenceSource.Now)
        {
            _comparison = comparison;
            _referenceSource = referenceSource;
        }

        /// <param name="comparison">How the bound moment is compared with the reference.</param>
        /// <param name="reference">The fixed moment compared against.</param>
        public DateTimeCompareConverter(
            ComparisonMode comparison,
            DateTime reference)
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
        /// <returns>The result. Out-of-range ticks, an undeclared source or comparison report an error and return <see langword="false"/>.</returns>
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

        private bool TryFixedMoment(out DateTime moment)
        {
            if (_referenceTicks >= DateTime.MinValue.Ticks && _referenceTicks <= DateTime.MaxValue.Ticks)
            {
                moment = new DateTime(_referenceTicks, _referenceKind);
                return true;
            }

            moment = default;
            return false;
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

        private static int Compare(DateTime value, DateTime reference) =>
            value.Kind != DateTimeKind.Unspecified && reference.Kind != DateTimeKind.Unspecified
                ? value.ToUniversalTime().CompareTo(reference.ToUniversalTime())
                : value.CompareTo(reference);
    }
}
