using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the binder pair the generator emits from the declaration in this assembly.
    /// </summary>
    /// <remarks>
    /// The generator has its own tests in its own repository, which run the Roslyn driver over a synthetic compilation.
    /// These are the other half: that the emitted code actually compiles inside Unity, against the real bases, and that
    /// what comes out behaves like a hand-written family — the same property, the same context menu, the same modes.
    /// </remarks>
    [TestFixture]
    public sealed class GeneratedBinderTests : SceneFixture
    {
        [Test]
        public void BothHalvesExist()
        {
            Assert.IsNotNull(typeof(GeneratedCanvasScalerReferencePixelsBinder), "The serializable half was not generated");
            Assert.IsNotNull(typeof(GeneratedCanvasScalerReferencePixelsMonoBinder), "The MonoBehaviour half was not generated");
        }

        [Test]
        public void TheGeneratedMonoBinder_StandsOnTheBaseTheValueTypeAsksFor()
        {
            Assert.IsTrue(
                typeof(ComponentFloatMonoBinder<CanvasScaler>).IsAssignableFrom(typeof(GeneratedCanvasScalerReferencePixelsMonoBinder)),
                "The generated binder does not stand on the float base");
        }

        [Test]
        public void TheGeneratedBinder_WritesTheProperty()
        {
            var gameObject = Spawn("Scaler");

            var scaler = gameObject.AddComponent<CanvasScaler>();
            var binder = gameObject.AddComponent<GeneratedCanvasScalerReferencePixelsMonoBinder>();

            ((IBinder<float>)binder).SetValue(50f);

            Assert.AreEqual(50f, scaler.referencePixelsPerUnit, 0.001f, "The generated binder did not write the property");
        }

        /// <summary>
        /// The menu path and the context-menu name are what the package's own contract tests check on every binder, so a
        /// generated one has to carry them exactly as a hand-written one does.
        /// </summary>
        [Test]
        public void TheGeneratedMonoBinder_CarriesItsMenuAndContextNames()
        {
            var type = typeof(GeneratedCanvasScalerReferencePixelsMonoBinder);

            var menu = (AddComponentMenu)type.GetCustomAttributes(typeof(AddComponentMenu), false)[0];
            var context = (AddBinderContextMenuAttribute)type.GetCustomAttributes(typeof(AddBinderContextMenuAttribute), false)[0];

            Assert.AreEqual("Aspid/MVVM/Binders/UI/CanvasScaler/CanvasScaler Binder – Reference Pixels Per Unit",
                menu.componentMenu, "The menu path did not reach the generated binder");

            Assert.AreEqual(typeof(CanvasScaler), context.Type, "The context-menu type is wrong");
            Assert.Contains("m_ReferencePixelsPerUnit", context.SerializePropertyNames, "The serialized property name did not reach the binder");
        }

        [Test]
        public void TheGeneratedSerializableBinder_AcceptsItsTarget()
        {
            var scaler = Spawn("Scaler").AddComponent<CanvasScaler>();

            Assert.IsTrue(new GeneratedCanvasScalerReferencePixelsBinder(scaler).CanBind);
        }
    }
}
