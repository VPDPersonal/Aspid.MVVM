using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the 3D collider properties: the capsule's height and direction, the per-collider layer masks, the
    /// contact offset, and the mesh cooking options.
    /// </summary>
    [TestFixture]
    public sealed class ColliderBinderTests : SceneFixture
    {
        [Test]
        public void CapsuleHeight_ReachesTheCollider_AndIsNeverNegative()
        {
            var collider = Spawn<CapsuleCollider>("CapsuleCollider");
            var binder = collider.gameObject.AddComponent<CapsuleColliderHeightMonoBinder>();

            ((IBinder<float>)binder).SetValue(3f);
            Assert.AreEqual(3f, collider.height, 0.001f, "The height did not reach the collider");

            ((IBinder<float>)binder).SetValue(-1f);
            Assert.AreEqual(0f, collider.height, 0.001f, "A negative height was not clamped");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.IsFalse(float.IsNaN(collider.height), "NaN reached the collider");
        }

        /// <summary>
        /// Unity accepts any integer as the capsule's axis and then behaves as if it were zero, so the binder clamps
        /// to the three axes that exist.
        /// </summary>
        [Test]
        public void CapsuleDirection_IsClampedToTheThreeAxes()
        {
            var collider = Spawn<CapsuleCollider>("CapsuleCollider");
            var binder = collider.gameObject.AddComponent<CapsuleColliderDirectionMonoBinder>();

            ((IBinder<int>)binder).SetValue(2);
            Assert.AreEqual(2, collider.direction, "The axis did not reach the collider");

            ((IBinder<int>)binder).SetValue(7);
            Assert.AreEqual(2, collider.direction, "An axis outside 0..2 was not clamped");
        }

        [Test]
        public void TheLayerMasks_TravelAsIntegers()
        {
            var collider = Spawn<BoxCollider>("BoxCollider");
            var include = collider.gameObject.AddComponent<ColliderIncludeLayersMonoBinder>();
            var exclude = collider.gameObject.AddComponent<ColliderExcludeLayersMonoBinder>();

            ((IBinder<int>)include).SetValue(1 << 8);
            ((IBinder<int>)exclude).SetValue(1 << 9);

            Assert.AreEqual(1 << 8, collider.includeLayers.value, "The include mask did not reach the collider");
            Assert.AreEqual(1 << 9, collider.excludeLayers.value, "The exclude mask did not reach the collider");
        }

        [Test]
        public void ContactOffset_ReachesTheCollider_AndIsNeverNegative()
        {
            var collider = Spawn<BoxCollider>("BoxCollider");
            var binder = collider.gameObject.AddComponent<ColliderContactOffsetMonoBinder>();

            ((IBinder<float>)binder).SetValue(0.05f);
            Assert.AreEqual(0.05f, collider.contactOffset, 0.001f, "The contact offset did not reach the collider");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NegativeInfinity);
            Assert.Greater(collider.contactOffset, 0f, "A non-finite offset reached the collider");
        }

        [Test]
        public void CookingOptions_ReachTheMeshCollider()
        {
            var collider = Spawn<MeshCollider>("MeshCollider");
            var binder = collider.gameObject.AddComponent<MeshColliderCookingOptionsMonoBinder>();

            ((IBinder<MeshColliderCookingOptions>)binder).SetValue(MeshColliderCookingOptions.None);

            Assert.AreEqual(MeshColliderCookingOptions.None, collider.cookingOptions, "The cooking options did not reach the collider");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var capsule = Spawn<CapsuleCollider>("CapsuleCollider");
            var box = Spawn<BoxCollider>("BoxCollider");
            var mesh = Spawn<MeshCollider>("MeshCollider");

            Assert.IsTrue(new CapsuleColliderHeightBinder(capsule).CanBind);
            Assert.IsTrue(new CapsuleColliderDirectionBinder(capsule).CanBind);
            Assert.IsTrue(new ColliderContactOffsetBinder(box).CanBind);
            Assert.IsTrue(new ColliderIncludeLayersBinder(box).CanBind);
            Assert.IsTrue(new ColliderExcludeLayersBinder(box).CanBind);
            Assert.IsTrue(new MeshColliderCookingOptionsBinder(mesh).CanBind);
        }
    }
}
