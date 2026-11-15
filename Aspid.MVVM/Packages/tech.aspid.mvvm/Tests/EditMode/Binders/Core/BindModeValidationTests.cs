using System;
using NUnit.Framework;
using Unity.Profiling;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    [TestFixture]
    internal sealed class BindModeValidationTests
    {
        [TestCase(BindMode.None, false, false, false, false)]
        [TestCase(BindMode.OneWay, true, true, true, false)]
        [TestCase(BindMode.TwoWay, true, false, true, true)]
        [TestCase(BindMode.OneTime, true, true, true, false)]
        [TestCase(BindMode.OneWayToSource, true, false, false, true)]
        [TestCase((BindMode)(-1), false, false, false, false)]
        [TestCase((BindMode)5, false, false, false, false)]
        [TestCase((BindMode)99, false, false, false, false)]
        public void PredicatesDescribeTheRequestedDirections(
            BindMode mode, bool valid, bool forwardOnly, bool forward, bool reverse)
        {
            Assert.That(mode.IsValid(), Is.EqualTo(valid));
            Assert.That(mode.IsForwardOnly(), Is.EqualTo(forwardOnly));
            Assert.That(mode.IncludesForward(), Is.EqualTo(forward));
            Assert.That(mode.IncludesReverse(), Is.EqualTo(reverse));
        }

        [TestCase(BindMode.None)]
        [TestCase((BindMode)(-1))]
        [TestCase((BindMode)99)]
        [TestCase((BindMode)int.MinValue)]
        [TestCase((BindMode)int.MaxValue)]
        public void EveryGuardRejectsInvalidModes(BindMode mode)
        {
            Assert.Throws<InvalidOperationException>(() => mode.EnsureForwardOnly());
            Assert.Throws<InvalidOperationException>(() => mode.EnsureReverse());
            Assert.Throws<InvalidOperationException>(() => mode.EnsureForward());
            Assert.Throws<ArgumentOutOfRangeException>(() => mode.EnsureValid());
            Assert.Throws<ArgumentOutOfRangeException>(() => mode.EnsureNot(BindMode.TwoWay));
        }

        [TestCase(BindMode.OneWay)]
        [TestCase(BindMode.TwoWay)]
        [TestCase(BindMode.OneTime)]
        [TestCase(BindMode.OneWayToSource)]
        public void GuardsAllowExactlyTheirDocumentedModes(BindMode mode)
        {
            Assert.DoesNotThrow(() => mode.EnsureValid());
            Check(() => mode.EnsureForwardOnly(), mode is BindMode.OneWay or BindMode.OneTime);
            Check(() => mode.EnsureReverse(), mode is BindMode.TwoWay or BindMode.OneWayToSource);
            Check(() => mode.EnsureForward(), mode is not BindMode.OneWayToSource);

            foreach (var forbidden in new[] { BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime, BindMode.OneWayToSource })
            {
                if (mode == forbidden)
                    Assert.Throws<ArgumentException>(() => mode.EnsureNot(forbidden));
                else
                    Assert.DoesNotThrow(() => mode.EnsureNot(forbidden));
            }
        }

        [Test]
        public void AnInvalidExcludedModeNamesItsArgument()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                () => BindMode.OneWay.EnsureNot((BindMode)99));
            Assert.That(exception.ParamName, Is.EqualTo("excludedMode"));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void AttributesRejectNullModeArrays(int attribute)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => CreateAttribute(attribute, null));
            Assert.That(exception.ParamName, Is.EqualTo("modes"));
        }

        [TestCase(BindMode.None)]
        [TestCase((BindMode)(-1))]
        [TestCase((BindMode)99)]
        public void AttributesRejectInvalidEntries(BindMode mode)
        {
            for (var attribute = 0; attribute < 3; attribute++)
            {
                var exception = Assert.Throws<ArgumentOutOfRangeException>(
                    () => CreateAttribute(attribute, BindMode.OneWay, mode));
                Assert.That(exception.ParamName, Is.EqualTo("modes"));
                Assert.That(exception.ActualValue, Is.EqualTo(mode));
            }
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void AttributesKeepEmptyAndExplicitModeLists(int attribute)
        {
            Assert.That(CreateAttribute(attribute), Is.Empty);
            Assert.That(CreateAttribute(attribute, BindMode.OneWay, BindMode.OneTime),
                Is.EqualTo(new[] { BindMode.OneWay, BindMode.OneTime }));
        }

        [TestCase(BindMode.None)]
        [TestCase((BindMode)99)]
        public void ForwardOnlyConstructorsRejectInvalidModes(BindMode mode)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DelegateOneWayBinder<int>(_ => { }, mode));
        }

        [Test]
        public void ErrorMessagesShowTheActualModeAndAllowedModes()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => BindMode.TwoWay.EnsureForwardOnly());
            var message = Regex.Replace(exception.Message, @"</?color(?:=#[0-9a-fA-F]+)?>", string.Empty);
            Assert.That(message, Is.EqualTo("Unsupported binding mode: TwoWay.\nAllowed: OneWay, OneTime."));
        }

        [Test]
        public void SuccessfulGuardsDoNotAllocate()
        {
            for (var i = 0; i < 100; i++)
                RunSuccessfulGuards();

            const ProfilerRecorderOptions options = ProfilerRecorderOptions.StartImmediately
                | ProfilerRecorderOptions.CollectOnlyOnCurrentThread;
            using var control = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC.Alloc", 16, options);
            GC.KeepAlive(new byte[16]);
            control.Stop();
            Assert.That(control.Valid, Is.True);
            Assert.That(control.Count, Is.GreaterThan(0), "The profiler must detect the control allocation.");

            using var recorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC.Alloc", 16, options);
            for (var i = 0; i < 1000; i++)
                RunSuccessfulGuards();

            recorder.Stop();
            Assert.That(recorder.Valid, Is.True);
            Assert.That(recorder.Count, Is.Zero);
        }

        private static void RunSuccessfulGuards()
        {
            BindMode.OneWay.EnsureForwardOnly();
            BindMode.TwoWay.EnsureReverse();
            BindMode.OneWay.EnsureForward();
            BindMode.OneWay.EnsureValid();
            BindMode.OneWay.EnsureNot(BindMode.TwoWay);
        }

        private static void Check(TestDelegate operation, bool allowed)
        {
            if (allowed)
                Assert.DoesNotThrow(operation);
            else
                Assert.Throws<InvalidOperationException>(operation);
        }

        private static IReadOnlyList<BindMode> CreateAttribute(int attribute, params BindMode[] modes) => attribute switch
        {
            0 => new BindModeAttribute(modes).Modes,
            1 => new BindModeOverrideAttribute(modes).Modes,
            2 => new UsedInModesAttribute(modes).Modes,
            _ => throw new ArgumentOutOfRangeException(nameof(attribute))
        };
    }
}
