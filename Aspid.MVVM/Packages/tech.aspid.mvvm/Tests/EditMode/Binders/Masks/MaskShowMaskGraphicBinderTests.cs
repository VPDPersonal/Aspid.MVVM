using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="Mask.showMaskGraphic"/> binder.
    /// </summary>
    [TestFixture]
    public sealed class MaskShowMaskGraphicBinderTests : SceneFixture
    {
        [Test]
        public void ShowMaskGraphic_ReachesTheMask()
        {
            var gameObject = Spawn("Mask");
            gameObject.AddComponent<Image>();

            var mask = gameObject.AddComponent<Mask>();
            var binder = gameObject.AddComponent<MaskShowMaskGraphicMonoBinder>();

            ((IBinder<bool>)binder).SetValue(false);

            Assert.IsFalse(mask.showMaskGraphic, "showMaskGraphic did not reach the mask");
        }
    }
}
