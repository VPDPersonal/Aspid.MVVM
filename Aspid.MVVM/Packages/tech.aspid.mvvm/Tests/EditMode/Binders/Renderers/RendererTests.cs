using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.Rendering;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="Renderer"/> binders: sorting, shadows and the enabled flag.
    /// </summary>
    [TestFixture]
    public sealed class RendererTests : SceneFixture
    {
        [Test]
        public void SortingOrderAndShadows_ReachAnyRenderer()
        {
            var renderer = Spawn<MeshRenderer>("Renderer");

            ((IBinder<int>)renderer.gameObject.AddComponent<RendererSortingOrderMonoBinder>()).SetValue(7);
            ((IBinder<ShadowCastingMode>)renderer.gameObject.AddComponent<RendererShadowCastingMonoBinder>()).SetValue(ShadowCastingMode.Off);

            Assert.AreEqual(7, renderer.sortingOrder, "The sorting order did not reach the renderer");
            Assert.AreEqual(ShadowCastingMode.Off, renderer.shadowCastingMode, "The shadow mode did not reach the renderer");
        }

        /// <summary>
        /// Unity ignores a sorting layer name no layer has and leaves the object where it was, which looks exactly like
        /// a depth bug — so the binder reports it instead.
        /// </summary>
        [Test]
        public void ASortingLayerThatDoesNotExist_IsReported()
        {
            var renderer = Spawn<MeshRenderer>("Renderer");
            var binder = renderer.gameObject.AddComponent<RendererSortingLayerNameMonoBinder>();

            LogAssert.Expect(LogType.Error, new Regex("No sorting layer named"));
            ((IBinder<string>)binder).SetValue("NoSuchLayer");

            Assert.AreEqual("Default", renderer.sortingLayerName, "The nonexistent layer was written anyway");
        }

        [Test]
        public void TheDefaultSortingLayer_IsAccepted()
        {
            var renderer = Spawn<MeshRenderer>("Renderer");
            var binder = renderer.gameObject.AddComponent<RendererSortingLayerNameMonoBinder>();

            Assert.DoesNotThrow(() => ((IBinder<string>)binder).SetValue("Default"));
            Assert.AreEqual("Default", renderer.sortingLayerName, "The Default layer was not accepted");
        }

        [Test]
        public void RendererEnabledBinder_DrivesTheFlag()
        {
            var renderer = Spawn<MeshRenderer>("Renderer");
            var binder = renderer.gameObject.AddComponent<RendererEnabledMonoBinder>();

            ((IBinder<bool>)binder).SetValue(false);
            Assert.IsFalse(renderer.enabled, "The renderer did not turn off");

            ((IBinder<bool>)binder).SetValue(true);
            Assert.IsTrue(renderer.enabled, "The renderer did not turn back on");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var renderer = Spawn<MeshRenderer>("Renderer");

            Assert.IsTrue(new RendererSortingOrderBinder(renderer).CanBind);
            Assert.IsTrue(new RendererSortingLayerNameBinder(renderer).CanBind);
            Assert.IsTrue(new RendererShadowCastingBinder(renderer).CanBind);
            Assert.IsTrue(new RendererEnabledBinder(renderer).CanBind);
        }
    }
}
