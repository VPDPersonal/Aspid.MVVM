using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="VectorToVectorIntConverter"/> — both widths, the rounding mode, the
    /// lossy <c>ConvertBack</c>, and the undeclared-mode fallback.
    /// </summary>
    [TestFixture]
    public sealed class VectorToVectorIntConverterTests
    {
        [Test]
        public void Vector3ToVector3Int_RoundTrips()
        {
            var converter = (ITwoWayConverter<Vector3, Vector3Int>)new VectorToVectorIntConverter();

            Assert.AreEqual(new Vector3Int(1, 2, 3), converter.Convert(new Vector3(1.4f, 2.4f, 3.4f)));
            Assert.AreEqual(new Vector3(1f, 2f, 3f), converter.ConvertBack(new Vector3Int(1, 2, 3)));
        }

        [Test]
        public void Vector2ToVector2Int_Floors() =>
            Assert.AreEqual(
                new Vector2Int(1, 2),
                new VectorToVectorIntConverter(RoundMode.Floor).Convert(new Vector2(1.9f, 2.9f)));

        // The mode is a serialized field, so an undeclared value survives a reordered enum. Rounding
        // to nearest is the mode a new converter starts in, and the inputs separate it from the other
        // three: 1.4 rounds down where Ceil would raise it, 2.6 rounds up where Floor and Truncate
        // would drop it. No other mode answers this pair.
        [Test]
        public void Vector2ToVector2Int_UndeclaredMode_ReportsItAndRoundsToNearest()
        {
            LogAssert.Expect(LogType.Error, new Regex("VectorToVectorIntConverter.*not a declared RoundMode"));

            Assert.AreEqual(
                new Vector2Int(1, 3),
                new VectorToVectorIntConverter((RoundMode)42).Convert(new Vector2(1.4f, 2.6f)));
        }

        // The third axis is negative because that is where Floor and Truncate part company, so the
        // wider overload rules out all three of the others rather than two of them.
        [Test]
        public void Vector3ToVector3Int_UndeclaredMode_ReportsItAndRoundsToNearest()
        {
            LogAssert.Expect(LogType.Error, new Regex("VectorToVectorIntConverter.*not a declared RoundMode"));

            Assert.AreEqual(
                new Vector3Int(1, 3, -1),
                ((IConverter<Vector3, Vector3Int>)new VectorToVectorIntConverter((RoundMode)42))
                    .Convert(new Vector3(1.4f, 2.6f, -1.4f)));
        }
    }
}
