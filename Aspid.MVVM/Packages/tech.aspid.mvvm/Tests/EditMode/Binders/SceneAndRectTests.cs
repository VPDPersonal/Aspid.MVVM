using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the scene-level properties that had no binder: the object's layer, the transform's parent and sibling
    /// order, and the five <see cref="RectTransform"/> values a stretched panel is described by.
    /// </summary>
    /// <remarks>
    /// The package could bind a tag and not a layer, a position and not a parent, an anchored position and not the
    /// anchors — so the parts of a layout that a responsive UI actually changes were the parts it could not reach.
    /// </remarks>
    [TestFixture]
    public sealed class SceneAndRectTests
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

        #region Layer
        [Test]
        public void Layer_ReachesTheObject()
        {
            var gameObject = NewGameObject("Object");
            var binder = gameObject.AddComponent<GameObjectLayerMonoBinder>();

            ((IBinder<int>)binder).SetValue(5);

            Assert.AreEqual(5, gameObject.layer, "Слой не доехал до объекта");
        }

        /// <summary>
        /// Unity has 32 layers and silently keeps the previous one for an index outside them, so the binder says so
        /// instead.
        /// </summary>
        [Test]
        public void ALayerThatDoesNotExist_IsReported()
        {
            var gameObject = NewGameObject("Object");
            var binder = gameObject.AddComponent<GameObjectLayerMonoBinder>();

            LogAssert.Expect(LogType.Error, new Regex("Layer 40 does not exist"));
            ((IBinder<int>)binder).SetValue(40);

            Assert.AreEqual(0, gameObject.layer, "Несуществующий слой всё же записался");
        }

        [Test]
        public void Layer_OneWayToSource_ReportsTheCurrentLayer()
        {
            var gameObject = NewGameObject("Object");
            gameObject.layer = 9;

            var binder = new GameObjectLayerBinder(gameObject, mode: BindMode.OneWayToSource);
            var received = -1;

            binder.Bind(new OneWayToSourceStructBindableMember<int>(value => received = value));

            Assert.AreEqual(9, received, "ViewModel не получила текущий слой");
        }
        #endregion

        #region Parent and sibling order
        [Test]
        public void Parent_ReachesTheTransform_AndKeepsTheLocalPosition()
        {
            var child = NewGameObject("Child");
            var slot = NewGameObject("Slot");

            slot.transform.position = new Vector3(10f, 0f, 0f);
            child.transform.localPosition = new Vector3(1f, 0f, 0f);

            var binder = child.AddComponent<TransformParentMonoBinder>();
            ((IBinder<Transform>)binder).SetValue(slot.transform);

            Assert.AreSame(slot.transform, child.transform.parent, "Родитель не сменился");
            Assert.AreEqual(new Vector3(1f, 0f, 0f), child.transform.localPosition, "Локальная позиция не сохранена");
        }

        /// <summary>
        /// A destroyed transform must not be assigned as a parent: the object would be reported as a child of
        /// something that no longer exists.
        /// </summary>
        /// <remarks>
        /// The child is detached before the slot is destroyed, because destroying a parent destroys its children —
        /// which would leave nothing to assert on.
        /// </remarks>
        [Test]
        public void ADestroyedParent_IsNotAssigned()
        {
            var child = NewGameObject("Child");
            var slot = NewGameObject("Slot");

            var binder = child.AddComponent<TransformParentMonoBinder>();
            ((IBinder<Transform>)binder).SetValue(slot.transform);
            Assert.AreSame(slot.transform, child.transform.parent, "Родитель не сменился");

            var slotTransform = slot.transform;
            child.transform.SetParent(null, worldPositionStays: false);
            Object.DestroyImmediate(slot);

            ((IBinder<Transform>)binder).SetValue(slotTransform);

            Assert.IsFalse(child.transform.parent, "Уничтоженный трансформ стал родителем");
        }

        [Test]
        public void SiblingIndex_IsClampedToTheSiblingsThatExist()
        {
            var parent = NewGameObject("Parent");
            var first = NewChild(parent, "First");
            var second = NewChild(parent, "Second");

            var binder = first.AddComponent<TransformSiblingIndexMonoBinder>();
            ((IBinder<int>)binder).SetValue(99);

            Assert.AreEqual(1, first.transform.GetSiblingIndex(), "Индекс не обрезан по числу соседей");
            Assert.AreEqual(0, second.transform.GetSiblingIndex(), "Второй объект не сдвинулся вперёд");
        }

        [Test]
        public void SiblingIndex_OneWayToSource_ReportsWhereTheObjectIs()
        {
            var parent = NewGameObject("Parent");
            NewChild(parent, "First");
            var second = NewChild(parent, "Second");

            var binder = new TransformSiblingIndexBinder(second.transform, BindMode.OneWayToSource);
            var received = -1;

            binder.Bind(new OneWayToSourceStructBindableMember<int>(value => received = value));

            Assert.AreEqual(1, received, "ViewModel не получила текущий индекс");
        }
        #endregion

        #region RectTransform
        [Test]
        public void TheAnchorsAndPivot_ReachTheRect()
        {
            var rect = NewRect();

            ((IBinder<Vector2>)rect.gameObject.AddComponent<RectTransformAnchorMinMonoBinder>()).SetValue(new Vector2(0.25f, 0.25f));
            ((IBinder<Vector2>)rect.gameObject.AddComponent<RectTransformAnchorMaxMonoBinder>()).SetValue(new Vector2(0.75f, 0.75f));
            ((IBinder<Vector2>)rect.gameObject.AddComponent<RectTransformPivotMonoBinder>()).SetValue(new Vector2(1f, 0f));

            Assert.AreEqual(new Vector2(0.25f, 0.25f), rect.anchorMin, "anchorMin не доехал");
            Assert.AreEqual(new Vector2(0.75f, 0.75f), rect.anchorMax, "anchorMax не доехал");
            Assert.AreEqual(new Vector2(1f, 0f), rect.pivot, "pivot не доехал");
        }

        /// <summary>
        /// Anchors outside 0..1 are how an element is stretched past its parent, so they are kept; a non-finite value
        /// would take the element off the screen and is refused.
        /// </summary>
        [Test]
        public void TheAnchors_KeepValuesOutsideTheUnitRange_AndRefuseNonFinite()
        {
            var rect = NewRect();
            var binder = rect.gameObject.AddComponent<RectTransformAnchorMinMonoBinder>();

            ((IBinder<Vector2>)binder).SetValue(new Vector2(-0.5f, 1.5f));
            Assert.AreEqual(new Vector2(-0.5f, 1.5f), rect.anchorMin, "Значение вне 0..1 не сохранено");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Vector2>)binder).SetValue(new Vector2(float.NaN, 0f));
            Assert.AreEqual(new Vector2(-0.5f, 1.5f), rect.anchorMin, "Нефинитное значение дошло до rect");
        }

        [Test]
        public void TheOffsets_KeepNegativeValues()
        {
            var rect = NewRect();

            ((IBinder<Vector2>)rect.gameObject.AddComponent<RectTransformOffsetMinMonoBinder>()).SetValue(new Vector2(-8f, -8f));
            ((IBinder<Vector2>)rect.gameObject.AddComponent<RectTransformOffsetMaxMonoBinder>()).SetValue(new Vector2(8f, 8f));

            Assert.AreEqual(new Vector2(-8f, -8f), rect.offsetMin, "offsetMin не доехал");
            Assert.AreEqual(new Vector2(8f, 8f), rect.offsetMax, "offsetMax не доехал");
        }

        /// <summary>
        /// The reason these are on the Vector2 base: a Vector3 one would report a third component the rect has not got.
        /// </summary>
        [Test]
        public void TheRectBinders_ReportAVector2Back()
        {
            var rect = NewRect();
            rect.pivot = new Vector2(0.3f, 0.7f);

            var binder = new RectTransformPivotBinder(rect, mode: BindMode.OneWayToSource);
            var received = default(Vector2);

            binder.Bind(new OneWayToSourceStructBindableMember<Vector2>(value => received = value));

            Assert.AreEqual(new Vector2(0.3f, 0.7f), received, "ViewModel получила не тот pivot");
        }
        #endregion

        #region Helpers
        private RectTransform NewRect()
        {
            var gameObject = NewGameObject("Rect");
            return gameObject.AddComponent<RectTransform>();
        }

        private GameObject NewChild(GameObject parent, string name)
        {
            var child = NewGameObject(name);
            child.transform.SetParent(parent.transform, worldPositionStays: false);

            return child;
        }

        private GameObject NewGameObject(string name)
        {
            var gameObject = new GameObject(name);
            _spawned.Add(gameObject);

            return gameObject;
        }
        #endregion
    }
}
