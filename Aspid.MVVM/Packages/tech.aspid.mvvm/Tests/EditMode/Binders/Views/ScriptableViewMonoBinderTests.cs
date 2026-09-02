using UnityEditor;
using UnityEngine;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests <see cref="ScriptableViewMonoBinder{TView}"/> through <see cref="StubScriptableViewMonoBinder"/>:
    /// the empty-view <c>CanBind</c> guard and view initialization on bind/unbind.
    /// </summary>
    [TestFixture]
    public sealed class ScriptableViewMonoBinderTests : SceneFixture
    {
        [Test]
        public void CanBind_WithNoViewAssigned_IsFalse()
        {
            var binder = Spawn<StubScriptableViewMonoBinder>();

            Assert.IsFalse(binder.CanBind, "The binder agreed to bind without an assigned view");
        }

        [Test]
        public void CanBind_WithAViewAssigned_IsTrue()
        {
            var binder = AssignView(Spawn<StubScriptableViewMonoBinder>(), Track(ScriptableObject.CreateInstance<StubScriptableView>()));

            Assert.IsTrue(binder.CanBind, "The binder refused to bind with a view assigned");
        }

        [Test]
        public void SetValue_WithAViewModel_InitializesTheAssignedView()
        {
            var view = Track(ScriptableObject.CreateInstance<StubScriptableView>());
            var binder = AssignView(Spawn<StubScriptableViewMonoBinder>(), view);
            var viewModel = new StubViewModel();

            binder.SetValue(viewModel);

            Assert.AreSame(viewModel, view.ViewModel, "The assigned view was not initialized with the ViewModel");
        }

        [Test]
        public void SetValue_WithNull_DeinitializesTheAssignedView()
        {
            var view = Track(ScriptableObject.CreateInstance<StubScriptableView>());
            var binder = AssignView(Spawn<StubScriptableViewMonoBinder>(), view);
            binder.SetValue(new StubViewModel());

            binder.SetValue(null);

            Assert.IsNull(view.ViewModel, "A null value did not deinitialize the assigned view");
        }

        [Test]
        public void Unbind_DeinitializesTheAssignedView()
        {
            var view = Track(ScriptableObject.CreateInstance<StubScriptableView>());
            var binder = AssignView(Spawn<StubScriptableViewMonoBinder>(), view);
            SetMode(binder, BindMode.OneWay);

            binder.Bind(new OneWayBindableMember<IViewModel>(new StubViewModel()));
            Assert.IsNotNull(view.ViewModel, "The assigned view was not initialized on bind");

            binder.Unbind();

            Assert.IsNull(view.ViewModel, "Unbind did not deinitialize the assigned view");
        }

        private static StubScriptableViewMonoBinder AssignView(StubScriptableViewMonoBinder binder, StubScriptableView view)
        {
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_view").objectReferenceValue = view;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            return binder;
        }

        private static void SetMode(MonoBinder binder, BindMode mode)
        {
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)mode;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
