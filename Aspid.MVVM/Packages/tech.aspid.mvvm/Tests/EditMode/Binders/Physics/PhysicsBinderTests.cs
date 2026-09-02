using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="Rigidbody"/> and <see cref="Rigidbody2D"/> binders.
    /// </summary>
    /// <remarks>
    /// <see cref="Rigidbody"/> stores a non-finite mass in silence and then leaves the simulation, while
    /// <see cref="Rigidbody2D"/> refuses it and logs an error. Both binders drop the write, so the pair behaves
    /// alike; both halves of that are pinned here.
    /// </remarks>
    [TestFixture]
    public sealed class PhysicsBinderTests : SceneFixture
    {
        /// <summary>
        /// Unity clamps the mass range itself but stores <see cref="float.NaN"/> verbatim, and a body with a NaN
        /// mass silently leaves the simulation — so the binder drops the write instead.
        /// </summary>
        [Test]
        public void RigidbodyMassBinder_DropsANonFiniteMassAndKeepsTheLastGoodOne()
        {
            var body = Spawn<Rigidbody>("Physics");
            var binder = body.gameObject.AddComponent<RigidbodyMassMonoBinder>();

            ((IBinder<float>)binder).SetValue(5f);
            Assert.AreEqual(5f, body.mass, 0.001f, "The ordinary mass did not reach the body");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.AreEqual(5f, body.mass, 0.001f, "NaN overwrote the working mass");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.PositiveInfinity);
            Assert.AreEqual(5f, body.mass, 0.001f, "Infinity overwrote the working mass");
        }

        /// <summary>
        /// The 2D body refuses a non-finite mass on its own, but logs an error naming the object while doing it.
        /// The binder drops the write first, so the pair behaves alike and the console stays quiet — an
        /// unexpected error log is enough to fail this test on its own.
        /// </summary>
        [Test]
        public void Rigidbody2DMassBinder_DropsANonFiniteMassWithoutUnityComplaining()
        {
            var body = Spawn<Rigidbody2D>("Physics");
            var binder = body.gameObject.AddComponent<Rigidbody2DMassMonoBinder>();

            ((IBinder<float>)binder).SetValue(5f);
            Assert.AreEqual(5f, body.mass, 0.001f, "The ordinary mass did not reach the body");

            LogAssert.Expect(LogType.Error, new Regex("is not finite"));
            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.AreEqual(5f, body.mass, 0.001f, "NaN overwrote the working mass");
        }

        [Test]
        public void RigidbodyFlagBinders_ReachTheBody()
        {
            var body = Spawn<Rigidbody>("Physics");

            var gravity = body.gameObject.AddComponent<RigidbodyUseGravityMonoBinder>();
            var kinematic = body.gameObject.AddComponent<RigidbodyIsKinematicMonoBinder>();

            ((IBinder<bool>)gravity).SetValue(false);
            ((IBinder<bool>)kinematic).SetValue(true);

            Assert.IsFalse(body.useGravity, "Gravity was not disabled");
            Assert.IsTrue(body.isKinematic, "The body was not switched to kinematic");
        }

        [Test]
        public void RigidbodyConstraints_ReachTheBody()
        {
            var body = Spawn<Rigidbody>("Physics");
            var binder = body.gameObject.AddComponent<RigidbodyConstraintsMonoBinder>();

            ((IBinder<RigidbodyConstraints>)binder).SetValue(
                RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation);

            Assert.AreEqual(RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation,
                body.constraints, "The constraint mask did not reach the body");
        }

        [Test]
        public void RigidbodyConstraints_OneWayToSource_ReportsTheCurrentMask()
        {
            var body = Spawn<Rigidbody>("Physics");
            body.constraints = RigidbodyConstraints.FreezePositionZ;

            var binder = new RigidbodyConstraintsBinder(body, mode: BindMode.OneWayToSource);
            var received = default(RigidbodyConstraints);

            binder.Bind(new OneWayToSourceStructBindableMember<RigidbodyConstraints>(value => received = value));

            Assert.AreEqual(RigidbodyConstraints.FreezePositionZ, received, "The ViewModel did not receive the current mask");
        }

        /// <summary>
        /// A negative gravity scale inverts the pull rather than being invalid, so the binder must not clamp it.
        /// </summary>
        [Test]
        public void GravityScaleBinder_KeepsANegativeValue()
        {
            var body = Spawn<Rigidbody2D>("Physics");
            var binder = body.gameObject.AddComponent<Rigidbody2DGravityScaleMonoBinder>();

            ((IBinder<float>)binder).SetValue(-3f);

            Assert.AreEqual(-3f, body.gravityScale, 0.001f, "A negative gravity scale was clamped");
        }

        [Test]
        public void SimulatedBinder_TakesTheBodyOutOfTheSimulation()
        {
            var body = Spawn<Rigidbody2D>("Physics");
            var binder = body.gameObject.AddComponent<Rigidbody2DSimulatedMonoBinder>();

            ((IBinder<bool>)binder).SetValue(false);

            Assert.IsFalse(body.simulated, "The body stayed in the simulation");
        }

        [Test]
        public void Rigidbody2DBodyType_ReachesTheBody()
        {
            var body = Spawn<Rigidbody2D>("Physics");
            var binder = body.gameObject.AddComponent<Rigidbody2DBodyTypeMonoBinder>();

            ((IBinder<RigidbodyType2D>)binder).SetValue(RigidbodyType2D.Static);

            Assert.AreEqual(RigidbodyType2D.Static, body.bodyType, "The body type did not reach the body");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var body = Spawn<Rigidbody>("Physics");
            var body2d = Spawn<Rigidbody2D>("Physics2D");

            Assert.IsTrue(new RigidbodyMassBinder(body).CanBind);
            Assert.IsTrue(new RigidbodyUseGravityBinder(body).CanBind);
            Assert.IsTrue(new RigidbodyIsKinematicBinder(body).CanBind);
            Assert.IsTrue(new RigidbodyConstraintsBinder(body).CanBind);
            Assert.IsTrue(new Rigidbody2DMassBinder(body2d).CanBind);
            Assert.IsTrue(new Rigidbody2DGravityScaleBinder(body2d).CanBind);
            Assert.IsTrue(new Rigidbody2DSimulatedBinder(body2d).CanBind);
            Assert.IsTrue(new Rigidbody2DBodyTypeBinder(body2d).CanBind);
        }
    }
}
