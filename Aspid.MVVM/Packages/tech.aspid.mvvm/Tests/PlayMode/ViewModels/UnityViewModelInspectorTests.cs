#if UNITY_EDITOR
using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class UnityViewModelInspectorTests : SceneFixture
    {
        [UnityTest]
        public IEnumerator MonoInspectorChangesUpdateTheInspectorBoundViewInPlayMode()
        {
            var model = Spawn<InspectorMonoProbe>();
            yield return CheckInspectorChanges(model, model);
        }

        [UnityTest]
        public IEnumerator ScriptableInspectorChangesUpdateTheInspectorBoundViewInPlayMode()
        {
            var model = Track(ScriptableObject.CreateInstance<InspectorScriptableProbe>());
            yield return CheckInspectorChanges(model, model);
        }

        private IEnumerator CheckInspectorChanges(Object target, IViewModel model)
        {
            Assert.That(Application.isPlaying, Is.True);
            var view = Spawn<InspectorBoundView>();
            var binder = view.gameObject.AddComponent<NameBinder>();
            view.Configure(binder);
            view.Initialize(model);
            Assert.That(binder.IsBound, Is.True);
            Assert.That(view.gameObject.name, Is.EqualTo("Value7"));

            yield return null;
            yield return SetInspectorValueAndWait(target, view, 42);
            yield return SetInspectorValueAndWait(target, view, -13);

            view.Deinitialize();
            SetInspectorValue(target, 99);
            var deadline = Time.realtimeSinceStartup + 0.25f;
            while (Time.realtimeSinceStartup < deadline)
                yield return null;

            Assert.That(view.gameObject.name, Is.EqualTo("Value-13"));
            Assert.That(binder.IsBound, Is.False);
        }

        private static IEnumerator SetInspectorValueAndWait(Object target, InspectorBoundView view, int value)
        {
            SetInspectorValue(target, value);
            var expected = "Value" + value;
            var deadline = Time.realtimeSinceStartup + 5f;
            while (view.gameObject.name != expected && Time.realtimeSinceStartup < deadline)
                yield return null;

            Assert.That(view.gameObject.name, Is.EqualTo(expected),
                "Inspector changes must reach the bound View without manually notifying or rebinding.");
        }

        private static void SetInspectorValue(Object target, int value)
        {
            using var serialized = new SerializedObject(target);
            serialized.Update();
            var field = serialized.FindProperty("_value");
            Assert.That(field, Is.Not.Null);
            field.intValue = value;
            Assert.That(serialized.ApplyModifiedPropertiesWithoutUndo(), Is.True);
        }

        private sealed class NameBinder : MonoBinder, IBinder<int>
        {
            public void SetValue(int value) =>
                gameObject.name = "Value" + value;
        }
    }

    [ViewModel]
    internal sealed partial class InspectorMonoProbe : MonoViewModel
    {
        [Tooltip("The value shown by the bound View.")]
        [Bind, SerializeField] private int _value = 7;
    }

    [ViewModel]
    internal sealed partial class InspectorScriptableProbe : ScriptableViewModel
    {
        [Tooltip("The value shown by the bound View.")]
        [Bind, SerializeField] private int _value = 7;
    }

    [View]
    internal sealed partial class InspectorBoundView : MonoView
    {
        [Tooltip("The binders displaying the value.")]
        [SerializeField] private MonoBinder[] _value;

        public void Configure(MonoBinder binder) =>
            _value = new[] { binder };
    }
}
#endif
