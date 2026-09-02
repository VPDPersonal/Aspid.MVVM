using System;
using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.StarterKit;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="BinderLogger"/>: every overload writes the same
    /// <c>"[Aspid.MVVM] TypeName: problem. consequence"</c> shape.
    /// </summary>
    [TestFixture]
    public sealed class BinderLoggerTests
    {
        [Test]
        public void Log_WritesThePrefixAndTheBinderName()
        {
            var binder = new RecordingBinder();
            LogAssert.Expect(LogType.Log, new Regex(@"^\[Aspid\.MVVM\] RecordingBinder: hello$"));

            binder.Log("hello");
        }

        [Test]
        public void LogWarning_WritesTheProblemAndTheConsequence()
        {
            var binder = new RecordingBinder();
            LogAssert.Expect(LogType.Warning, new Regex(@"^\[Aspid\.MVVM\] RecordingBinder: odd setup\. Using a default\.$"));

            binder.LogWarning("odd setup", "Using a default.");
        }

        [Test]
        public void LogError_WritesTheProblemAndTheConsequence()
        {
            var binder = new RecordingBinder();
            LogAssert.Expect(LogType.Error, new Regex(@"^\[Aspid\.MVVM\] RecordingBinder: bad value\. Ignoring it\.$"));

            binder.LogError("bad value", "Ignoring it.");
        }

        [Test]
        public void LogError_AnException_NamesItAndItsMessage()
        {
            var binder = new RecordingBinder();
            LogAssert.Expect(LogType.Error,
                new Regex(@"^\[Aspid\.MVVM\] RecordingBinder: threw InvalidOperationException \(broken\)\. Skipping the push\."));

            binder.LogError(new InvalidOperationException("broken"), "Skipping the push.");
        }

        [Test]
        public void TypeOverloads_ReportOnBehalfOfAnotherBinder()
        {
            LogAssert.Expect(LogType.Error, new Regex(@"^\[Aspid\.MVVM\] RecordingBinder: on behalf\. Reported anyway\.$"));

            BinderLogger.LogError(typeof(RecordingBinder), "on behalf", "Reported anyway.");
        }

        private sealed class RecordingBinder : Binder, IBinder<int>
        {
            public void SetValue(int value) { }
        }
    }
}
