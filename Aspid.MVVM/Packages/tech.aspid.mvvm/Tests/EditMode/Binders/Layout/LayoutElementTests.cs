using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="LayoutElement"/> binders.
    /// </summary>
    [TestFixture]
    public sealed class LayoutElementTests : SceneFixture
    {
        [Test]
        public void PreferredSizeBinders_ReachTheLayoutElement()
        {
            var element = Spawn<LayoutElement>("Layout");

            var width = element.gameObject.AddComponent<LayoutElementPreferredWidthMonoBinder>();
            var height = element.gameObject.AddComponent<LayoutElementPreferredHeightMonoBinder>();

            ((IBinder<float>)width).SetValue(120f);
            ((IBinder<float>)height).SetValue(48f);

            Assert.AreEqual(120f, element.preferredWidth, 0.001f);
            Assert.AreEqual(48f, element.preferredHeight, 0.001f);
        }

        /// <summary>
        /// A negative preferred size means "no preference" to Unity, so it is passed through rather than clamped.
        /// </summary>
        [Test]
        public void ANegativePreferredSize_MeansNoPreferenceAndIsPassedThrough()
        {
            var element = Spawn<LayoutElement>("Layout");
            var binder = element.gameObject.AddComponent<LayoutElementPreferredWidthMonoBinder>();

            ((IBinder<float>)binder).SetValue(-1f);

            Assert.AreEqual(-1f, element.preferredWidth, 0.001f, "The negative value was clamped");
        }

        [Test]
        public void IgnoreLayoutBinder_TakesTheElementOutOfTheFlow()
        {
            var element = Spawn<LayoutElement>("Layout");
            var binder = element.gameObject.AddComponent<LayoutElementIgnoreLayoutMonoBinder>();

            ((IBinder<bool>)binder).SetValue(true);

            Assert.IsTrue(element.ignoreLayout, "The element was not taken out of the layout");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var element = Spawn<LayoutElement>("Layout");

            Assert.IsTrue(new LayoutElementPreferredWidthBinder(element).CanBind);
            Assert.IsTrue(new LayoutElementFlexibleHeightBinder(element).CanBind);
            Assert.IsTrue(new LayoutElementIgnoreLayoutBinder(element).CanBind);
        }
    }
}
