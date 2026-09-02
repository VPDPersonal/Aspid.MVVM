#nullable enable
using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using static Aspid.MVVM.Tests.ConverterReflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ConditionalConverter{T}"/> — the predicate-selected branch and the
    /// half-configured Inspector shapes.
    /// </summary>
    [TestFixture]
    public sealed class ConditionalConverterTests
    {
        [Test]
        public void Conditional_TrueBranch_IsApplied() =>
            Assert.AreEqual(11, Conditional(new IsPositive(), new AddConverter(1), new AddConverter(100)).Convert(10));

        [Test]
        public void Conditional_FalseBranch_IsApplied() =>
            Assert.AreEqual(90, Conditional(new IsPositive(), new AddConverter(1), new AddConverter(100)).Convert(-10));

        [Test]
        public void Conditional_NullPredicateInTheConstructor_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _ = new ConditionalConverter<int>(null!, new AddConverter(1), null));

        // A configured branch with no predicate to select it is a half-finished setup, not a design.
        [Test]
        public void Conditional_NoPredicateButConfiguredBranch_PassesThroughAndReportsEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("predicate that selects it is missing"));

            var converter = Conditional(null, new AddConverter(1), new AddConverter(100));
            Assert.AreEqual(10, converter.Convert(10));
            converter.Convert(11);
        }

        // A fully empty instance is the identity by design, so it stays quiet.
        [Test]
        public void Conditional_FullyEmpty_PassesTheValueThroughSilently() =>
            Assert.AreEqual(10, Conditional(null, null, null).Convert(10));

        [Test]
        public void Conditional_EmptyBranch_PassesTheValueThrough() =>
            Assert.AreEqual(10, Conditional(new IsPositive(), null, new AddConverter(100)).Convert(10));

        // The Inspector shape: a wrapper deserialized with any subset of its slots filled in. The
        // constructor refuses a null predicate, so the half-configured states are built field by field.
        private static ConditionalConverter<int> Conditional(
            IConverter<int, bool>? predicate,
            IConverter<int, int>? then,
            IConverter<int, int>? @else)
        {
            var converter = Empty<ConditionalConverter<int>>();

            SetField(converter, "_predicate", predicate);
            SetField(converter, "_then", then);
            SetField(converter, "_else", @else);

            return converter;
        }

        private sealed class IsPositive : IConverter<int, bool>
        {
            public bool Convert(int value) => value > 0;
        }
    }
}
