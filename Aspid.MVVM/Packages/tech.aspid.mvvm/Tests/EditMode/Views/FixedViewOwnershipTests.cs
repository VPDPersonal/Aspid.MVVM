using System;
using NUnit.Framework;
using Unity.Profiling;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class FixedViewOwnershipTests
    {
        [Test]
        public void ScalarCleanupCallsAnExplicitInterfaceImplementation()
        {
            var binder = new ExplicitUnbindBinder();
            var view = new ExplicitUnbindView { Value = binder };
            view.Initialize(new FixedBindingModel());
            Assert.That(binder.IsBound, Is.True);

            view.Deinitialize();

            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.UnbindCalls, Is.EqualTo(1));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void DirectCleanupLeavesTheBinderWithItsNewOwner(bool collection)
        {
            var binder = new CallbackTestBinder();
            IView view = collection
                ? new FixedArrayBindingView { Value = new IBinder[] { binder } }
                : new FixedScalarBindingView { Value = binder };
            var model = new FixedBindingModel();
            view.Initialize(model);
            binder.Unbind(new UnbindContext(view));
            binder.Bind(new BindContext(model.Member, this));
            view.Deinitialize();
            Assert.That(binder.IsBound, Is.True);
            binder.Unbind(new UnbindContext(this));
        }

        [Test]
        public void SkippedBinderDoesNotReleaseALaterExternalBinding()
        {
            var binder = new OptionalBinder();
            var view = new FixedScalarBindingView { Value = binder };
            var model = new FixedBindingModel();
            view.Initialize(model);
            Assert.That(binder.IsBound, Is.False);
            binder.Allow = true;
            binder.Bind(new BindContext(model.Member, this));
            view.Deinitialize();
            Assert.That(binder.IsBound, Is.True);
            binder.Unbind(new UnbindContext(this));
        }

        [TestCase(BindMode.OneTime, false)]
        [TestCase(BindMode.OneTime, true)]
        [TestCase(BindMode.OneWay, false)]
        [TestCase(BindMode.OneWay, true)]
        public void GeneratedLifecycleAddsNoAllocationsToDirectBinding(BindMode mode, bool collection)
        {
            IBinderAdder member = mode is BindMode.OneTime
                ? new OneTimeStructBindableMember<int>(1)
                : new OneWayStructBindableMember<int>(1);
            var binder = new CallbackTestBinder();
            IView view = collection
                ? new FixedArrayBindingView { Value = new IBinder[] { binder } }
                : new FixedScalarBindingView { Value = binder };
            var model = new FixedBindingModel(member);
            Assert.That(Measure(-1, 1), Is.GreaterThan(0), "The profiler must detect the control allocation.");
            Measure(0, 32);
            Measure(1, 32);
            var direct = Measure(0, 1000);
            var generated = Measure(1, 1000);
            TestContext.WriteLine($"Mode={mode}, collection={collection}, direct={direct}, generated={generated}");
            Assert.That(generated, Is.EqualTo(direct));

            int Measure(int route, int count)
            {
                using var recorder = ProfilerRecorder.StartNew(
                    ProfilerCategory.Memory, "GC.Alloc", 8192,
                    ProfilerRecorderOptions.StartImmediately | ProfilerRecorderOptions.CollectOnlyOnCurrentThread);
                for (var i = 0; i < count; i++)
                {
                    if (route < 0)
                    {
                        GC.KeepAlive(new byte[4096]);
                    }
                    else if (route == 0)
                    {
                        binder.Bind(new BindContext(member, view));
                        binder.Unbind(new UnbindContext(view));
                    }
                    else
                    {
                        view.Initialize(model);
                        view.Deinitialize();
                    }
                }

                recorder.Stop();
                Assert.That(recorder.Valid, Is.True);
                Assert.That(recorder.Count, Is.LessThan(recorder.Capacity));
                return recorder.Count;
            }
        }

        private sealed class OptionalBinder : Binder, IBinder<int>
        {
            public bool Allow;
            public override bool CanBind =>
                Allow;
            public void SetValue(int value) { }
        }
    }

    [View(AutoBinderFields = false)]
    internal partial class FixedScalarBindingView
    {
        public IBinder Value;
        public IBinder Nested;
    }

    [View(AutoBinderFields = false)]
    internal partial class ExplicitUnbindView
    {
        public ExplicitUnbindBinder Value;
    }

    internal sealed class ExplicitUnbindBinder : IBinder
    {
        private object _owner;

        public bool IsBound { get; private set; }
        public int UnbindCalls { get; private set; }

        public void Bind(in BindContext context)
        {
            _owner = context.Owner;
            IsBound = true;
        }

        void IBinder.Unbind(in UnbindContext context)
        {
            if (!ReferenceEquals(_owner, context.Owner)) return;
            UnbindCalls++;
            IsBound = false;
            _owner = null;
        }
    }

    [View(AutoBinderFields = false)]
    internal partial class FixedArrayBindingView
    {
        public IBinder[] Value;
        public IBinder Nested;
    }

    internal sealed class FixedBindingModel : IViewModel
    {
        public readonly IBinderAdder Member;

        public FixedBindingModel(IBinderAdder member = null)
        {
            Member = member ?? new OneWayStructBindableMember<int>(1);
        }

        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters) =>
            new(Member);
    }
}
