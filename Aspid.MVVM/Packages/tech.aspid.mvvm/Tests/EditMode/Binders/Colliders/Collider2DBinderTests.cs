using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the 2D collider properties: trigger, offset, density, material and the per-shape sizes.
    /// </summary>
    [TestFixture]
    public sealed class Collider2DBinderTests : SceneFixture
    {
        [Test]
        public void IsTrigger_ReachesTheCollider()
        {
            var collider = Spawn<BoxCollider2D>("BoxCollider2D");
            var binder = collider.gameObject.AddComponent<Collider2DIsTriggerMonoBinder>();

            ((IBinder<bool>)binder).SetValue(true);

            Assert.IsTrue(collider.isTrigger, "The trigger did not reach the 2D collider");
        }

        /// <summary>
        /// A negative offset is ordinary — it is how a crouch moves a collider down — so only a non-finite value is
        /// refused.
        /// </summary>
        [Test]
        public void Offset_KeepsNegatives_AndRefusesNonFinite()
        {
            var collider = Spawn<BoxCollider2D>("BoxCollider2D");
            var binder = collider.gameObject.AddComponent<Collider2DOffsetMonoBinder>();

            ((IBinder<Vector2>)binder).SetValue(new Vector2(-1f, -2f));
            Assert.AreEqual(new Vector2(-1f, -2f), collider.offset, "The negative offset was not kept");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<Vector2>)binder).SetValue(new Vector2(float.NaN, 0f));
            Assert.AreEqual(new Vector2(-1f, -2f), collider.offset, "A non-finite offset reached the collider");
        }

        /// <summary>
        /// Unity ignores a density write unless the attached body computes its mass from its colliders, so the test
        /// has to give it one — which is also what the binder now documents.
        /// </summary>
        [Test]
        public void Density_ReachesTheCollider_AndIsNeverNegative()
        {
            var collider = Spawn<BoxCollider2D>("BoxCollider2D");
            var body = collider.gameObject.AddComponent<Rigidbody2D>();
            body.useAutoMass = true;
            var binder = collider.gameObject.AddComponent<Collider2DDensityMonoBinder>();

            ((IBinder<float>)binder).SetValue(2f);
            Assert.AreEqual(2f, collider.density, 0.001f, "The density did not reach the collider");

            ((IBinder<float>)binder).SetValue(-4f);
            Assert.AreEqual(0f, collider.density, 0.001f, "A negative density was not clamped");
        }

        [Test]
        public void TheShapes_TakeTheirSizes()
        {
            var box = Spawn<BoxCollider2D>("BoxCollider2D");
            var circle = Spawn<CircleCollider2D>("CircleCollider2D");
            var capsule = Spawn<CapsuleCollider2D>("CapsuleCollider2D");

            ((IBinder<Vector2>)box.gameObject.AddComponent<BoxCollider2DSizeMonoBinder>()).SetValue(new Vector2(2f, 3f));
            ((IBinder<float>)circle.gameObject.AddComponent<CircleCollider2DRadiusMonoBinder>()).SetValue(1.5f);
            ((IBinder<Vector2>)capsule.gameObject.AddComponent<CapsuleCollider2DSizeMonoBinder>()).SetValue(new Vector2(1f, 4f));

            Assert.AreEqual(new Vector2(2f, 3f), box.size, "The box size did not reach the collider");
            Assert.AreEqual(1.5f, circle.radius, 0.001f, "The circle radius did not reach the collider");
            Assert.AreEqual(new Vector2(1f, 4f), capsule.size, "The capsule size did not reach the collider");
        }

        /// <summary>
        /// The 2D material binder is built on <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/>, so a
        /// destroyed asset must arrive as <see langword="null"/> rather than as a live reference the Inspector shows
        /// as <c>Missing</c>.
        /// </summary>
        [Test]
        public void Material_ADestroyedAssetArrivesAsNull()
        {
            var collider = Spawn<BoxCollider2D>("BoxCollider2D");
            var binder = collider.gameObject.AddComponent<Collider2DMaterialMonoBinder>();
            var material = Track(new PhysicsMaterial2D("Ice"));

            ((IBinder<PhysicsMaterial2D>)binder).SetValue(material);
            Assert.AreSame(material, collider.sharedMaterial, "The live material did not reach the collider");

            Destroy(material);
            ((IBinder<PhysicsMaterial2D>)binder).SetValue(material);

            Assert.IsNull(collider.sharedMaterial, "A destroyed material was written as a live reference");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var box2D = Spawn<BoxCollider2D>("BoxCollider2D");
            var circle2D = Spawn<CircleCollider2D>("CircleCollider2D");
            var capsule2D = Spawn<CapsuleCollider2D>("CapsuleCollider2D");

            Assert.IsTrue(new Collider2DIsTriggerBinder(box2D).CanBind);
            Assert.IsTrue(new Collider2DOffsetBinder(box2D).CanBind);
            Assert.IsTrue(new Collider2DDensityBinder(box2D).CanBind);
            Assert.IsTrue(new Collider2DMaterialBinder(box2D).CanBind);
            Assert.IsTrue(new BoxCollider2DSizeBinder(box2D).CanBind);
            Assert.IsTrue(new CircleCollider2DRadiusBinder(circle2D).CanBind);
            Assert.IsTrue(new CapsuleCollider2DSizeBinder(capsule2D).CanBind);
        }
    }
}
