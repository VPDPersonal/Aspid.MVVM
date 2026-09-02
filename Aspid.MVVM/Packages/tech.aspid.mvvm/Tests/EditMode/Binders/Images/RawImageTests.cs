using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="RawImage.uvRect"/> binder.
    /// </summary>
    [TestFixture]
    public sealed class RawImageTests : SceneFixture
    {
        [Test]
        public void UvRect_ReachesTheRawImage_AndRefusesANonFiniteComponent()
        {
            var raw = Spawn<RawImage>("RawImage");
            var binder = raw.gameObject.AddComponent<RawImageUvRectMonoBinder>();

            ((IBinder<Rect>)binder).SetValue(new Rect(0f, 0.5f, 2f, 2f));
            Assert.AreEqual(new Rect(0f, 0.5f, 2f, 2f), raw.uvRect, "The UV rect did not reach the RawImage");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Rect>)binder).SetValue(new Rect(0f, float.NaN, 1f, 1f));
            Assert.AreEqual(new Rect(0f, 0.5f, 2f, 2f), raw.uvRect, "A non-finite component reached the RawImage");
        }

        [Test]
        public void TheSerializableTwin_AcceptsItsTarget()
        {
            var raw = Spawn<RawImage>("RawImage");

            Assert.IsTrue(new RawImageUvRectBinder(raw).CanBind);
        }
    }
}
