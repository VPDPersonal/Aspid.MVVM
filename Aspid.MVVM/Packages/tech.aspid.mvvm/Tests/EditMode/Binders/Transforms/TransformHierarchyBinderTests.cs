using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="Transform"/> hierarchy binders: parent and sibling index.
    /// </summary>
    [TestFixture]
    public sealed class TransformHierarchyBinderTests : SceneFixture
    {
        [Test]
        public void Parent_ReachesTheTransform_AndKeepsTheLocalPosition()
        {
            var child = Spawn("Child");
            var slot = Spawn("Slot");

            slot.transform.position = new Vector3(10f, 0f, 0f);
            child.transform.localPosition = new Vector3(1f, 0f, 0f);

            var binder = child.AddComponent<TransformParentMonoBinder>();
            ((IBinder<Transform>)binder).SetValue(slot.transform);

            Assert.AreSame(slot.transform, child.transform.parent, "The parent did not change");
            Assert.AreEqual(new Vector3(1f, 0f, 0f), child.transform.localPosition, "The local position was not kept");
        }

        /// <summary>
        /// A destroyed transform must not be assigned as a parent: the object would be reported as a child of
        /// something that no longer exists. The child is detached before the slot is destroyed, because destroying
        /// a parent destroys its children too.
        /// </summary>
        [Test]
        public void ADestroyedParent_IsNotAssigned()
        {
            var child = Spawn("Child");
            var slot = Spawn("Slot");

            var binder = child.AddComponent<TransformParentMonoBinder>();
            ((IBinder<Transform>)binder).SetValue(slot.transform);
            Assert.AreSame(slot.transform, child.transform.parent, "The parent did not change");

            var slotTransform = slot.transform;
            child.transform.SetParent(null, worldPositionStays: false);
            Destroy(slot);

            ((IBinder<Transform>)binder).SetValue(slotTransform);

            Assert.IsFalse(child.transform.parent, "A destroyed transform became the parent");
        }

        [Test]
        public void SiblingIndex_IsClampedToTheSiblingsThatExist()
        {
            var parent = Spawn("Parent");
            var first = NewChild(parent, "First");
            var second = NewChild(parent, "Second");

            var binder = first.AddComponent<TransformSiblingIndexMonoBinder>();
            ((IBinder<int>)binder).SetValue(99);

            Assert.AreEqual(1, first.transform.GetSiblingIndex(), "The index was not clamped to the sibling count");
            Assert.AreEqual(0, second.transform.GetSiblingIndex(), "The second object did not move forward");
        }

        [Test]
        public void SiblingIndex_OneWayToSource_ReportsWhereTheObjectIs()
        {
            var parent = Spawn("Parent");
            NewChild(parent, "First");
            var second = NewChild(parent, "Second");

            var binder = new TransformSiblingIndexBinder(second.transform, BindMode.OneWayToSource);
            var received = -1;

            binder.Bind(new OneWayToSourceStructBindableMember<int>(value => received = value));

            Assert.AreEqual(1, received, "The ViewModel did not receive the current index");
        }

        private GameObject NewChild(GameObject parent, string name)
        {
            var child = Spawn(name);
            child.transform.SetParent(parent.transform, worldPositionStays: false);

            return child;
        }
    }
}
