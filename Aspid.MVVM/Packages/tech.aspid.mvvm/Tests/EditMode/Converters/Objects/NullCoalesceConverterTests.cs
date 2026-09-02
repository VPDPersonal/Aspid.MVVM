using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="NullCoalesceConverter{T}"/>, including how it treats a destroyed
    /// <see cref="UnityEngine.Object"/> on the bound side and on the fallback side.
    /// </summary>
    [TestFixture]
    public sealed class NullCoalesceConverterTests : SceneFixture
    {
        [Test]
        public void Convert_SubstitutesTheFallback()
        {
            Assert.AreEqual("x", new NullCoalesceConverter<string>("x").Convert(null));
            Assert.AreEqual("abc", new NullCoalesceConverter<string>("x").Convert("abc"));
        }

        [Test]
        public void DestroyedUnityObject_ReturnsTheFallback()
        {
            var icon = Spawn(nameof(DestroyedUnityObject_ReturnsTheFallback));
            var fallback = Spawn("Fallback");
            var converter = new NullCoalesceConverter<GameObject>(fallback);

            // While it is alive it has to pass straight through, or the fallback would be permanent.
            Assert.AreSame(icon, converter.Convert(icon));

            Destroy(icon);

            // The managed reference is still alive at this point, so `??` and `is null` would both
            // hand the destroyed object to the binder. Only Unity's overloaded == catches it.
            Assert.AreSame(fallback, converter.Convert(icon));
        }

        // The Unity check is a runtime type test on the value, not a constraint on T, so a converter
        // declared over object still catches a destroyed asset flowing through an object-typed binding.
        [Test]
        public void DestroyedUnityObjectUnderObjectOfT_ReturnsTheFallback()
        {
            var icon = Spawn(nameof(DestroyedUnityObjectUnderObjectOfT_ReturnsTheFallback));
            var converter = new NullCoalesceConverter<object>("placeholder");

            Assert.AreSame(icon, converter.Convert(icon));

            Destroy(icon);

            Assert.AreEqual("placeholder", converter.Convert(icon));
        }

        // A fallback destroyed after the converter was built meets the same emptiness check the bound
        // value gets, and is reported — then returned exactly as authored rather than turned back into
        // null: the converter guarantees "not the bound value", not "not destroyed".
        [Test]
        public void DestroyedFallback_IsReportedAndStillReturned()
        {
            var fallback = Spawn(nameof(DestroyedFallback_IsReportedAndStillReturned));
            var converter = new NullCoalesceConverter<GameObject>(fallback);

            Destroy(fallback);
            LogAssert.Expect(LogType.Error, new Regex("fallback is missing or destroyed"));

            Assert.AreSame(fallback, converter.Convert(null));
        }

        // The constructor runs that same check rather than a plain ??, which would read a destroyed
        // object as a perfectly good fallback and postpone the complaint to the first conversion.
        [Test]
        public void DestroyedFallbackInTheConstructor_Throws()
        {
            var fallback = Spawn(nameof(DestroyedFallbackInTheConstructor_Throws));
            Destroy(fallback);

            Assert.Throws<ArgumentNullException>(() => new NullCoalesceConverter<GameObject>(fallback));
        }

        // An unassigned fallback reduces the converter to a no-op that forwards the very null it exists
        // to replace. The constructor rejects that shape, so it only ever arrives from the Inspector —
        // built here the way the type picker builds it. Reported on every conversion, not once: the
        // second call is what pins it.
        [Test]
        public void MissingFallback_IsReportedEveryTime()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("fallback is missing or destroyed"));

            var converter = (NullCoalesceConverter<object>)Activator.CreateInstance(
                typeof(NullCoalesceConverter<object>), nonPublic: true);

            Assert.IsNull(converter.Convert(null));
            converter.Convert(null);
        }
    }
}
