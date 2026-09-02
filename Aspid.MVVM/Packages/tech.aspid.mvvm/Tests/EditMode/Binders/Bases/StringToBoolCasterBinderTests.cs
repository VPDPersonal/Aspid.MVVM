using System;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="StringToBoolCasterBinder"/>, the <see cref="IBinder{T}">IBinder&lt;string&gt;</see>
    /// that reads a string's emptiness as a boolean.
    /// </summary>
    [TestFixture]
    public sealed class StringToBoolCasterBinderTests
    {
        [Test]
        public void SetValue_AnEmptyOrNullString_IsTrueByDefault()
        {
            var received = new List<bool>();
            var binder = new StringToBoolCasterBinder(received.Add);

            binder.SetValue(null);
            binder.SetValue(string.Empty);
            binder.SetValue("filled");

            Assert.AreEqual(new List<bool> { true, true, false }, received);
        }

        [Test]
        public void SetValue_Inverted_AFilledStringIsTrue()
        {
            var received = new List<bool>();
            var binder = new StringToBoolCasterBinder(received.Add, isInvert: true);

            binder.SetValue("filled");
            binder.SetValue(string.Empty);

            Assert.AreEqual(new List<bool> { true, false }, received);
        }

        [Test]
        public void SetValue_ACustomConverter_IsUsedInstead()
        {
            var received = new List<bool>();
            var binder = new StringToBoolCasterBinder(received.Add, new EqualsYesConverter());

            binder.SetValue("yes");
            binder.SetValue("no");

            Assert.AreEqual(new List<bool> { true, false }, received, "The custom converter was not used");
        }

        [Test]
        public void Constructor_ANullSetValue_Throws() =>
            Assert.Throws<ArgumentNullException>(() => _ = new StringToBoolCasterBinder(setValue: null!));

        [Test]
        public void Constructor_ANullConverter_Throws() =>
            Assert.Throws<ArgumentNullException>(
                () => _ = new StringToBoolCasterBinder(_ => { }, converter: null!));

        [Test]
        public void Constructor_ATwoWayMode_Throws() =>
            Assert.Throws<InvalidOperationException>(
                () => _ = new StringToBoolCasterBinder(_ => { }, mode: BindMode.TwoWay));

        [Test]
        public void Constructor_AOneWayToSourceMode_Throws() =>
            Assert.Throws<InvalidOperationException>(
                () => _ = new StringToBoolCasterBinder(_ => { }, mode: BindMode.OneWayToSource));

        private sealed class EqualsYesConverter : IConverter<string?, bool>
        {
            public bool Convert(string? value) =>
                value == "yes";
        }
    }
}
