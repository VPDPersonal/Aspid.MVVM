using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the new <see cref="Rigidbody"/> and <see cref="Rigidbody2D"/> binders.
    /// </summary>
    /// <remarks>
    /// Physics had no binders at all, while every collider was covered. The interesting part is not the plumbing
    /// but the difference between the two bodies: <see cref="Rigidbody"/> stores a non-finite mass in silence and
    /// then leaves the simulation, while <see cref="Rigidbody2D"/> refuses it and logs an error. Both binders drop
    /// the write, so the pair behaves alike; both halves of that are pinned here.
    /// </remarks>
    [TestFixture]
    public sealed class PhysicsBinderTests
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

        /// <summary>
        /// Unity clamps the mass range itself but stores <see cref="float.NaN"/> verbatim, and a body with a NaN
        /// mass silently leaves the simulation — so the binder drops the write instead.
        /// </summary>
        [Test]
        public void RigidbodyMassBinder_DropsANonFiniteMassAndKeepsTheLastGoodOne()
        {
            var gameObject = NewGameObject();
            var body = gameObject.AddComponent<Rigidbody>();
            var binder = gameObject.AddComponent<RigidbodyMassMonoBinder>();

            ((IBinder<float>)binder).SetValue(5f);
            Assert.AreEqual(5f, body.mass, 0.001f, "Обычная масса не доехала");

            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.AreEqual(5f, body.mass, 0.001f, "NaN затёр рабочую массу");

            ((IBinder<float>)binder).SetValue(float.PositiveInfinity);
            Assert.AreEqual(5f, body.mass, 0.001f, "Бесконечность затёрла рабочую массу");
        }

        /// <summary>
        /// The 2D body refuses a non-finite mass on its own, but logs an error naming the object while doing it.
        /// The binder drops the write first, so the pair behaves alike and the console stays quiet — an
        /// unexpected error log is enough to fail this test on its own.
        /// </summary>
        [Test]
        public void Rigidbody2DMassBinder_DropsANonFiniteMassWithoutUnityComplaining()
        {
            var gameObject = NewGameObject();
            var body = gameObject.AddComponent<Rigidbody2D>();
            var binder = gameObject.AddComponent<Rigidbody2DMassMonoBinder>();

            ((IBinder<float>)binder).SetValue(5f);
            Assert.AreEqual(5f, body.mass, 0.001f, "Обычная масса не доехала");

            ((IBinder<float>)binder).SetValue(float.NaN);
            Assert.AreEqual(5f, body.mass, 0.001f, "NaN затёр рабочую массу");
        }

        [Test]
        public void RigidbodyFlagBinders_ReachTheBody()
        {
            var gameObject = NewGameObject();
            var body = gameObject.AddComponent<Rigidbody>();

            var gravity = gameObject.AddComponent<RigidbodyUseGravityMonoBinder>();
            var kinematic = gameObject.AddComponent<RigidbodyIsKinematicMonoBinder>();

            ((IBinder<bool>)gravity).SetValue(false);
            ((IBinder<bool>)kinematic).SetValue(true);

            Assert.IsFalse(body.useGravity, "Гравитация не отключена");
            Assert.IsTrue(body.isKinematic, "Тело не переведено в кинематическое");
        }

        /// <summary>
        /// A negative gravity scale inverts the pull rather than being invalid, so the binder must not clamp it.
        /// </summary>
        [Test]
        public void GravityScaleBinder_KeepsANegativeValue()
        {
            var gameObject = NewGameObject();
            var body = gameObject.AddComponent<Rigidbody2D>();
            var binder = gameObject.AddComponent<Rigidbody2DGravityScaleMonoBinder>();

            ((IBinder<float>)binder).SetValue(-3f);

            Assert.AreEqual(-3f, body.gravityScale, 0.001f, "Отрицательный масштаб гравитации был обрезан");
        }

        [Test]
        public void SimulatedBinder_TakesTheBodyOutOfTheSimulation()
        {
            var gameObject = NewGameObject();
            var body = gameObject.AddComponent<Rigidbody2D>();
            var binder = gameObject.AddComponent<Rigidbody2DSimulatedMonoBinder>();

            ((IBinder<bool>)binder).SetValue(false);

            Assert.IsFalse(body.simulated, "Тело осталось в симуляции");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var body = NewGameObject().AddComponent<Rigidbody>();
            var body2d = NewGameObject().AddComponent<Rigidbody2D>();

            Assert.IsTrue(new RigidbodyMassBinder(body).IsBind);
            Assert.IsTrue(new RigidbodyUseGravityBinder(body).IsBind);
            Assert.IsTrue(new RigidbodyIsKinematicBinder(body).IsBind);
            Assert.IsTrue(new Rigidbody2DMassBinder(body2d).IsBind);
            Assert.IsTrue(new Rigidbody2DGravityScaleBinder(body2d).IsBind);
            Assert.IsTrue(new Rigidbody2DSimulatedBinder(body2d).IsBind);
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("Physics");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
