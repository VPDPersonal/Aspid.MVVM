using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="ValueToStringCasterBinder{T}"/>, the <see cref="IBinder{T}"/> that stringifies a typed value.
    /// </summary>
    [TestFixture]
    public sealed class ValueToStringCasterBinderTests
    {
        [Test]
        public void SetValue_WithAFormat_AppliesIt()
        {
            var received = new List<string?>();
            var binder = new ValueToStringCasterBinder<int>(received.Add, "Value: {0}");

            binder.SetValue(42);

            Assert.AreEqual(new List<string?> { "Value: 42" }, received);
        }

        [Test]
        public void SetValue_ANullValue_ForwardsNull()
        {
            var received = new List<string?>();
            var binder = new ValueToStringCasterBinder<string>(received.Add, string.Empty);

            binder.SetValue(null);

            Assert.AreEqual(new List<string?> { null }, received);
        }

        [Test]
        public void SetValue_ACustomConverter_IsUsedInstead()
        {
            var received = new List<string?>();
            var binder = new ValueToStringCasterBinder<int>(received.Add, new DoublingConverter());

            binder.SetValue(21);

            Assert.AreEqual(new List<string?> { "42" }, received, "The custom converter was not used");
        }

        [Test]
        public void Constructor_ANullSetValue_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => _ = new ValueToStringCasterBinder<int>(setValue: null!, format: string.Empty));

        [Test]
        public void Constructor_ANullConverter_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => _ = new ValueToStringCasterBinder<int>(_ => { }, converter: null!));

        [Test]
        public void Constructor_ATwoWayMode_Throws() =>
            Assert.Throws<InvalidOperationException>(
                () => _ = new ValueToStringCasterBinder<int>(_ => { }, string.Empty, mode: BindMode.TwoWay));

        [Test]
        public void Constructor_AOneWayToSourceMode_Throws() =>
            Assert.Throws<InvalidOperationException>(
                () => _ = new ValueToStringCasterBinder<int>(_ => { }, string.Empty, mode: BindMode.OneWayToSource));

        private sealed class DoublingConverter : IConverter<int, string?>
        {
            public string? Convert(int value) =>
                (value * 2).ToString();
        }
    }
}
