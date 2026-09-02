using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="FloatToVectorConverter"/> —
    /// the axis mask and the base value used for the axes not written.
    /// </summary>
    [TestFixture]
    public sealed class FloatToVectorConverterTests
    {
        [Test]
        public void Vector2_WritesBothAxesByDefault()
        {
            var result = ((IConverter<float, Vector2>)new FloatToVectorConverter(AxisMask.X | AxisMask.Y)).Convert(5f);

            Assert.AreEqual(new Vector2(5f, 5f), result);
        }

        [Test]
        public void Vector2_WritesOnlyTheChosenAxis()
        {
            var result = ((IConverter<float, Vector2>)new FloatToVectorConverter(AxisMask.X, new Vector4(9f, 9f, 9f, 9f))).Convert(5f);

            Assert.AreEqual(new Vector2(5f, 9f), result);
        }

        [Test]
        public void Vector3_WritesEveryAxisByDefault()
        {
            var result = new FloatToVectorConverter(AxisMask.All).Convert(5f);

            Assert.AreEqual(new Vector3(5f, 5f, 5f), result);
        }

        [Test]
        public void Vector3_WritesOnlyTheChosenAxes()
        {
            var result = new FloatToVectorConverter(AxisMask.X | AxisMask.Z, new Vector4(1f, 9f, 1f, 1f)).Convert(5f);

            Assert.AreEqual(new Vector3(5f, 9f, 5f), result);
        }
    }
}
