using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="QuaternionToEulerConverter"/>'s signed-180 normalization and its
    /// opt-out.
    /// </summary>
    [TestFixture]
    public sealed class QuaternionToEulerConverterTests
    {
        // Unity reports Euler angles in 0..360, so a needle a little past zero reads as 359 rather
        // than -1 unless it is folded into signed-180.
        [Test]
        public void Convert_NormalizesToSigned180()
        {
            var rotation = Quaternion.Euler(0f, 0f, 350f);

            Assert.AreEqual(-10f, new QuaternionToEulerConverter().Convert(rotation).z, 1e-2f);
            Assert.AreEqual(350f, new QuaternionToEulerConverter(false).Convert(rotation).z, 1e-2f);
        }
    }
}
