using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="Vector4ToRectOffsetConverter"/>'s component order.
    /// </summary>
    [TestFixture]
    public sealed class Vector4ToRectOffsetConverterTests
    {
        [Test]
        public void Convert_ReadsTheFourNumbersInOrder()
        {
            var padding = new Vector4ToRectOffsetConverter().Convert(new Vector4(1f, 2f, 3f, 4f));

            Assert.AreEqual(1, padding.left);
            Assert.AreEqual(2, padding.right);
            Assert.AreEqual(3, padding.top);
            Assert.AreEqual(4, padding.bottom);
        }
    }
}
