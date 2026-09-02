using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="ContentSizeFitter"/> binders.
    /// </summary>
    [TestFixture]
    public sealed class ContentSizeFitterTests : SceneFixture
    {
        [Test]
        public void ContentSizeFitter_BothAxesAreBindable()
        {
            var fitter = Spawn<ContentSizeFitter>("ContentSizeFitter");
            var horizontal = fitter.gameObject.AddComponent<ContentSizeFitterHorizontalFitMonoBinder>();
            var vertical = fitter.gameObject.AddComponent<ContentSizeFitterVerticalFitMonoBinder>();

            ((IBinder<ContentSizeFitter.FitMode>)horizontal).SetValue(ContentSizeFitter.FitMode.PreferredSize);
            ((IBinder<ContentSizeFitter.FitMode>)vertical).SetValue(ContentSizeFitter.FitMode.Unconstrained);

            Assert.AreEqual(ContentSizeFitter.FitMode.PreferredSize, fitter.horizontalFit, "The horizontal mode did not reach the fitter");
            Assert.AreEqual(ContentSizeFitter.FitMode.Unconstrained, fitter.verticalFit, "The vertical mode did not reach the fitter");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var fitter = Spawn<ContentSizeFitter>("ContentSizeFitter");

            Assert.IsTrue(new ContentSizeFitterHorizontalFitBinder(fitter).CanBind);
            Assert.IsTrue(new ContentSizeFitterVerticalFitBinder(fitter).CanBind);
        }
    }
}
