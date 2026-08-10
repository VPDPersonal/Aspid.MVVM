#if UNITY_EDITOR
using TMPro;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Regression tests for the input field binders re-subscribing from <c>OnValidate</c> while unbound.
    /// </summary>
    /// <remarks>
    /// <c>OnValidate</c> exists to re-wire the field subscriptions after the bind mode is changed in the inspector
    /// during play mode. It did so without checking whether the binder was bound at all — and <c>Unbind</c> returns
    /// immediately when it is not, so <c>OnUnbound</c>, and with it the unsubscribe, never ran: the listener stayed
    /// on the field with nothing left to unhook it.
    /// <para/>
    /// These run in play mode because the method's first act is to return unless <c>Application.isPlaying</c>. An
    /// EditMode version would pass against the broken tree without observing anything.
    /// </remarks>
    [TestFixture]
    public sealed class OnValidateSubscriptionTests
    {
        private readonly List<GameObject> _spawned = new();

        [TearDown]
        public void TearDown()
        {
            foreach (var gameObject in _spawned)
            {
                if (gameObject) Object.Destroy(gameObject);
            }

            _spawned.Clear();
        }

        [UnityTest]
        public IEnumerator OnValidate_WhileUnbound_LeavesTheFieldUntouched()
        {
            var (binder, field) = Create();
            yield return null;

            InvokeOnValidate(binder);

            Assert.AreEqual(0, RuntimeListeners(field.onValueChanged),
                "Незабинденный биндер подписался на поле, и отписать его уже некому");
        }

        [UnityTest]
        public IEnumerator OnValidate_WhileBound_KeepsExactlyOneSubscription()
        {
            var (binder, field) = Create();
            yield return null;

            ((IBinder)binder).Bind(new TwoWayBindableMember<string>(string.Empty, _ => { }));
            Assert.AreEqual(1, RuntimeListeners(field.onValueChanged), "Привязка не подписалась на поле");

            InvokeOnValidate(binder);
            InvokeOnValidate(binder);

            Assert.AreEqual(1, RuntimeListeners(field.onValueChanged),
                "Повторный OnValidate размножил подписки");
        }

        /// <summary>
        /// <see cref="UnityEvent"/> reports no listener count of its own, so the number comes from the runtime call
        /// list behind it. A rename upstream turns this into a loud failure rather than a silent pass.
        /// </summary>
        private static int RuntimeListeners(UnityEventBase unityEvent)
        {
            const BindingFlags Flags = BindingFlags.NonPublic | BindingFlags.Instance;

            var calls = typeof(UnityEventBase).GetField("m_Calls", Flags)?.GetValue(unityEvent);
            Assert.IsNotNull(calls, "UnityEvent сменил внутреннее устройство — тест больше ничего не проверяет");

            var runtime = calls.GetType().GetField("m_RuntimeCalls", Flags)?.GetValue(calls);
            Assert.IsTrue(runtime is ICollection, "Список слушателей UnityEvent сменил тип");

            return ((ICollection)runtime).Count;
        }

        private static void InvokeOnValidate(InputFieldMonoBinder binder)
        {
            var method = typeof(InputFieldMonoBinder)
                .GetMethod("OnValidate", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsNotNull(method, "OnValidate переименован — тест больше ничего не проверяет");
            method.Invoke(binder, null);
        }

        private (InputFieldMonoBinder binder, TMP_InputField field) Create()
        {
            var gameObject = new GameObject("InputField");
            gameObject.SetActive(false);
            _spawned.Add(gameObject);

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
