using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for binders whose target, or whose own component, has been destroyed.
    /// </summary>
    /// <remarks>
    /// A destroyed <see cref="Object"/> is not <see langword="null"/> to C#: the managed wrapper outlives the
    /// native object, so <c>is not null</c> accepts a reference that throws on first use. Every runtime null check
    /// in the binders was written that way, while the editor layer already used Unity's own conversion — the
    /// understanding was there, it just had not reached the runtime.
    /// <para/>
    /// Two consequences are covered here: a <c>TargetBinder</c> whose serialized target was destroyed used to
    /// report <c>IsBind</c> as <see langword="true"/> and then throw from <c>OnBound</c>; and a destroyed
    /// <c>MonoBinder</c> sitting in a View's array was not recognised as an empty slot, so the loop called
    /// <c>Bind</c> on it.
    /// </remarks>
    [TestFixture]
    public sealed class DestroyedTargetTests
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
        public void TargetBinder_WithADestroyedTarget_RefusesToBind()
        {
            var button = NewGameObject("Button").AddComponent<Button>();
            var binder = new ButtonCommandBinder(button);

            Object.DestroyImmediate(button);

            Assert.IsFalse(binder.IsBind, "Биндер согласился привязаться к уничтоженной кнопке");
        }

        [Test]
        public void TargetBinder_WithALiveTarget_StillBinds()
        {
            var button = NewGameObject("Button").AddComponent<Button>();
            var binder = new ButtonCommandBinder(button);

            Assert.IsTrue(binder.IsBind, "Биндер отказался привязаться к живой кнопке");
        }

        /// <summary>
        /// The guard now lives in <c>TargetBinder</c> itself, so a binder that never carried its own
        /// <c>IsBind</c> override — most of them — is covered as well.
        /// </summary>
        [Test]
        public void ATargetBinderWithoutItsOwnOverride_IsAlsoGuarded()
        {
            var slider = NewGameObject("Slider").AddComponent<Slider>();
            var binder = new SliderValueBinder(slider);

            Object.DestroyImmediate(slider);

            Assert.IsFalse(binder.IsBind, "Незащищённое семейство биндеров всё ещё соглашается на уничтоженную цель");
        }

        [Test]
        public void BindSafely_WithADestroyedBinderInTheArray_SkipsItAndBindsTheRest()
        {
            var doomed = NewGameObject("Doomed").AddComponent<TextMonoBinder>();

            var survivorObject = NewGameObject("Survivor");
            survivorObject.AddComponent<TMPro.TextMeshProUGUI>();
            var survivor = survivorObject.AddComponent<TextMonoBinder>();

            var binders = new MonoBinder[] { doomed, survivor };
            Object.DestroyImmediate(doomed);

            var member = new OneWayBindableMember<string>("Привязано");

            // Пустой слот сообщается — раньше он им не считался и получал Bind().
            LogAssert.Expect(LogType.Error, new Regex(@"Binder at index 0 '_binders' can't be null"));
            binders.BindSafely(member, owner: null, memberName: "_binders");

            Assert.IsTrue(survivor.IsBound, "Живой биндер не был привязан из-за уничтоженного соседа");
        }

        private GameObject NewGameObject(string name)
        {
            var gameObject = new GameObject(name);
            _spawned.Add(gameObject);

            return gameObject;
        }
    }
}
