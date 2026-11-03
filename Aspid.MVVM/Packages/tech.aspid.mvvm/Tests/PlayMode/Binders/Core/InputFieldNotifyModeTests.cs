using TMPro;
using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests the notification modes of input field binders while TMP honors silent writes in Play Mode.
    /// </summary>
    [TestFixture]
    public sealed class InputFieldNotifyModeTests : SceneFixture
    {
        [TestCase(false, NotifyMode.Always, 1, 2)]
        [TestCase(false, NotifyMode.AfterBind, 0, 1)]
        [TestCase(false, NotifyMode.Never, 0, 0)]
        [TestCase(true, NotifyMode.Always, 1, 2)]
        [TestCase(true, NotifyMode.AfterBind, 0, 1)]
        [TestCase(true, NotifyMode.Never, 0, 0)]
        public void InputFieldBinder_RaisesOnValueChangedPerMode(
            bool serializable,
            NotifyMode notifyMode,
            int onBind,
            int afterBind)
        {
            Assert.IsTrue(Application.isPlaying, "TMP notification modes must be checked in Play Mode");

            var field = Spawn<TMP_InputField>("InputField");
            IBinder binder;

            if (serializable)
            {
                binder = new InputFieldBinder(field, notifyMode);
            }
            else
            {
                var monoBinder = field.gameObject.AddComponent<InputFieldMonoBinder>();
                typeof(InputFieldMonoBinder)
                    .GetField("_notifyMode", BindingFlags.Instance | BindingFlags.NonPublic)
                    .SetValue(monoBinder, notifyMode);
                typeof(MonoBinder)
                    .GetField("_mode", BindingFlags.Instance | BindingFlags.NonPublic)
                    .SetValue(monoBinder, BindMode.TwoWay);
                binder = monoBinder;
            }

            var events = 0;
            var received = new List<string>();
            field.onValueChanged.AddListener(_ => events++);

            try
            {
                var member = new TwoWayBindableMember<string>("first", received.Add);
                binder.Bind(member);

                Assert.AreEqual("first", field.text);
                Assert.AreEqual(onBind, events, "Wrong number of events on bind");

                member.Value = "second";

                Assert.AreEqual("second", field.text);
                Assert.AreEqual(afterBind, events, "Wrong number of events after bind");
                Assert.IsEmpty(received, "The binder's own write came back as user input");

                field.text = "typed";

                Assert.AreEqual(afterBind + 1, events, "User input did not raise onValueChanged");
                Assert.AreEqual(new[] { "typed" }, received, "User input did not reach the ViewModel");
            }
            finally
            {
                binder.Unbind();
            }
        }
    }
}
