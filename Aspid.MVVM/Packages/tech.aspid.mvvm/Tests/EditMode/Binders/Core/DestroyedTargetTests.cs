using UnityEngine.UI;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

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

            Assert.IsFalse(binder.CanBind, "The binder agreed to bind to a destroyed button");
        }

        [Test]
        public void TargetBinder_WithALiveTarget_StillBinds()
        {
            var button = Spawn("Button").AddComponent<Button>();
            var binder = new ButtonCommandBinder(button);

            Assert.IsTrue(binder.CanBind, "The binder refused to bind to a live button");
        }

        /// <summary>
        /// The guard now lives in <c>TargetBinder</c> itself, so a binder that never carried its own
        /// <c>CanBind</c> override — most of them — is covered as well.
        /// </summary>
        [Test]
        public void ATargetBinderWithoutItsOwnOverride_IsAlsoGuarded()
        {
            var slider = Spawn("Slider").AddComponent<Slider>();
            var binder = new SliderValueBinder(slider);

            Destroy(slider);

            Assert.IsFalse(binder.CanBind, "An unguarded binder family still accepts a destroyed target");
        }

        [Test]
        public void BindSafely_WithADestroyedBinderInTheArray_SkipsItAndBindsTheRest()
        {
            var doomed = Spawn("Doomed").AddComponent<TextMonoBinder>();

            var survivorObject = Spawn("Survivor");
            survivorObject.AddComponent<TMPro.TextMeshProUGUI>();
            var survivor = survivorObject.AddComponent<TextMonoBinder>();

            var binders = new MonoBinder[] { doomed, survivor };
            Destroy(doomed);

            var member = new OneWayBindableMember<string>("Bound");

            LogAssert.Expect(LogType.Error, new Regex(@"Binder at index 0 '_binders' can't be null"));
            binders.BindSafely(member, owner: null, memberName: "_binders");

            Assert.IsTrue(survivor.IsBound, "The live binder was not bound because of its destroyed neighbour");
        }
    }
}
