using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="EulerToQuaternionConverter"/>'s round trip through Unity's 0..360
    /// Euler reading.
    /// </summary>
    [TestFixture]
    public sealed class EulerToQuaternionConverterTests
    {
        [Test]
        public void ConvertBack_RoundTripsThroughTheSameRotation()
        {
            var converter = new EulerToQuaternionConverter();
            var euler = new Vector3(0f, 45f, 0f);

            Assert.AreEqual(euler.y, converter.ConvertBack(converter.Convert(euler)).y, 1e-2f);
        }
    }
}
