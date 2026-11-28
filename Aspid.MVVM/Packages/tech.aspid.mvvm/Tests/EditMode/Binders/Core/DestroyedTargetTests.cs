using System;
using UnityEngine.UI;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for binders whose target, or whose own component, has been destroyed.
    /// </summary>
    /// <remarks>
    /// A destroyed <see cref="UnityEngine.Object"/> is not <see langword="null"/> to C#: the managed wrapper
    /// outlives the native object, so <c>is not null</c> accepts a reference that throws on first use.
    /// </remarks>
    [TestFixture]
    public sealed class DestroyedTargetTests : SceneFixture
    {
        [Test]
        public void TargetBinder_WithADestroyedTarget_RefusesToBind()
        {
            var button = Spawn("Button").AddComponent<Button>();
            var binder = new ButtonCommandBinder(button);

            Destroy(button);

            var context = new BindContext(new OneWayBindableMember<IRelayCommand>(null), binder);
            Assert.Throws<InvalidOperationException>(() => binder.Bind(in context));
            Assert.IsFalse(binder.IsBound);
        }

        [Test]
        public void TargetBinder_WithALiveTarget_StillBinds()
        {
            var button = Spawn("Button").AddComponent<Button>();
            var binder = new ButtonCommandBinder(button);

            binder.Bind(new BindContext(new OneWayBindableMember<IRelayCommand>(null), binder));
            Assert.IsTrue(binder.IsBound);
            binder.Unbind(new UnbindContext(binder));
        }

        [Test]
        public void SliderValueBinder_RejectsADestroyedSliderBeforeAttachment()
        {
            var slider = Spawn("Slider").AddComponent<Slider>();
            var binder = new SliderValueBinder(slider);

            Destroy(slider);

            var context = new BindContext(new OneWayStructBindableMember<float>(0.5f), binder);
            Assert.Throws<InvalidOperationException>(() => binder.Bind(in context));
            Assert.IsFalse(binder.IsBound);
        }

        [Test]
        public void SafeBinding_SkipsADestroyedSingleBinder()
        {
            IBinder binder = Spawn("Doomed").AddComponent<TextMonoBinder>();
            Destroy((TextMonoBinder)binder);
            var member = new OneWayBindableMember<string>("Bound");

            Assert.DoesNotThrow(() => binder.BindSafely(new BindContext(member, this)));
            Assert.DoesNotThrow(() => binder.UnbindSafely(new UnbindContext(this)));
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void SafeBinding_SilentlySkipsADestroyedBinderInTheArray()
        {
            var doomed = Spawn("Doomed").AddComponent<TextMonoBinder>();

            var survivorObject = Spawn("Survivor");
            survivorObject.AddComponent<TMPro.TextMeshProUGUI>();
            var survivor = survivorObject.AddComponent<TextMonoBinder>();

            var binders = new MonoBinder[] { doomed, survivor };
            Destroy(doomed);

            var member = new OneWayBindableMember<string>("Bound");

            binders.BindSafely(new BindContext(member, this));
            Assert.That(survivor.IsBound, Is.True);

            binders.UnbindSafely(new UnbindContext(this));
            Assert.That(survivor.IsBound, Is.False);
            LogAssert.NoUnexpectedReceived();
        }
    }
}
