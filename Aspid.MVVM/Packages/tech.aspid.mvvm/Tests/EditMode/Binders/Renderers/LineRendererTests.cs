using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="LineRenderer"/> width, loop and colour binders.
    /// </summary>
    [TestFixture]
    public sealed class LineRendererTests : SceneFixture
    {
        [Test]
        public void TheLineRendererOptions_ReachTheRenderer()
        {
            var line = Spawn<LineRenderer>("LineRenderer");

            ((IBinder<float>)line.gameObject.AddComponent<LineRendererWidthMultiplierMonoBinder>()).SetValue(3f);
            ((IBinder<bool>)line.gameObject.AddComponent<LineRendererLoopMonoBinder>()).SetValue(true);

            Assert.AreEqual(3f, line.widthMultiplier, 0.001f, "The width multiplier did not reach the renderer");
            Assert.IsTrue(line.loop, "The loop flag did not reach the renderer");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var line = Spawn<LineRenderer>("LineRenderer");

            Assert.IsTrue(new LineRendererWidthMultiplierBinder(line).CanBind);
            Assert.IsTrue(new LineRendererLoopBinder(line).CanBind);
        }

        [Test]
        public void GetColor_WithStartAndEndMode_ReturnsStartColorInsteadOfThrowing()
        {
            var line = Spawn<LineRenderer>("LineRenderer");
            line.startColor = Color.red;
            line.endColor = Color.green;

            Assert.AreEqual(Color.red, line.GetColor(LineRendererColorMode.StartAndEnd));
        }

        /// <summary>
        /// <c>StartAndEnd</c> is the MonoBinder's default color mode, so in
        /// <see cref="BindMode.OneWayToSource"/> the very first <c>Bind</c> reads the property back and used to throw
        /// before the ViewModel ever saw a value.
        /// </summary>
        [Test]
        public void LineRendererColorMonoBinder_OneWayToSource_BindsWithDefaultColorMode()
        {
            var line = Spawn<LineRenderer>("LineRenderer");
            line.startColor = Color.red;

            var binder = line.gameObject.AddComponent<LineRendererColorMonoBinder>();
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.OneWayToSource;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            var received = default(Color);
            var member = new OneWayToSourceStructBindableMember<Color>(value => received = value);

            Assert.DoesNotThrow(() => binder.Bind(member));
            Assert.AreEqual(Color.red, received);
        }
    }
}
