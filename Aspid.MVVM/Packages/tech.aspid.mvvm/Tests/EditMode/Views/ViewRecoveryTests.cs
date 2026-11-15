using System;
using System.Reflection;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using UnityEngine;
using UnityEngine.TestTools;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class ViewRecoveryTests : SceneFixture
    {
        [TestCase("initializing")]
        [TestCase("initialized")]
        public void FailedGeneratedViewCanDeinitializeAndRetry(string stage)
        {
            var model = new RecoveryModel();
            var view = new RecoveryView();
            view.FailStage = stage;
            Assert.Throws<InvalidOperationException>(() => view.Initialize(model));
            Assert.That(view.ViewModel, Is.Null);
            Assert.That(view.First.IsBound, Is.False);
            Assert.That(view.Second.IsBound, Is.False);
            var calls = view.First.Calls;
            model.Member.Value = 2;
            Assert.That(view.First.Calls, Is.EqualTo(calls));
            view.Deinitialize();
            view.FailStage = null;
            view.Second.Fail = false;
            view.Initialize(model);
            calls = view.First.Calls;
            model.Member.Value = 3;
            Assert.That(view.First.Calls, Is.EqualTo(calls + 1));
            view.Deinitialize();
            view.Deinitialize();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InheritedTypedAndUntypedOverloadsRecover(bool typed)
        {
            var view = new TypedRecoveryView { FailTyped = true };
            var model = new RecoveryModel();
            Assert.Throws<InvalidOperationException>(() => Initialize());
            Assert.That(view.First.IsBound, Is.False);
            Assert.That(view.Third.IsBound, Is.False);
            Assert.That(view.ViewModel, Is.Null);
            view.Deinitialize();
            view.FailTyped = false;
            Initialize();
            Assert.That(view.First.IsBound, Is.True);
            Assert.That(view.Third.IsBound, Is.True);
            view.Deinitialize();

            void Initialize()
            {
                if (typed)
                    view.Initialize(model);
                else
                    ((IView)view).Initialize(model);
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InterfaceTypedOverloadsRecover(bool typed)
        {
            var view = new InterfaceRecoveryView { Fail = true };
            var model = new RecoveryModel();
            Assert.Throws<InvalidOperationException>(() => Initialize());
            Assert.That(view.First.IsBound, Is.False);
            Assert.That(view.ViewModel, Is.Null);
            view.Deinitialize();
            view.Fail = false;
            Initialize();
            Assert.That(view.First.IsBound, Is.True);
            view.Deinitialize();

            void Initialize()
            {
                if (typed)
                    view.Initialize((IRecoveryModel)model);
                else
                    ((IView)view).Initialize(model);
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InheritedTypedOverrideResetsEveryGuard(bool typed)
        {
            var view = new FurtherRecoveryView { FailTyped = true };
            var model = new RecoveryModel();
            Assert.Throws<InvalidOperationException>(() => Initialize());
            view.Deinitialize();
            Assert.That(view.First.IsBound || view.Third.IsBound || view.Fourth.IsBound, Is.False);
            view.FailTyped = false;
            Initialize();
            Assert.That(view.First.IsBound && view.Third.IsBound && view.Fourth.IsBound, Is.True);
            view.Deinitialize();

            void Initialize()
            {
                if (typed)
                    view.Initialize(model);
                else
                    ((IView)view).Initialize(model);
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public void DeinitializedViewsStillDisposeWithTheirOwner(bool manual)
        {
            var child = new RecoveryChildView();
            var initializer = CreateInitializer(manual, child);
            Initialize(initializer, new RecoveryModel());
            Deinitialize(initializer);
            typeof(ViewInitializerBase).GetMethod("OnDestroy", BindingFlags.Instance | BindingFlags.NonPublic)
                .Invoke(initializer, null);
            Destroy(initializer.gameObject);
            Assert.That(child.DisposeCalls, Is.EqualTo(1));
        }

        [Test]
        public void BinderFailureDoesNotPreventTheGeneratedViewFromInitializing()
        {
            var model = new RecoveryModel();
            var view = new RecoveryView();
            view.Second.Fail = true;
            ExpectBindingFailure();

            Assert.DoesNotThrow(() => view.Initialize(model));
            Assert.That(view.ViewModel, Is.SameAs(model));
            Assert.That(view.First.IsBound, Is.True);
            Assert.That(view.Second.IsBound, Is.False);
            var calls = view.First.Calls;
            model.Member.Value = 2;
            Assert.That(view.First.Calls, Is.EqualTo(calls + 1));
            view.Deinitialize();
            calls = view.First.Calls;
            model.Member.Value = 3;
            Assert.That(view.First.Calls, Is.EqualTo(calls));
            view.Second.Fail = false;
            view.Initialize(model);
            Assert.That(view.Second.IsBound, Is.True);
            view.Deinitialize();
        }

        [Test]
        public void IsolatedBindingFailuresPreserveAnotherOwnersBinder()
        {
            var model = new RecoveryModel();
            var external = new OneWayStructBindableMember<int>(9);
            var view = new RecoveryView();
            view.First.Bind(new BindContext(external, view.First));
            view.Second.Fail = true;
            ExpectBindingFailure();
            ExpectBindingFailure();
            Assert.DoesNotThrow(() => view.Initialize(model));
            Assert.That(view.First.IsBound, Is.True);
            view.Deinitialize();
            var calls = view.First.Calls;
            external.Value = 10;
            Assert.That(view.First.Calls, Is.EqualTo(calls + 1));
            view.First.Unbind(new UnbindContext(view.First));
        }

        [TestCase(false, BindMode.OneWay)]
        [TestCase(true, BindMode.OneWay)]
        [TestCase(false, BindMode.OneTime)]
        [TestCase(true, BindMode.OneTime)]
        public void FailedOnAttachDoesNotKeepOwnershipWhenTheBinderIsReused(bool collection, BindMode mode)
        {
            var failed = new RecoveryBinder(mode) { FailAttach = true };
            var healthy = new RecoveryBinder();
            var member = new OneWayStructBindableMember<int>(1);
            IView view = collection
                ? new FixedArrayBindingView { Value = new IBinder[] { failed, healthy } }
                : new FixedScalarBindingView { Value = failed, Nested = healthy };
            ExpectBindingFailure();
            view.Initialize(new FixedBindingModel(member));
            Assert.That(failed.IsBound, Is.False);
            failed.FailAttach = false;
            failed.Bind(new BindContext(new OneWayStructBindableMember<int>(2), failed));
            view.Deinitialize();
            Assert.That(healthy.IsBound, Is.False);
            Assert.That(failed.IsBound, Is.True);
            failed.Unbind(new UnbindContext(failed));
        }

        private static void ExpectBindingFailure()
        {
            LogAssert.Expect(LogType.Exception, new Regex(@"\[BindSafely\].*InvalidOperationException"));
        }

        [Test]
        public void DeinitializeFinishesAllCleanupAfterAHookThrows()
        {
            var view = new TypedRecoveryView();
            view.Initialize(new RecoveryModel());
            view.FailDeinitialize = true;
            Assert.Throws<InvalidOperationException>(() => view.Deinitialize());
            Assert.That(view.ViewModel, Is.Null);
            Assert.That(view.First.IsBound || view.Second.IsBound || view.Third.IsBound, Is.False);
            view.Deinitialize();
            view.FailDeinitialize = false;
            view.Initialize(new RecoveryModel());
            view.Deinitialize();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InitializerRollsBackEarlierAndFailingViewsAndCanRetry(bool manual)
        {
            var first = new RecoveryChildView();
            var second = new RecoveryChildView { FailInitialize = true, FailDeinitialize = true };
            var third = new RecoveryChildView();
            var initializer = CreateInitializer(manual, first, second, third);
            var model = new RecoveryModel();
            Assert.That(Assert.Throws<InvalidOperationException>(() => Initialize(initializer, model)).Message, Is.EqualTo("initialize"));
            Assert.That(initializer.IsInitialized, Is.False);
            Assert.That(first.ViewModel, Is.Null);
            Assert.That(second.ViewModel, Is.Null);
            Assert.That(third.InitializeCalls, Is.Zero);
            Deinitialize(initializer);
            second.FailInitialize = second.FailDeinitialize = false;
            Initialize(initializer, model);
            Assert.That(initializer.IsInitialized, Is.True);
            second.FailDeinitialize = true;
            Assert.Throws<InvalidOperationException>(() => Deinitialize(initializer));
            Assert.That(first.ViewModel ?? second.ViewModel ?? third.ViewModel, Is.Null);
            Assert.That(initializer.IsInitialized, Is.False);
            Deinitialize(initializer);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InitializerReportsEveryViewCleanupFailureAndCanRetry(bool manual)
        {
            var first = new RecoveryChildView { FailDeinitialize = true };
            var second = new RecoveryChildView { FailDeinitialize = true };
            var healthy = new RecoveryChildView();
            var initializer = CreateInitializer(manual, first, second, healthy);
            var model = new RecoveryModel();
            Initialize(initializer, model);

            var aggregate = Assert.Throws<AggregateException>(() => Deinitialize(initializer));

            Assert.That(aggregate.Flatten().InnerExceptions.Count, Is.EqualTo(2));
            Assert.That(first.ViewModel ?? second.ViewModel ?? healthy.ViewModel, Is.Null);
            Assert.That(initializer.IsInitialized, Is.False);
            first.FailDeinitialize = second.FailDeinitialize = false;
            Initialize(initializer, model);
            Assert.DoesNotThrow(() => Deinitialize(initializer));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InitializerDoesNotReleaseAPreexistingView(bool manual)
        {
            var model = new RecoveryModel();
            var first = new RecoveryChildView();
            var external = new RecoveryChildView();
            external.Initialize(model);
            var initializer = CreateInitializer(manual, first, external);
            Assert.Throws<InvalidOperationException>(() => Initialize(initializer, model));
            Assert.That(first.ViewModel, Is.Null);
            Deinitialize(initializer);
            Destroy(initializer.gameObject);
            Assert.That(external.ViewModel, Is.SameAs(model));
            external.Deinitialize();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void DestroyContinuesDisposingViewsAfterOneThrows(bool manual)
        {
            var first = new RecoveryChildView();
            var second = new RecoveryChildView { FailDeinitialize = true };
            var third = new RecoveryChildView();
            var initializer = CreateInitializer(manual, first, second, third);
            Initialize(initializer, new RecoveryModel());
            var destroy = typeof(ViewInitializerBase).GetMethod("OnDestroy", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(Assert.Throws<TargetInvocationException>(() => destroy.Invoke(initializer, null)).InnerException,
                Is.TypeOf<InvalidOperationException>());
            Assert.That(first.ViewModel ?? second.ViewModel ?? third.ViewModel, Is.Null);
            Assert.That(first.DisposeCalls + second.DisposeCalls + third.DisposeCalls, Is.EqualTo(3));
        }

        private ViewInitializerBase CreateInitializer(bool manual, params IView[] views)
        {
            var initializer = manual
                ? (ViewInitializerBase)Spawn().AddComponent<ViewInitializerManual>()
                : Spawn().AddComponent<ViewInitializer>();
            if (!manual)
            {
                SetField(initializer, "_initializeStage", Enum.ToObject(
                    typeof(ViewInitializer).GetNestedType("InitializeStage", BindingFlags.NonPublic), 0));
            }

            var slots = new ViewInitializeComponent[views.Length];

            for (var i = 0; i < views.Length; i++)
            {
                slots[i] = new ViewInitializeComponent();
                SetField(slots[i], "_resolveType", ResolveType.Reference);
                SetField(slots[i], "_reference", views[i]);
            }

            SetField(initializer, "_viewComponents", slots);
            return initializer;
        }

        private static void Initialize(ViewInitializerBase initializer, IViewModel model)
        {
            if (initializer is ViewInitializerManual manual)
                manual.Initialize(model);
            else
            {
                var slot = new ViewModelInitializeComponent();
                SetField(slot, "_resolveType", ResolveType.Reference);
                SetField(slot, "_reference", model);
                SetField(initializer, "_viewModelComponent", slot);
                ((ViewInitializer)initializer).Initialize();
            }
        }

        private static void Deinitialize(ViewInitializerBase initializer)
        {
            if (initializer is ViewInitializerManual manual)
                manual.Deinitialize();
            else
                ((ViewInitializer)initializer).Deinitialize();
        }

        private static void SetField(object target, string name, object value)
        {
            for (var type = target.GetType(); type is not null; type = type.BaseType)
            {
                var field = type.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                if (field is null)
                    continue;

                field.SetValue(target, value);
                return;
            }

            throw new MissingFieldException(name);
        }
    }

    internal interface IRecoveryModel : IViewModel { }

    [View(AutoBinderFields = false)]
    internal partial class InterfaceRecoveryView : IView<IRecoveryModel>
    {
        public RecoveryBinder First = new();
        [IgnoreBind] public bool Fail;

        partial void OnInitializedInternal(IViewModel viewModel)
        {
            if (Fail)
                throw new InvalidOperationException("interface");
        }
    }

    internal sealed class RecoveryModel : IRecoveryModel
    {
        public readonly OneWayStructBindableMember<int> Member = new(1);

        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters) =>
            new(Member);
    }

    internal sealed class RecoveryBinder : Binder, IBinder<int>
    {
        public bool Fail;
        public bool FailAttach;
        public int Calls;

        public RecoveryBinder(BindMode mode = BindMode.OneWay)
            : base(mode) { }

        public void SetValue(int value)
        {
            Calls++;
            if (Fail)
                throw new InvalidOperationException("binder");
        }

        protected override void OnAttach()
        {
            if (FailAttach)
                throw new InvalidOperationException("bound");
        }
    }

    [View(AutoBinderFields = false)]
    internal partial class RecoveryView
    {
        public RecoveryBinder First = new();
        public RecoveryBinder Second = new();
        [IgnoreBind] public string FailStage;
        [IgnoreBind] public bool FailDeinitialize;

        partial void OnInitializingInternal(IViewModel viewModel)
        {
            if (FailStage == "initializing")
                throw new InvalidOperationException("initializing");
        }

        partial void OnInitializedInternal(IViewModel viewModel)
        {
            if (FailStage == "initialized")
                throw new InvalidOperationException("initialized");
        }

        partial void OnDeinitializingInternal()
        {
            if (FailDeinitialize)
                throw new InvalidOperationException("deinitialize");
        }
    }

    [View(AutoBinderFields = false)]
    internal partial class TypedRecoveryView : RecoveryView, IView<RecoveryModel>
    {
        public RecoveryBinder Third = new();
        [IgnoreBind] public bool FailTyped;

        partial void OnInitializedInternal(IViewModel viewModel)
        {
            if (FailTyped)
                throw new InvalidOperationException("typed");
        }
    }

    [View(AutoBinderFields = false)]
    internal partial class FurtherRecoveryView : TypedRecoveryView
    {
        public RecoveryBinder Fourth = new();
    }

    internal sealed class RecoveryChildView : IView, IDisposable
    {
        public bool FailInitialize;
        public bool FailDeinitialize;
        public int InitializeCalls;
        public int DisposeCalls;

        public IViewModel ViewModel { get; private set; }

        public void Initialize(IViewModel model)
        {
            InitializeCalls++;
            ViewModel = model;
            if (FailInitialize)
                throw new InvalidOperationException("initialize");
        }

        public void Deinitialize()
        {
            ViewModel = null;
            if (FailDeinitialize)
                throw new InvalidOperationException("deinitialize");
        }

        public void Dispose()
        {
            DisposeCalls++;
            Deinitialize();
        }
    }
}
