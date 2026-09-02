using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="PassthroughConverter{T}"/> — the identity in both directions.
    /// </summary>
    [TestFixture]
    public sealed class PassthroughConverterTests
    {
        [Test]
        public void Passthrough_ReturnsTheInput() =>
            Assert.AreEqual(7, new PassthroughConverter<int>().Convert(7));

        [Test]
        public void Passthrough_ConvertBack_ReturnsTheInput() =>
            Assert.AreEqual(7, new PassthroughConverter<int>().ConvertBack(7));
    }
}
