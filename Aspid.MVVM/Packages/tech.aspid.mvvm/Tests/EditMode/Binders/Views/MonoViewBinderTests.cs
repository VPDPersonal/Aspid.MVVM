using System;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests that <see cref="MonoViewBinder"/> and <see cref="MonoViewBinder{TView}"/> pass their constructor
    /// arguments straight through to <see cref="ViewTargetBinder{TView}"/>.
    /// </summary>
    [TestFixture]
    public sealed class MonoViewBinderTests : SceneFixture
    {
        [Test]
        public void Constructor_PassesTheModeThrough()
        {
            var binder = new MonoViewBinder<StubView>(Spawn<StubView>(), BindMode.OneTime);

            Assert.AreEqual(BindMode.OneTime, binder.Mode);
        }

        [Test]
        public void Constructor_DefaultsToOneWay()
        {
            var binder = new MonoViewBinder<StubView>(Spawn<StubView>());

            Assert.AreEqual(BindMode.OneWay, binder.Mode);
        }

        [Test]
        public void NonGenericConstructor_PassesTheModeThrough()
        {
            var binder = new MonoViewBinder(Spawn<MonoView>(), BindMode.OneTime);

            Assert.AreEqual(BindMode.OneTime, binder.Mode);
        }

        [Test]
        public void NonGenericConstructor_WithNullTarget_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new MonoViewBinder(null));
        }
    }
}
