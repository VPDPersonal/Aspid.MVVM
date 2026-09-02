using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="AnyToStringCasterBinder"/>, the <see cref="IAnyBinder"/> that stringifies whatever value it receives.
    /// </summary>
    [TestFixture]
    public sealed class AnyToStringCasterBinderTests
    {
        [Test]
        public void SetValue_UsesTheDefaultConverter()
        {
            var received = new List<string?>();
            var binder = new AnyToStringCasterBinder(received.Add);

            ((IAnyBinder)binder).SetValue(42);

            Assert.AreEqual(new List<string?> { "42" }, received);
        }

        [Test]
        public void SetValue_ANullValue_ForwardsNull()
        {
            var received = new List<string?>();
            var binder = new AnyToStringCasterBinder(received.Add);

            ((IAnyBinder)binder).SetValue<object?>(null);

            Assert.AreEqual(new List<string?> { null }, received);
        }

        [Test]
        public void SetValue_ACustomConverter_IsUsedInstead()
        {
            var received = new List<string?>();
            var binder = new AnyToStringCasterBinder(received.Add, new UpperCaseConverter());

            ((IAnyBinder)binder).SetValue("abc");

            Assert.AreEqual(new List<string?> { "ABC" }, received, "The custom converter was not used");
        }

        [Test]
        public void Constructor_ANullSetValue_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _ = new AnyToStringCasterBinder(setValue: null!));

        [Test]
        public void Constructor_ANullConverter_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => _ = new AnyToStringCasterBinder(_ => { }, converter: null!));

        [Test]
        public void Constructor_ATwoWayMode_Throws() =>
            Assert.Throws<InvalidOperationException>(
                () => _ = new AnyToStringCasterBinder(_ => { }, mode: BindMode.TwoWay));

        [Test]
        public void Constructor_AOneWayToSourceMode_Throws() =>
            Assert.Throws<InvalidOperationException>(
                () => _ = new AnyToStringCasterBinder(_ => { }, mode: BindMode.OneWayToSource));

        private sealed class UpperCaseConverter : IConverter<object?, string?>
        {
            public string? Convert(object? value) =>
                value?.ToString()?.ToUpperInvariant();
        }
    }
}
