using System;
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
        [TestCase(true)]
        [TestCase(false)]
        public void DeinitializeIdentifiesEveryFailingField(bool multipleFailures)
        {
            var view = Spawn<MonoView>();
            var first = Spawn<FailingCleanupMonoBinder>();
            var second = Spawn<FailingCleanupMonoBinder>();
            var third = Spawn<FailingCleanupMonoBinder>();
            first.Failure = new Exception("first failure");
            if (multipleFailures)
            {
                second.Failure = new Exception("second failure");
                third.Failure = new Exception("third failure");
            }

            using var serialized = new SerializedObject(view);
            var groups = serialized.FindProperty("_bindersList");
            groups.arraySize = 2;
            SetGroup(groups.GetArrayElementAtIndex(0), "Value", null, first, second);
            SetGroup(groups.GetArrayElementAtIndex(1), "Other", third);
            serialized.ApplyModifiedPropertiesWithoutUndo();
            view.Initialize(new FixedBindingModel(new OneTimeStructBindableMember<int>(1)));
            Assert.That(third.IsBound, Is.True);

            if (multipleFailures)
            {
                var aggregate = Assert.Throws<AggregateException>(view.Deinitialize);
                Assert.That(aggregate.InnerExceptions.Count, Is.EqualTo(2));
                var valueFailure = aggregate.InnerExceptions[0];
                Assert.That(valueFailure.Message, Is.EqualTo("Failed to unbind field 'Value'."));
                Assert.That(valueFailure.InnerException, Is.TypeOf<AggregateException>());
                Assert.That(((AggregateException)valueFailure.InnerException).InnerExceptions,
                    Is.EqualTo(new[] { first.Failure, second.Failure }));
                Assert.That(aggregate.InnerExceptions[1].Message, Is.EqualTo("Failed to unbind field 'Other'."));
                Assert.That(aggregate.InnerExceptions[1].InnerException, Is.SameAs(third.Failure));
            }
            else
            {
                var failure = Assert.Throws<InvalidOperationException>(view.Deinitialize);
                Assert.That(failure.Message, Is.EqualTo("Failed to unbind field 'Value'."));
                Assert.That(failure.InnerException, Is.SameAs(first.Failure));
            }

            Assert.That(first.IsBound || second.IsBound || third.IsBound, Is.False);
            Assert.That(view.ViewModel, Is.Null);
        }

        private static void SetGroup(SerializedProperty group, string name, params MonoBinder[] binders)
        {
            group.FindPropertyRelative("_name").stringValue = name;
            var slots = group.FindPropertyRelative("_monoBinders");
            slots.arraySize = binders.Length;
            for (var i = 0; i < binders.Length; i++)
                slots.GetArrayElementAtIndex(i).objectReferenceValue = binders[i];
        }

        [Test]
        public void InitializeIdentifiesTheFailingField()
        {
            var view = Spawn<MonoView>();
            var binder = Spawn<FailingCleanupMonoBinder>();
            binder.BindingFailure = new Exception("binding failed");
            using var serialized = new SerializedObject(view);
            var groups = serialized.FindProperty("_bindersList");
            groups.arraySize = 1;
            SetGroup(groups.GetArrayElementAtIndex(0), "Health", binder);
            serialized.ApplyModifiedPropertiesWithoutUndo();

            var failure = Assert.Throws<InvalidOperationException>(() => view.Initialize(new FixedBindingModel()));

            Assert.That(failure.Message, Is.EqualTo("Failed to bind field 'Health'."));
            Assert.That(failure.InnerException, Is.SameAs(binder.BindingFailure));
            Assert.That(view.ViewModel, Is.Null);
        }

        [Test]
        public void DeinitializingSkipsABinderDestroyedBeforeActivation()
        {
            var view = Spawn<MonoView>();
            var inactive = Spawn();
            inactive.SetActive(false);
            var binder = inactive.AddComponent<ProbeMonoBinder>();
            using var serialized = new SerializedObject(view);
            var groups = serialized.FindProperty("_bindersList");
            groups.arraySize = 1;
            var group = groups.GetArrayElementAtIndex(0);
            group.FindPropertyRelative("_name").stringValue = "Value";
            var binders = group.FindPropertyRelative("_monoBinders");
            binders.arraySize = 1;
            binders.GetArrayElementAtIndex(0).objectReferenceValue = binder;
            serialized.ApplyModifiedPropertiesWithoutUndo();

            view.Initialize(new FixedBindingModel(new OneWayBindableMember<bool>(true)));
            Destroy(binder);
            Assert.That(binder.IsBound, Is.True);

            view.Deinitialize();

            Assert.That(binder.Calls, Does.Not.Contain("OnDetach"));
            Assert.That(view.ViewModel, Is.Null);
            binder.Unbind(new UnbindContext(view));
        }

        [Test]
        public void Bind_WithoutAView_ThrowsAndCanRetry()
        {
            var binder = Spawn<StubViewMonoBinder>();
            var model = new StubViewModel();
            var context = new BindContext(new OneWayBindableMember<IViewModel>(model), this);

            Assert.Throws<InvalidOperationException>(() => binder.Bind(context));
            Assert.That(binder.IsBound, Is.False);

            var view = binder.gameObject.AddComponent<StubView>();
            binder.Bind(context);

            Assert.That(view.ViewModel, Is.SameAs(model));
            binder.Unbind(new UnbindContext(this));
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
            public Exception BindingFailure;

            protected override void OnBinding()
            {
                if (BindingFailure is not null)
                    throw BindingFailure;
            }

            public void SetValue(int value) { }

            protected override void OnUnbinding()
            {
                if (Failure is not null)
                    throw Failure;
            }
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
