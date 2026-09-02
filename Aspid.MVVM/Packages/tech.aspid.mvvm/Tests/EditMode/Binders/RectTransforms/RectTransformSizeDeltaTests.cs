using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the second reverse channel on the <see cref="RectTransform.sizeDelta"/> binders.
    /// </summary>
    /// <remarks>
    /// The family is built on the Vector3 base, so in <see cref="BindMode.OneWayToSource"/> it must also report a plain <see cref="Vector2"/>.
    /// </remarks>
    [TestFixture]
    public sealed class RectTransformSizeDeltaTests : SceneFixture
    {
        [Test]
        public void SizeDelta_ReportsTheSizeAsAVector2()
        {
            var rect = Spawn<RectTransform>("Rect");
            rect.sizeDelta = new Vector2(120f, 40f);

            var binder = new RectTransformSizeDeltaBinder(rect, mode: BindMode.OneWayToSource);
            var received = default(Vector2);

            binder.Bind(new OneWayToSourceStructBindableMember<Vector2>(value => received = value));

            Assert.AreEqual(new Vector2(120f, 40f), received, "The ViewModel did not receive the size as a Vector2");
        }

        /// <summary>
        /// The Vector3 channel has to keep working: it is what every existing binding to this family uses.
        /// </summary>
        [Test]
        public void SizeDelta_StillReportsTheVector3Channel()
        {
            var rect = Spawn<RectTransform>("Rect");
            rect.sizeDelta = new Vector2(80f, 20f);

            var binder = new RectTransformSizeDeltaBinder(rect, mode: BindMode.OneWayToSource);
            var received = default(Vector3);

            binder.Bind(new OneWayToSourceStructBindableMember<Vector3>(value => received = value));

            Assert.AreEqual(new Vector3(80f, 20f, 0f), received, "The Vector3 channel stopped working");
        }

        [Test]
        public void SizeDelta_TheMonoBinderReportsBothChannels()
        {
            var rect = Spawn<RectTransform>("Rect");
            rect.sizeDelta = new Vector2(10f, 30f);

            var binder = rect.gameObject.AddComponent<RectTransformSizeDeltaMonoBinder>();
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.OneWayToSource;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            var asVector2 = default(Vector2);
            ((IReverseBinder<Vector2>)binder).ValueChanged += value => asVector2 = value;

            var asVector3 = default(Vector3);
            ((IReverseBinder<Vector3>)binder).ValueChanged += value => asVector3 = value;

            binder.Bind(new OneWayToSourceStructBindableMember<Vector2>(_ => { }));

            Assert.AreEqual(new Vector2(10f, 30f), asVector2, "The Vector2 channel did not raise");
            Assert.AreEqual(new Vector3(10f, 30f, 0f), asVector3, "The Vector3 channel did not raise");
        }
    }
}
