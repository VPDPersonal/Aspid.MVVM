using System;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using Aspid.MVVM.Validation;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class UnityBinderRegressionTests : SceneFixture
    {
        [Test]
        public void MissingComponentCanBeResolvedAfterItIsAddedOrReplaced()
        {
            var binder = Spawn<ComponentProbe>();
            Assert.That(binder.CanBind, Is.False);
            var target = binder.gameObject.AddComponent<BoxCollider>();
            Assert.That(binder.CanBind, Is.True);
            Assert.That(binder.Component, Is.SameAs(target));
            Destroy(target);
            Assert.That(binder.CanBind, Is.False);
            target = binder.gameObject.AddComponent<BoxCollider>();
            Assert.That(binder.CanBind, Is.True);
            Assert.That(binder.Component, Is.SameAs(target));
        }

        [Test]
        public void FailedComponentResolutionCanBeRetried()
        {
            var binder = Spawn<ComponentProbe>();
            binder.Fail = true;
            Assert.Throws<InvalidOperationException>(() => _ = binder.CanBind);
            binder.Fail = false;
            var target = binder.gameObject.AddComponent<BoxCollider>();
            Assert.That(binder.Component, Is.SameAs(target));
        }

        [Test]
        public void CachedReadsDoNotRepeatAMissingComponentLookup()
        {
            var binder = Spawn<ComponentProbe>();
            var attempts = binder.ResolveAttempts;
            Assert.That(binder.Component, Is.Null);
            Assert.That(binder.Component, Is.Null);
            Assert.That(binder.ResolveAttempts, Is.EqualTo(attempts + 1));

            var target = binder.gameObject.AddComponent<BoxCollider>();
            Assert.That(binder.Component, Is.Null);
            Assert.That(binder.CanBind, Is.True);
            Assert.That(binder.Component, Is.SameAs(target));
            Assert.That(binder.Component, Is.SameAs(target));
            Assert.That(binder.ResolveAttempts, Is.EqualTo(attempts + 2));
        }

        [Test]
        public void BindingRetriesMissingAndDestroyedComponents()
        {
            var binder = Spawn<ComponentProbe>();
            var member = new OneWayStructBindableMember<int>(1);
            var context = new BindContext(member, this);
            binder.Bind(in context);
            Assert.That(binder.IsBound, Is.False);

            var target = binder.gameObject.AddComponent<BoxCollider>();
            binder.Bind(in context);
            Assert.That(binder.IsBound, Is.True);
            Assert.That(binder.LastComponent, Is.SameAs(target));
            var attempts = binder.ResolveAttempts;
            member.Value = 2;
            Assert.That(binder.ResolveAttempts, Is.EqualTo(attempts));

            binder.Unbind(new UnbindContext(this));
            Destroy(target);
            target = binder.gameObject.AddComponent<BoxCollider>();
            binder.Bind(in context);
            Assert.That(binder.IsBound, Is.True);
            Assert.That(binder.LastComponent, Is.SameAs(target));
            Assert.That(binder.ResolveAttempts, Is.EqualTo(attempts + 1));
            binder.Unbind(new UnbindContext(this));
        }

#if UNITY_EDITOR && !ASPID_MVVM_EDITOR_DISABLED
        [Test]
        public void HardResetMarksPreviousDataDirtyAfterSoftReset()
        {
            var binder = Spawn<ComponentProbe>();
            var validatable = (IMonoBinderValidatable)binder;
            Assert.That(validatable.Id, Is.Not.Null);
            validatable.SetId("Field");
            validatable.ResetId(MonoBinderResetMode.Soft);
            Assert.That(validatable.PreviousId.Id, Is.EqualTo("Field"));
            UnityEditor.EditorUtility.ClearDirty(binder);
            validatable.ResetId();
            Assert.That(validatable.PreviousId.Id, Is.Null);
            Assert.That(UnityEditor.EditorUtility.IsDirty(binder), Is.True);

            var view = Spawn<CallbackView>();
            validatable.SetView(view);
            validatable.ResetView(MonoBinderResetMode.Soft);
            Assert.That(validatable.PreviousView.View, Is.SameAs(view));
            UnityEditor.EditorUtility.ClearDirty(binder);
            validatable.ResetView();
            Assert.That(validatable.PreviousView.View, Is.Null);
            Assert.That(UnityEditor.EditorUtility.IsDirty(binder), Is.True);
        }
#endif

        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(3, false)]
        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        public void ViewUpdateDoesNotResumeAfterDetachmentOrRebinding(int kind, bool rebind)
        {
            var (binder, state) = CreateViewBinder(kind);
            var first = new StubViewModel();
            var next = new StubViewModel();
            var member = new OneWayBindableMember<IViewModel>(first);
            binder.Bind(new BindContext(member, this));
            Assert.That(state.Model, Is.SameAs(first));
            state.Deinitializing = () =>
            {
                state.Deinitializing = null;
                binder.Unbind(new UnbindContext(this));
                if (rebind)
                    binder.Bind(new BindContext(new OneWayBindableMember<IViewModel>(next), this));
            };

            member.Value = new StubViewModel();

            Assert.That(binder.IsBound, Is.EqualTo(rebind));
            Assert.That(state.Model, Is.SameAs(rebind ? next : null));
            if (rebind)
                binder.Unbind(new UnbindContext(this));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void NewerReentrantValueWins(int kind)
        {
            var (binder, state) = CreateViewBinder(kind);
            var member = new OneWayBindableMember<IViewModel>(new StubViewModel());
            binder.Bind(new BindContext(member, this));
            var next = new StubViewModel();
            state.Deinitializing = () =>
            {
                state.Deinitializing = null;
                member.Value = next;
            };
            member.Value = new StubViewModel();
            Assert.That(state.Model, Is.SameAs(next));
            member.Value = null;
            Assert.That(state.Model, Is.Null);
            binder.Unbind(new UnbindContext(this));
        }

        private (IBinder Binder, ViewState State) CreateViewBinder(int kind)
        {
            if (kind < 2)
            {
                var view = Spawn<CallbackView>();
                IBinder binder = kind == 0
                    ? new MonoViewBinder<CallbackView>(view)
                    : view.gameObject.AddComponent<CallbackMonoBinder>();
                return (binder, view.State);
            }

            var scriptable = Track(ScriptableObject.CreateInstance<CallbackScriptableView>());
            if (kind == 3) return (new ScriptableViewBinder<CallbackScriptableView>(scriptable), scriptable.State);
            var mono = Spawn<CallbackScriptableBinder>();
            typeof(ScriptableViewMonoBinder<CallbackScriptableView>)
                .GetField("_view", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(mono, scriptable);
            return (mono, scriptable.State);
        }

        private sealed class ComponentProbe : ComponentMonoBinder<BoxCollider>, IBinder<int>
        {
            public bool Fail;
            public int ResolveAttempts;
            public BoxCollider LastComponent;

            public BoxCollider Component => CachedComponent;

            public void SetValue(int value) =>
                LastComponent = CachedComponent;

            protected override BoxCollider ResolveComponent()
            {
                ++ResolveAttempts;
                if (Fail)
                    throw new InvalidOperationException("resolution");
                return base.ResolveComponent();
            }
        }

        private sealed class CallbackMonoBinder : MonoViewMonoBinder<CallbackView> { }
        private sealed class CallbackScriptableBinder : ScriptableViewMonoBinder<CallbackScriptableView> { }

        private sealed class ViewState
        {
            public IViewModel Model;
            public Action Deinitializing;

            public void Deinitialize()
            {
                Model = null;
                Deinitializing?.Invoke();
            }
        }

        private sealed class CallbackView : MonoBehaviour, IView
        {
            public readonly ViewState State = new();
            public IViewModel ViewModel => State.Model;
            public void Initialize(IViewModel viewModel) =>
                State.Model = viewModel;
            public void Deinitialize() =>
                State.Deinitialize();
        }

        private sealed class CallbackScriptableView : ScriptableObject, IView
        {
            public readonly ViewState State = new();
            public IViewModel ViewModel => State.Model;
            public void Initialize(IViewModel viewModel) =>
                State.Model = viewModel;
            public void Deinitialize() =>
                State.Deinitialize();
        }
    }
}
