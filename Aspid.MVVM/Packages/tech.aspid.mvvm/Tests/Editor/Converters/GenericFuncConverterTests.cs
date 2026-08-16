using System;
using NUnit.Framework;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// The documented null contract of <see cref="GenericFuncConverter{TFrom,TTo}"/>.
    /// </summary>
    /// <remarks>
    /// The converter overload delegated with <c>converter.Convert</c> — a method group read off a null
    /// reference, which throws <see cref="NullReferenceException"/> where the constructor promised
    /// <see cref="ArgumentNullException"/>. Every <c>ToConvert</c> extension funnels through here.
    /// </remarks>
    [TestFixture]
    internal sealed class GenericFuncConverterTests
    {
        [Test]
        public void Constructor_NullFunction_ThrowsArgumentNull() =>
            Assert.Throws<ArgumentNullException>(() => _ = new GenericFuncConverter<int, int>((Func<int, int>)null!));

        [Test]
        public void Constructor_NullConverter_ThrowsArgumentNull() =>
            Assert.Throws<ArgumentNullException>(() => _ = new GenericFuncConverter<int, int>((IConverter<int, int>)null!));

        [Test]
        public void ToConvertSpecific_NullConverter_ThrowsArgumentNull() =>
            Assert.Throws<ArgumentNullException>(() => _ = ((IConverter<int, int>)null!).ToConvertSpecific());

        [Test]
        public void ToConvert_NullFunction_ThrowsArgumentNull() =>
            Assert.Throws<ArgumentNullException>(() => _ = ((Func<int, int>)null!).ToConvert());

        [Test]
        public void Convert_WrappedConverter_DelegatesToIt()
        {
            var converter = new GenericFuncConverter<int, int>(new Doubler());
            Assert.AreEqual(42, converter.Convert(21));
        }

        private sealed class Doubler : IConverter<int, int>
        {
            public int Convert(int value) => value * 2;
        }
    }
}
