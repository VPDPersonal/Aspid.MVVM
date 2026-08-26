using System;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// The documented null contract of <see cref="FuncConverter{TFrom,TTo}"/>.
    /// </summary>
    /// <remarks>
    /// The converter overload delegated with <c>converter.Convert</c> — a method group read off a null
    /// reference, which throws <see cref="NullReferenceException"/> where the constructor promised
    /// <see cref="ArgumentNullException"/>. <see cref="ConverterExtensions.ToConverter{TFrom, TTo}"/>
    /// funnels through here.
    /// </remarks>
    [TestFixture]
    internal sealed class FuncConverterTests
    {
        [Test]
        public void Constructor_NullFunction_ThrowsArgumentNull() =>
            Assert.Throws<ArgumentNullException>(() => _ = new FuncConverter<int, int>((Func<int, int>)null!));

        [Test]
        public void Constructor_NullConverter_ThrowsArgumentNull() =>
            Assert.Throws<ArgumentNullException>(() => _ = new FuncConverter<int, int>((IConverter<int, int>)null!));

        [Test]
        public void ToConvert_NullFunction_ThrowsArgumentNull() =>
            Assert.Throws<ArgumentNullException>(() => _ = ((Func<int, int>)null!).ToConverter());

        [Test]
        public void Convert_WrappedConverter_DelegatesToIt()
        {
            var converter = new FuncConverter<int, int>(new Doubler());
            Assert.AreEqual(42, converter.Convert(21));
        }

        private sealed class Doubler : IConverter<int, int>
        {
            public int Convert(int value) => value * 2;
        }
    }
}
