using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the second reverse channel on the sizeDelta binders, and for <see cref="RectMask2D.padding"/>.
    /// </summary>
    /// <remarks>
    /// A rect's size is two numbers. The sizeDelta family is built on the Vector3 base, so in
    /// <see cref="BindMode.OneWayToSource"/> it reported <c>Vector3(width, height, 0)</c> — a value the property never
    /// held, and one a ViewModel field of type <see cref="Vector2"/> could not receive at all.
    /// </remarks>
    [TestFixture]
    public sealed class SizeDeltaAndMaskTests
    {
        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.DestroyImmediate(gameObject);
            }

            _spawned.Clear();
        }

        [Test]
        public void SizeDelta_ReportsTheSizeAsAVector2()
        {
            var rect = NewRect();
            rect.sizeDelta = new Vector2(120f, 40f);

            var binder = new RectTransformSizeDeltaBinder(rect, mode: BindMode.OneWayToSource);
            var received = default(Vector2);

            binder.Bind(new OneWayToSourceStructBindableMember<Vector2>(value => received = value));

            Assert.AreEqual(new Vector2(120f, 40f), received, "ViewModel не получила размер как Vector2");
        }

        /// <summary>
        /// The Vector3 channel has to keep working: it is what every existing binding to this family uses.
        /// </summary>
        [Test]
        public void SizeDelta_StillReportsTheVector3Channel()
        {
            var rect = NewRect();
            rect.sizeDelta = new Vector2(80f, 20f);

            var binder = new RectTransformSizeDeltaBinder(rect, mode: BindMode.OneWayToSource);
            var received = default(Vector3);

            binder.Bind(new OneWayToSourceStructBindableMember<Vector3>(value => received = value));

            Assert.AreEqual(new Vector3(80f, 20f, 0f), received, "Vector3-канал перестал работать");
        }

        [Test]
        public void SizeDelta_TheMonoBinderReportsBothChannels()
        {
            var rect = NewRect();
            rect.sizeDelta = new Vector2(10f, 30f);

            var binder = rect.gameObject.AddComponent<RectTransformSizeDeltaMonoBinder>();
            var serializedObject = new UnityEditor.SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.OneWayToSource;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            var asVector2 = default(Vector2);
            ((IReverseBinder<Vector2>)binder).ValueChanged += value => asVector2 = value;

            var asVector3 = default(Vector3);
            ((IReverseBinder<Vector3>)binder).ValueChanged += value => asVector3 = value;

            binder.Bind(new OneWayToSourceStructBindableMember<Vector2>(_ => { }));

            Assert.AreEqual(new Vector2(10f, 30f), asVector2, "Vector2-канал не поднялся");
            Assert.AreEqual(new Vector3(10f, 30f, 0f), asVector3, "Vector3-канал не поднялся");
        }

        /// <summary>
        /// The padding is a <see cref="Vector4"/> and the bound value is a <see cref="Vector3"/>, so the fourth side must
        /// keep what it had — otherwise binding three sides silently zeroes the fourth.
        /// </summary>
        [Test]
        public void MaskPadding_KeepsTheFourthSide()
        {
            var rect = NewRect();
            var mask = rect.gameObject.AddComponent<RectMask2D>();

            mask.padding = new Vector4(1f, 2f, 3f, 4f);

            var binder = rect.gameObject.AddComponent<RectMask2DPaddingMonoBinder>();
            ((IBinder<Vector3>)binder).SetValue(new Vector3(5f, 6f, 7f));

            Assert.AreEqual(new Vector4(5f, 6f, 7f, 4f), mask.padding, "Четвёртая сторона не сохранена");
        }

        [Test]
        public void MaskPadding_RefusesANonFiniteComponent()
        {
            var rect = NewRect();
            var mask = rect.gameObject.AddComponent<RectMask2D>();

            mask.padding = new Vector4(1f, 1f, 1f, 1f);

            var binder = rect.gameObject.AddComponent<RectMask2DPaddingMonoBinder>();
            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Vector3>)binder).SetValue(new Vector3(2f, float.NaN, 2f));

            Assert.AreEqual(new Vector4(1f, 1f, 1f, 1f), mask.padding, "Нефинитная компонента дошла до маски");
        }

        private RectTransform NewRect()
        {
            var gameObject = new GameObject("Rect");
            _spawned.Add(gameObject);

            return gameObject.AddComponent<RectTransform>();
        }
    }
}
