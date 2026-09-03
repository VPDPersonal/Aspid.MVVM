using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="DebugLogBinder"/>'s message building — the one place where a
    /// converter returning <see langword="null"/> and having no converter at all had to stop being
    /// the same thing.
    /// </summary>
    [TestFixture]
    public sealed class DebugLogBinderTests
    {
        [Test]
        public void SetValue_NullValue_LogsInsteadOfThrowing()
        {
            LogAssert.Expect(LogType.Log, "SetValue: null");

            new DebugLogBinder().SetValue<string>(null);
        }

        [Test]
        public void SetValue_NullValue_WithoutConverter_LogsInsteadOfThrowing()
        {
            LogAssert.Expect(LogType.Log, "SetValue: null");

            new DebugLogBinder(new NullConverter()).SetValue("ignored");
        }

        [Test]
        public void SetValue_UsesTheConfiguredConverter()
        {
            LogAssert.Expect(LogType.Log, "SetValue: HP: 42");

            new DebugLogBinder(new ValueToStringConverter<object>("HP: {0}")).SetValue(42);
        }

        // The parameter is documented as "pass null to use ValueToStringConverter", which the
        // constructor used to contradict by overwriting the field initializer with null.
        [Test]
        public void DefaultConstructed_FallsBackToValueToStringConverter()
        {
            LogAssert.Expect(LogType.Log, "SetValue: 42");

            new DebugLogBinder().SetValue(42);
        }

        private sealed class NullConverter : IConverter<object, string>
        {
            public string Convert(object value) => null;
        }
    }
}
