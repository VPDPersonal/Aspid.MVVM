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
    /// Tests for the <see cref="Shadow"/>/<see cref="Outline"/> effect binders: colour and distance.
    /// </summary>
    [TestFixture]
    public sealed class ShadowEffectBinderTests : SceneFixture
    {
        [Test]
        public void TheShadowBinders_ReachTheEffect()
        {
            var gameObject = Spawn("Shadow");
            gameObject.AddComponent<Image>();

            var shadow = gameObject.AddComponent<Outline>();
            var color = gameObject.AddComponent<ShadowEffectColorMonoBinder>();
            var distance = gameObject.AddComponent<ShadowEffectDistanceMonoBinder>();

            ((IBinder<Color>)color).SetValue(Color.red);
            ((IBinder<Vector2>)distance).SetValue(new Vector2(-2f, 3f));

            Assert.AreEqual(Color.red, shadow.effectColor, "The effect colour did not reach the shadow");
            Assert.AreEqual(new Vector2(-2f, 3f), shadow.effectDistance, "The effect distance did not reach the shadow");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Vector2>)distance).SetValue(new Vector2(float.NaN, 0f));
            Assert.AreEqual(new Vector2(-2f, 3f), shadow.effectDistance, "A non-finite distance reached the effect");
        }
    }
}
