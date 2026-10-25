using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests that a non-Mono binder Unity rebuilt through deserialization still reports to the ViewModel.
    /// </summary>
    /// <remarks>
    /// Unity calls no constructor on a serializable class that has no parameterless one, so field initializers do not
    /// run on a binder loaded from a scene or prefab. The flag that suppresses the binder's own echo must therefore be
    /// idle at its default value: <see cref="BindMode.OneWayToSource"/> never writes, so nothing would reset a flag
    /// that came out of deserialization set to "drop".
    /// </remarks>
    [TestFixture]
    public sealed class DeserializedReverseBinderTests : SceneFixture
    {
        [Test]
        public void ToggleBinder_RebuiltByTheSerializer_ReportsToTheViewModelInOneWayToSource()
        {
            var original = Spawn<Toggle>("Toggle");
            var (binder, toggle) = Rebuild(original, new ToggleIsOnBinder(original, mode: BindMode.OneWayToSource));

            var initial = toggle.isOn;
            var received = new List<bool>();
            binder.Bind(new OneWayToSourceStructBindableMember<bool>(received.Add));

            Assert.AreEqual(new[] { initial }, received, "The initial value did not reach the ViewModel");

            toggle.isOn = !initial;

            Assert.AreEqual(new[] { initial, !initial }, received, "The user change did not reach the ViewModel");
        }

        [Test]
        public void SliderBinder_RebuiltByTheSerializer_ReportsToTheViewModelInOneWayToSource()
        {
            var original = Spawn<Slider>("Slider");
            var (binder, slider) = Rebuild(original, new SliderValueBinder(original, mode: BindMode.OneWayToSource));

            var received = new List<float>();
            binder.Bind(new OneWayToSourceStructBindableMember<float>(received.Add));

            Assert.AreEqual(new[] { 0f }, received, "The initial value did not reach the ViewModel");

            slider.value = 0.75f;

            Assert.AreEqual(new[] { 0f, 0.75f }, received, "The user change did not reach the ViewModel");
        }

        [Test]
        public void InputFieldBinder_RebuiltByTheSerializer_ReportsToTheViewModelInOneWayToSource()
        {
            var original = Spawn<TMP_InputField>("InputField");
            var (binder, field) = Rebuild(original, new InputFieldBinder(original, mode: BindMode.OneWayToSource));

            var initial = field.text;
            var received = new List<string>();
            binder.Bind(new OneWayToSourceBindableMember<string>(received.Add));

            Assert.AreEqual(new[] { initial }, received, "The initial value did not reach the ViewModel");

            // The event the field raises on a keystroke.
            field.onValueChanged.Invoke("typed");

            Assert.AreEqual(new[] { initial, "typed" }, received, "The user change did not reach the ViewModel");
        }

        /// <summary>
        /// Has Unity rebuild <paramref name="binder"/> the way a scene or prefab load does: Instantiate copies the
        /// GameObject through serialization, so the clone holds a binder the serializer made, aimed at the clone's
        /// own <typeparamref name="TTarget"/>.
        /// </summary>
        private (TBinder binder, TTarget target) Rebuild<TBinder, TTarget>(TTarget target, TBinder binder)
            where TBinder : Binder
            where TTarget : Component
        {
            target.gameObject.AddComponent<SerializedBinderHolder>().Binder = binder;

            var clone = Track(Object.Instantiate(target.gameObject));
            var rebuilt = clone.GetComponent<SerializedBinderHolder>().Binder as TBinder;

            Assert.IsNotNull(rebuilt, "The binder did not survive serialization");
            Assert.AreNotSame(binder, rebuilt, "Instantiate reused the binder instead of deserializing a new one");

            return (rebuilt, clone.GetComponent<TTarget>());
        }
    }
}
