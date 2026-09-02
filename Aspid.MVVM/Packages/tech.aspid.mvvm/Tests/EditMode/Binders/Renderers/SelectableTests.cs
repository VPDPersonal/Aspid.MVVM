using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for the <see cref="Selectable"/> transition, target graphic and hand-over binders.
    /// </summary>
    [TestFixture]
    public sealed class SelectableTests : SceneFixture
    {
        [Test]
        public void TheSelectableOptions_ReachTheControl()
        {
            var button = Spawn<Button>("Button");
            var graphic = Spawn<Image>("Graphic");

            ((IBinder<Selectable.Transition>)button.gameObject.AddComponent<SelectableTransitionMonoBinder>()).SetValue(Selectable.Transition.None);
            ((IBinder<Graphic>)button.gameObject.AddComponent<SelectableTargetGraphicMonoBinder>()).SetValue(graphic);

            Assert.AreEqual(Selectable.Transition.None, button.transition, "The transition did not reach the control");
            Assert.AreSame(graphic, button.targetGraphic, "The target graphic did not reach the control");
        }

        [Test]
        public void ADestroyedTargetGraphic_ArrivesAsNull()
        {
            var button = Spawn<Button>("Button");
            var graphic = Spawn<Image>("Graphic");
            var binder = button.gameObject.AddComponent<SelectableTargetGraphicMonoBinder>();

            ((IBinder<Graphic>)binder).SetValue(graphic);
            Destroy(graphic);
            ((IBinder<Graphic>)binder).SetValue(graphic);

            Assert.IsFalse(button.targetGraphic, "The destroyed graphic stayed alive for Unity");
        }

        [Test]
        public void TheSerializableTwins_AcceptTheirTargets()
        {
            var button = Spawn<Button>("Button");

            Assert.IsTrue(new SelectableTransitionBinder(button).CanBind);
            Assert.IsTrue(new SelectableTargetGraphicBinder(button).CanBind);
        }

        [Test]
        public void SelectableToSourceMonoBinder_HandsOverTheControl()
        {
            var button = Spawn<Button>("Button");
            var binder = button.gameObject.AddComponent<SelectableToSourceMonoBinder>();

            Selectable received = null;
            binder.Bind(new OneWayToSourceBindableMember<Selectable>(value => received = value));

            Assert.AreSame(button, received, "The ViewModel did not receive the Selectable");
        }
    }
}
