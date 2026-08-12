using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for a <c>*MonoBinder</c> placed on a GameObject that has no component for it to drive.
    /// </summary>
    /// <remarks>
    /// <c>MonoBinder.IsBind</c> returned <see langword="true"/> unconditionally and no component binder overrode it,
    /// so binding succeeded and the first value threw a <see cref="System.NullReferenceException"/> from inside a
    /// leaf class's property setter — naming neither the binder nor the GameObject. The serializable side has had
    /// the equivalent guard on <c>TargetBinder</c> all along.
    /// </remarks>
    [TestFixture]
    public sealed class MissingComponentTests
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
        public void WithoutItsComponent_TheBinderRefusesToBind()
        {
            var binder = NewGameObject().AddComponent<AudioSourceVolumeMonoBinder>();

            Assert.IsFalse(binder.IsBind, "Биндер согласился привязаться без своего компонента");
        }

        [Test]
        public void WithoutItsComponent_BindingDeliversNothingInsteadOfThrowing()
        {
            var binder = NewGameObject().AddComponent<AudioSourceVolumeMonoBinder>();
            var member = new OneWayBindableMember<float>(0.5f);

            Assert.DoesNotThrow(() => ((IBinder)binder).Bind(member),
                "Привязка без компонента всё ещё падает");
        }

        [Test]
        public void WithItsComponent_TheBinderStillBinds()
        {
            var gameObject = NewGameObject();
            var audioSource = gameObject.AddComponent<AudioSource>();
            var binder = gameObject.AddComponent<AudioSourceVolumeMonoBinder>();

            Assert.IsTrue(binder.IsBind, "Биндер отказался привязаться при наличии компонента");

            ((IBinder<float>)binder).SetValue(0.25f);
            Assert.AreEqual(0.25f, audioSource.volume, 0.001f, "Значение не доехало до компонента");
        }

        private GameObject NewGameObject()
        {
            var gameObject = new GameObject("MissingComponent");
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
