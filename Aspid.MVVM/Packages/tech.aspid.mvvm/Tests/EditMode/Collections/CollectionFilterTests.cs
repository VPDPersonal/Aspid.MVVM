#nullable enable
using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for the composite and leaf <see cref="ICollectionFilter{T}"/> implementations.
    /// </summary>
    [TestFixture]
    public sealed class CollectionFilterTests
    {
        private static readonly ICollectionFilter<int> Positive = new PredicateCollectionFilter<int>(value => value > 0);
        private static readonly ICollectionFilter<int> Even = new PredicateCollectionFilter<int>(value => value % 2 == 0);

        [Test]
        public void Predicate_DelegatesToTheWrappedPredicate()
        {
            Assert.IsTrue(Positive.Matches(1));
            Assert.IsFalse(Positive.Matches(-1));
        }

        [Test]
        public void Predicate_NullInTheConstructor_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _ = new PredicateCollectionFilter<int>(null!));

        [Test]
        public void And_PassesOnlyWhenEveryFilterPasses()
        {
            var filter = new AndCollectionFilter<int>(Positive, Even);

            Assert.IsTrue(filter.Matches(2));
            Assert.IsFalse(filter.Matches(1));
            Assert.IsFalse(filter.Matches(-2));
        }

        [Test]
        public void And_SkipsEmptySlots() =>
            Assert.IsTrue(new AndCollectionFilter<int>(null, Positive).Matches(1));

        [Test]
        public void And_NoFilters_PassesEverything()
        {
            Assert.IsTrue(new AndCollectionFilter<int>().Matches(-1));
            Assert.IsTrue(new AndCollectionFilter<int>(null).Matches(-1));
        }

        [Test]
        public void Or_PassesWhenAnyFilterPasses()
        {
            var filter = new OrCollectionFilter<int>(Positive, Even);

            Assert.IsTrue(filter.Matches(1));
            Assert.IsTrue(filter.Matches(-2));
            Assert.IsFalse(filter.Matches(-1));
        }

        [Test]
        public void Or_SkipsEmptySlots() =>
            Assert.IsFalse(new OrCollectionFilter<int>(null, Positive).Matches(-1));

        [Test]
        public void Or_NoFilters_PassesEverything()
        {
            Assert.IsTrue(new OrCollectionFilter<int>().Matches(-1));
            Assert.IsTrue(new OrCollectionFilter<int>(null).Matches(-1));
        }

        [Test]
        public void Not_InvertsTheNestedFilter()
        {
            var filter = new NotCollectionFilter<int>(Positive);

            Assert.IsFalse(filter.Matches(1));
            Assert.IsTrue(filter.Matches(-1));
        }

        [Test]
        public void Not_NullInTheConstructor_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _ = new NotCollectionFilter<int>(null!));

        [Test]
        public void Conditional_Enabled_AppliesTheNestedFilter()
        {
            var filter = new ConditionalCollectionFilter<int>(Positive);

            Assert.IsTrue(filter.Matches(1));
            Assert.IsFalse(filter.Matches(-1));
        }

        [Test]
        public void Conditional_Disabled_PassesEverything() =>
            Assert.IsTrue(new ConditionalCollectionFilter<int>(Positive, isEnabled: false).Matches(-1));

        [Test]
        public void Conditional_ToggleIsReadOnEveryMatch()
        {
            var filter = new ConditionalCollectionFilter<int>(Positive);

            filter.IsEnabled = false;
            Assert.IsTrue(filter.Matches(-1));

            filter.IsEnabled = true;
            Assert.IsFalse(filter.Matches(-1));
        }

        [Test]
        public void Conditional_NullInTheConstructor_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _ = new ConditionalCollectionFilter<int>(null!));

        [Test]
        public void Converter_UsesTheConverterAsThePredicate()
        {
            var filter = new ConverterCollectionFilter<int>(new IsPositive());

            Assert.IsTrue(filter.Matches(1));
            Assert.IsFalse(filter.Matches(0));
        }

        [Test]
        public void Converter_NullInTheConstructor_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _ = new ConverterCollectionFilter<int>(null!));

        private sealed class IsPositive : IConverter<int, bool>
        {
            public bool Convert(int value) => value > 0;
        }
    }
}
