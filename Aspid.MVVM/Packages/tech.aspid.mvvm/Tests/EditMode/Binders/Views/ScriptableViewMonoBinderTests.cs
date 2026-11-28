using System;
using UnityEditor;
using UnityEngine;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests <see cref="ScriptableViewMonoBinder{TView}"/> through <see cref="StubScriptableViewMonoBinder"/>:
    /// required view validation and view initialization on bind/unbind.
    /// </summary>
    [TestFixture]
    public sealed class ScriptableViewMonoBinderTests : SceneFixture
    {
        [TestCase(false)]
        [TestCase(true)]
        public void Bind_WithoutALiveView_ThrowsAndCanRetry(bool destroyAssignedView)
        {
            var binder = Spawn<StubScriptableViewMonoBinder>();
            SetMode(binder, BindMode.OneWay);
            if (destroyAssignedView)
            {
                var assignedView = ScriptableObject.CreateInstance<StubScriptableView>();
                AssignView(binder, assignedView);
                Destroy(assignedView);
            }

            var model = new StubViewModel();
            var context = new BindContext(new OneWayBindableMember<IViewModel>(model), this);

            Assert.Throws<InvalidOperationException>(() => binder.Bind(context));
            Assert.That(binder.IsBound, Is.False);

            var view = Track(ScriptableObject.CreateInstance<StubScriptableView>());
            AssignView(binder, view);
            binder.Bind(context);

            Assert.That(view.ViewModel, Is.SameAs(model));
            binder.Unbind(new UnbindContext(this));
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

            binder.Bind(new BindContext(new OneWayBindableMember<IViewModel>(new StubViewModel()), binder));
            Assert.IsNotNull(view.ViewModel, "The assigned view was not initialized on bind");

            binder.Unbind(new UnbindContext(binder));

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
