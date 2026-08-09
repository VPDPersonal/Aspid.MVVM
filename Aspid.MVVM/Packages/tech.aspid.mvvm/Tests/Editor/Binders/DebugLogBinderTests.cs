using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="DebugLogBinder"/>'s message building — the one place where a
    /// converter returning <see langword="null"/> and having no converter at all had to stop being
    /// the same thing.
    /// </summary>
    [TestFixture]
    internal sealed class DebugLogBinderTests
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

            new DebugLogBinder(new ObjectToStringConverter("HP: {0}")).SetValue(42);
        }

        // The parameter is documented as "pass null to use ObjectToStringConverter", which the
        // constructor used to contradict by overwriting the field initialiser with null.
        [Test]
        public void DefaultConstructed_FallsBackToObjectToStringConverter()
        {
            LogAssert.Expect(LogType.Log, "SetValue: 42");

            new DebugLogBinder().SetValue(42);
        }

        private sealed class NullConverter : IConverterObjectToString
        {
            public string Convert(object value) => null;
        }
    }
}
