using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="LookRotationConverter"/> — the zero-direction and zero-up degrades,
    /// and the flattened look.
    /// </summary>
    [TestFixture]
    public sealed class LookRotationConverterTests
    {
        // LookRotation warns and returns identity on a zero vector; checking first keeps the console
        // clean when a target has not been picked yet.
        [Test]
        public void Convert_ZeroDirectionIsTheIdentityWithoutAWarning() =>
            Assert.AreEqual(Quaternion.identity, new LookRotationConverter().Convert(Vector3.zero));

        // An up vector cleared in the Inspector leaves LookRotation with no plane to level against.
        // The converter reports it and looks with world up, so the result is the ordinary one rather
        // than whatever Unity does with a degenerate pair.
        [Test]
        public void Convert_ZeroUp_ReportsItAndLooksWithWorldUp()
        {
            LogAssert.Expect(LogType.Error, new Regex("up vector is zero"));

            var rotation = new LookRotationConverter(Vector3.zero).Convert(Vector3.forward);

            Assert.AreEqual(0f, Quaternion.Angle(Quaternion.identity, rotation), 1e-2f);
        }

        [Test]
        public void Convert_FlattensWhenAsked()
        {
            var rotation = new LookRotationConverter(Vector3.up, flatten: true).Convert(new Vector3(0f, 5f, 1f));

            Assert.AreEqual(0f, rotation.eulerAngles.x, 1e-2f);
        }
    }
}
