using System;
using UnityEngine;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests that <see cref="ScriptableViewBinder"/> and <see cref="ScriptableViewBinder{TView}"/> pass their
    /// constructor arguments straight through to <see cref="ViewTargetBinder{TView}"/>.
    /// </summary>
    [TestFixture]
    public sealed class ScriptableViewBinderTests : SceneFixture
    {
        [Test]
        public void Constructor_PassesTheModeThrough()
        {
            var view = Track(ScriptableObject.CreateInstance<StubScriptableView>());
            var binder = new ScriptableViewBinder<StubScriptableView>(view, BindMode.OneTime);

            Assert.AreEqual(BindMode.OneTime, binder.Mode);
        }

        [Test]
        public void Constructor_DefaultsToOneWay()
        {
            var view = Track(ScriptableObject.CreateInstance<StubScriptableView>());
            var binder = new ScriptableViewBinder<StubScriptableView>(view);

            Assert.AreEqual(BindMode.OneWay, binder.Mode);
        }

        [Test]
        public void NonGenericConstructor_PassesTheModeThrough()
        {
            var view = Track(ScriptableObject.CreateInstance<EmptyScriptableView>());
            var binder = new ScriptableViewBinder(view, BindMode.OneTime);

            Assert.AreEqual(BindMode.OneTime, binder.Mode);
        }

        [Test]
        public void NonGenericConstructor_WithNullTarget_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ScriptableViewBinder(null));
        }
    }
}
