#if UNITY_EDITOR
using TMPro;
using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for the input field binders re-subscribing from <c>OnValidate</c> while unbound.
    /// </summary>
    /// <remarks>
    /// These run in play mode because the method's first act is to return unless <c>Application.isPlaying</c>.
    /// </remarks>
    [TestFixture]
    public sealed class OnValidateSubscriptionTests : SceneFixture
    {
        [UnityTest]
        public IEnumerator OnValidate_WhileUnbound_LeavesTheFieldUntouched()
        {
            var (binder, field) = Create();
            yield return null;

            InvokeOnValidate(binder);

            Assert.AreEqual(0, RuntimeListeners(field.onValueChanged),
                "An unbound binder subscribed to the field, with nothing left to unhook it");
        }

        [UnityTest]
        public IEnumerator OnValidate_WhileBound_KeepsExactlyOneSubscription()
        {
            var (binder, field) = Create();
            yield return null;

            ((IBinder)binder).Bind(new TwoWayBindableMember<string>(string.Empty, _ => { }));
            Assert.AreEqual(1, RuntimeListeners(field.onValueChanged), "The bind did not subscribe to the field");

            InvokeOnValidate(binder);
            InvokeOnValidate(binder);

            Assert.AreEqual(1, RuntimeListeners(field.onValueChanged),
                "A repeated OnValidate multiplied the subscriptions");
        }

        /// <summary>
        /// <see cref="UnityEvent"/> reports no listener count of its own, so the number comes from the runtime call
        /// list behind it. A rename upstream turns this into a loud failure rather than a silent pass.
        /// </summary>
        private static int RuntimeListeners(UnityEventBase unityEvent)
        {
            const BindingFlags Flags = BindingFlags.NonPublic | BindingFlags.Instance;

            var calls = typeof(UnityEventBase).GetField("m_Calls", Flags)?.GetValue(unityEvent);
            Assert.IsNotNull(calls, "UnityEvent changed its internal layout — this test no longer verifies anything");

            var runtime = calls.GetType().GetField("m_RuntimeCalls", Flags)?.GetValue(calls);
            Assert.IsTrue(runtime is ICollection, "UnityEvent's listener list changed type");

            return ((ICollection)runtime).Count;
        }

        private static void InvokeOnValidate(InputFieldMonoBinder binder)
        {
            var method = typeof(InputFieldMonoBinder)
                .GetMethod("OnValidate", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsNotNull(method, "OnValidate was renamed — this test no longer verifies anything");
            method.Invoke(binder, null);
        }

        private (InputFieldMonoBinder binder, TMP_InputField field) Create()
        {
            var gameObject = Spawn("InputField");
            gameObject.SetActive(false);

            var field = gameObject.AddComponent<TMP_InputField>();
            var binder = gameObject.AddComponent<InputFieldMonoBinder>();

            var serializedObject = new UnityEditor.SerializedObject(binder);
            serializedObject.FindProperty("_mode").enumValueIndex = (int)BindMode.TwoWay;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            gameObject.SetActive(true);

            return (binder, field);
        }
    }
}
#endif
