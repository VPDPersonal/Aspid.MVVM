using System;
using System.Reflection;
using UnityEditor;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests <see cref="MonoViewMonoBinder{TView}"/> through <see cref="StubViewMonoBinder"/>: resolving the view
    /// on the same GameObject and initializing it on bind/unbind. Also smoke-tests the concrete
    /// <see cref="MonoViewMonoBinder"/> against a real <see cref="MonoView"/>.
    /// </summary>
    [TestFixture]
    public sealed class MonoViewMonoBinderTests : SceneFixture
    {
        [Test]
        public void RealMonoViewPreservesFailuresFromEveryBinderGroup()
        {
            var view = Spawn<MonoView>();
            var first = Spawn<FailingCleanupMonoBinder>();
            var second = Spawn<FailingCleanupMonoBinder>();
            first.Failure = new Exception("first group");
            second.Failure = new Exception("second group");
            var groupType = typeof(MonoView).GetNestedType("Binders", BindingFlags.NonPublic);
            var groups = Array.CreateInstance(groupType, 2);
            var binders = new[] { first, second };
            for (var i = 0; i < binders.Length; i++)
            {
                var group = Activator.CreateInstance(groupType, nonPublic: true);
                groupType.GetField("_name", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(group, "Value");
                groupType.GetField("_monoBinders", BindingFlags.Instance | BindingFlags.NonPublic)
                    .SetValue(group, new MonoBinder[] { binders[i] });
                groups.SetValue(group, i);
            }
            typeof(MonoView).GetField("_bindersList", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(view, groups);
            view.Initialize(new FixedBindingModel(new OneTimeStructBindableMember<int>(1)));

            var aggregate = Assert.Throws<AggregateException>(view.Deinitialize);

            Assert.That(aggregate.Flatten().InnerExceptions, Is.EquivalentTo(new[] { first.Failure, second.Failure }));
            Assert.That(first.IsBound || second.IsBound, Is.False);
            Assert.That(view.ViewModel, Is.Null);
        }

        [Test]
        public void CanBind_WithoutTheViewOnTheGameObject_IsFalse()
        {
            var binder = Spawn<StubViewMonoBinder>();

            Assert.IsFalse(binder.CanBind, "The binder agreed to bind without a view on its GameObject");
        }

        [Test]
        public void CanBind_WithTheViewOnTheGameObject_IsTrue()
        {
            var gameObject = Spawn();
            gameObject.AddComponent<StubView>();
            var binder = gameObject.AddComponent<StubViewMonoBinder>();

            Assert.IsTrue(binder.CanBind, "The binder refused to bind with a view present on the same GameObject");
        }

        [Test]
        public void SetValue_WithAViewModel_InitializesTheView()
        {
            var gameObject = Spawn();
            var view = gameObject.AddComponent<StubView>();
            var binder = gameObject.AddComponent<StubViewMonoBinder>();
            var viewModel = new StubViewModel();

            binder.SetValue(viewModel);

            Assert.AreSame(viewModel, view.ViewModel, "The view on the same GameObject was not initialized");
        }

        [Test]
        public void SetValue_WithNull_DeinitializesTheView()
        {
            var gameObject = Spawn();
            var view = gameObject.AddComponent<StubView>();
            var binder = gameObject.AddComponent<StubViewMonoBinder>();
            binder.SetValue(new StubViewModel());

            binder.SetValue(null);

            Assert.IsNull(view.ViewModel, "A null value did not deinitialize the view");
        }

        [Test]
        public void Unbind_DeinitializesTheView()
        {
            var gameObject = Spawn();
            var view = gameObject.AddComponent<StubView>();
            var binder = gameObject.AddComponent<StubViewMonoBinder>();
            SetMode(binder, BindMode.OneWay);

            binder.Bind(new BindContext(new OneWayBindableMember<IViewModel>(new StubViewModel()), binder));
            Assert.IsNotNull(view.ViewModel, "The view was not initialized on bind");

            binder.Unbind(new UnbindContext(binder));

            Assert.IsNull(view.ViewModel, "Unbind did not deinitialize the view");
        }

        [Test]
        public void NonGenericMonoViewMonoBinder_InitializesTheRealMonoViewOnTheSameGameObject()
        {
            var gameObject = Spawn();
            var view = gameObject.AddComponent<MonoView>();
            ClearBindersList(view);
            var binder = gameObject.AddComponent<MonoViewMonoBinder>();
            var viewModel = new StubViewModel();

            binder.SetValue(viewModel);

            Assert.AreSame(viewModel, view.ViewModel, "The MonoView on the same GameObject was not initialized");
        }

        private sealed class FailingCleanupMonoBinder : MonoBinder, IBinder<int>
        {
            public Exception Failure;

            public void SetValue(int value) { }

            protected override void OnUnbinding() =>
                throw Failure;
        }

        private static void SetMode(MonoBinder binder, BindMode mode)
        {
            var serializedObject = new SerializedObject(binder);

            serializedObject.FindProperty("_mode").enumValueIndex = (int)mode;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ClearBindersList(MonoView view)
        {
            var serializedObject = new SerializedObject(view);

            serializedObject.FindProperty("_bindersList").arraySize = 0;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
