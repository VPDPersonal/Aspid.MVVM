using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class ViewLifecycleContractTests
    {
        [TestCase(false)]
        [TestCase(true)]
        public void FailedOuterBindingPreservesDeclaredNestedBindings(bool collection)
        {
            var member = new OneWayStructBindableMember<int>(1);
            var nested = new CallbackTestBinder();
            var outer = new CallbackTestBinder();
            IView view = collection
                ? new FixedArrayBindingView { Value = new IBinder[] { outer }, Nested = nested }
                : new FixedScalarBindingView { Value = outer, Nested = nested };
            outer.Attaching = () =>
            {
                nested.Bind(new BindContext(member, view));
                throw new InvalidOperationException("outer");
            };
            ExpectBindingFailure();
            view.Initialize(new DisposableLookupModel());
            Assert.That(outer.IsBound, Is.False);
            Assert.That(nested.IsBound, Is.True);
            outer.Attaching = null;
            outer.Bind(new BindContext(member, outer));
            view.Deinitialize();
            Assert.That(nested.IsBound, Is.False);
            Assert.That(outer.IsBound, Is.True);
            outer.Unbind(new UnbindContext(outer));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void CompletionFailureRemainsOwnedUntilTheViewDeinitializes(bool collection)
        {
            var binder = new CallbackTestBinder { Bound = () => throw new InvalidOperationException("notification") };
            IView view = collection
                ? new FixedArrayBindingView { Value = new IBinder[] { binder } }
                : new FixedScalarBindingView { Value = binder };
            ExpectBindingFailure();
            view.Initialize(new FixedBindingModel());
            Assert.That(binder.IsBound, Is.True);
            view.Deinitialize();
            Assert.That(binder.IsBound, Is.False);
            Assert.That(binder.UnboundCalls, Is.EqualTo(1));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void CleanupReportsEveryExceptionAndAllowsReinitialization(bool collection)
        {
            var failure = new InvalidOperationException("first");
            var first = new CallbackTestBinder { Unbound = () => throw failure };
            var secondFailure = new Exception("second");
            var second = new CallbackTestBinder { Unbound = () => throw secondFailure };
            IView view = collection
                ? new FixedArrayBindingView { Value = new IBinder[] { first, second } }
                : new FixedScalarBindingView { Value = first, Nested = second };
            view.Initialize(new FixedBindingModel());
            var aggregate = Assert.Throws<AggregateException>(view.Deinitialize);
            Assert.That(aggregate.Flatten().InnerExceptions, Is.EquivalentTo(new[] { failure, secondFailure }));
            Assert.That(first.IsBound || second.IsBound, Is.False);
            Assert.That(view.ViewModel, Is.Null);
            Assert.DoesNotThrow(view.Deinitialize);
            first.Unbound = second.Unbound = null;
            view.Initialize(new FixedBindingModel());
            view.Deinitialize();
            Assert.That(first.UnboundCalls, Is.EqualTo(2));
            Assert.That(second.UnboundCalls, Is.EqualTo(2));
        }

        [Test]
        public void ViewHooksBaseViewsAndBindersRetainAllFailures()
        {
            var view = new TypedCallbackTestView();
            var starting = new Exception("start");
            var binding = new Exception("binder");
            var finishing = new Exception("finish");
            view.Initialize(new DisposableLookupModel());
            view.Deinitializing = () => throw starting;
            view.Value.Unbinding = () => throw binding;
            view.Deinitialized = () => throw finishing;

            var aggregate = Assert.Throws<AggregateException>(view.Deinitialize);

            Assert.That(aggregate.Flatten().InnerExceptions, Is.EquivalentTo(new[] { starting, binding, finishing }));
            Assert.That(view.Value.IsBound, Is.False);
            Assert.That(view.ViewModel, Is.Null);
            Assert.DoesNotThrow(view.Deinitialize);
        }

        [Test]
        public void InitializationFailureKeepsPriorityOverMultipleCleanupFailures()
        {
            var failure = new Exception("initialize");
            var view = new CallbackTestView
            {
                Initializing = () => throw failure,
                Deinitializing = () => throw new Exception("cleanup start"),
                Deinitialized = () => throw new Exception("cleanup end")
            };

            Assert.That(Assert.Catch(() => view.Initialize(new DisposableLookupModel())), Is.SameAs(failure));
            Assert.That(view.ViewModel, Is.Null);
            Assert.That(view.Value.IsBound, Is.False);
        }

        [Test]
        public void OneViewFailureKeepsItsInstance()
        {
            var failure = new Exception("single");
            var view = new CallbackTestView { Deinitialized = () => throw failure };
            view.Initialize(new DisposableLookupModel());
            Assert.That(Assert.Catch(view.Deinitialize), Is.SameAs(failure));
            Assert.That(view.ViewModel, Is.Null);
        }

        [Test]
        public void GeneratedViewsIgnoreRepeatedDeinitializationFromHooksAndBinders()
        {
            var view = new CallbackTestView();
            view.Deinitializing = view.Deinitialize;
            view.Deinitialized = view.Deinitialize;
            view.Value.Unbinding = view.Deinitialize;
            view.Value.Unbound = view.Deinitialize;
            var model = new DisposableLookupModel();
            view.Initialize(model);
            view.Deinitialize();
            Assert.That(view.ViewModel, Is.Null);
            Assert.That(view.Value.IsBound, Is.False);
            Assert.That(view.DeinitializingCalls, Is.EqualTo(1));
            view.Initialize(model);
            view.Deinitialize();
            Assert.That(view.DeinitializingCalls, Is.EqualTo(2));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void FailedInitializationAlsoIgnoresRepeatedCleanup(bool typed)
        {
            var view = new TypedCallbackTestView();
            var failure = new InvalidOperationException("initialization");
            var completedCallbacks = 0;
            view.TypedInitializing = () => throw failure;
            view.Deinitializing = () => { view.Deinitialize(); completedCallbacks++; };
            view.Deinitialized = () => { view.Deinitialize(); completedCallbacks++; };
            var model = new DisposableLookupModel();
            var thrown = typed
                ? Assert.Catch(() => view.Initialize(model))
                : Assert.Catch(() => ((IView)view).Initialize(model));
            Assert.That(thrown, Is.SameAs(failure));
            Assert.That(completedCallbacks, Is.EqualTo(2));
            Assert.That(view.ViewModel, Is.Null);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void GeneratedViewsRejectCleanupDuringInitializationAndAllowRetry(bool typed)
        {
            var view = new TypedCallbackTestView();
            var model = new DisposableLookupModel();
            view.TypedInitializing = view.Deinitialize;
            Assert.Throws<InvalidOperationException>(() => Initialize());
            Assert.That(view.ViewModel, Is.Null);
            Assert.That(view.Value.IsBound, Is.False);
            view.TypedInitializing = null;
            Initialize();
            Assert.That(view.Value.IsBound, Is.True);
            view.Deinitialize();

            void Initialize()
            {
                if (typed)
                    view.Initialize(model);
                else
                    ((IView)view).Initialize(model);
            }
        }

        [Test]
        public void ReinitializeFailureLeavesTheGeneratedViewDetachedWithoutDisposingModels()
        {
            var view = new CallbackTestView();
            var oldModel = new DisposableLookupModel();
            var newModel = new DisposableLookupModel();
            view.Initialize(oldModel);
            view.Initializing = () => throw new InvalidOperationException("initialize");
            Assert.Throws<InvalidOperationException>(() => view.Reinitialize(newModel));
            Assert.That(view.ViewModel, Is.Null);
            Assert.That(view.Value.IsBound, Is.False);
            Assert.That(oldModel.DisposeCalls + newModel.DisposeCalls, Is.Zero);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void DisposalReturnsTheModelAndLeavesItsLifetimeWithTheCaller(bool typed)
        {
            var view = new DisposableCallbackView();
            var model = new DisposableLookupModel();
            view.Initialize(model);
            var previous = typed ? view.DisposeView() : ((IView)view).DisposeView();
            Assert.That(previous, Is.SameAs(model));
            Assert.That(view.ViewModel, Is.Null);
            Assert.That(view.DisposeCalls, Is.EqualTo(1));
            Assert.That(model.DisposeCalls, Is.Zero);
        }

        [Test]
        public void NullViewsAreRejectedByEveryLifecycleHelper()
        {
            IView missing = null;
            DisposableCallbackView missingDisposable = null;
            Assert.That(Assert.Throws<ArgumentNullException>(() => missing.DeinitializeView()).ParamName, Is.EqualTo("view"));
            Assert.That(Assert.Throws<ArgumentNullException>(() => missing.Reinitialize(null)).ParamName, Is.EqualTo("view"));
            Assert.That(Assert.Throws<ArgumentNullException>(() => missing.DisposeView()).ParamName, Is.EqualTo("view"));
            Assert.That(Assert.Throws<ArgumentNullException>(() => missingDisposable.DisposeView()).ParamName, Is.EqualTo("view"));
        }

        [Test]
        public void OptionalViewsCanBeSkippedExplicitly()
        {
            IView missing = null;
            DisposableCallbackView missingDisposable = null;
            Assert.That(missing?.DeinitializeView(), Is.Null);
            Assert.That(missing?.Reinitialize(new DisposableLookupModel()), Is.Null);
            Assert.That(missing?.DisposeView(), Is.Null);
            Assert.That(missingDisposable?.DisposeView(), Is.Null);
            Assert.That(missing.DestroyView(), Is.Null);
            Assert.That(missing.DestroyViewAndGameObject(), Is.Null);
        }

        [Test]
        public void SafeLifecycleHelpersSkipNullViews()
        {
            IView missing = null;
            DisposableCallbackView missingDisposable = null;

            Assert.That(missing.ReinitializeSafely(new DisposableLookupModel()), Is.Null);
            Assert.That(missing.DeinitializeViewSafely(), Is.Null);
            Assert.That(missing.DisposeViewSafely(), Is.Null);
            Assert.That(missingDisposable.DisposeViewSafely(), Is.Null);
        }

        [Test]
        public void ReinitializeSafelyLogsInitializationFailure()
        {
            var view = new CallbackTestView();
            view.Initialize(new DisposableLookupModel());
            view.Initializing = () => throw new InvalidOperationException("safe initialization");

            LogAssert.Expect(LogType.Exception, "InvalidOperationException: safe initialization");
            Assert.That(view.ReinitializeSafely(new DisposableLookupModel()), Is.Null);
            Assert.That(view.ViewModel, Is.Null);
        }

        [Test]
        public void SafeDeinitializationLogsFailureAndStopsReinitialization()
        {
            var model = new DisposableLookupModel();
            var view = new CallbackTestView();
            view.Deinitialized = () => throw new InvalidOperationException("safe deinitialization");

            view.Initialize(model);
            LogAssert.Expect(LogType.Exception, "InvalidOperationException: safe deinitialization");
            Assert.That(view.ReinitializeSafely(new DisposableLookupModel()), Is.Null);
            Assert.That(view.ViewModel, Is.Null);

            view.Initialize(model);
            LogAssert.Expect(LogType.Exception, "InvalidOperationException: safe deinitialization");
            Assert.That(view.DeinitializeViewSafely(), Is.Null);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void DisposeViewSafelyLogsFailureAndInvokesDisposeOnce(bool typed)
        {
            var view = new DisposableCallbackView();
            view.Initialize(new DisposableLookupModel());
            view.Deinitialized = () => throw new InvalidOperationException("safe disposal");

            LogAssert.Expect(LogType.Exception, "InvalidOperationException: safe disposal");
            Assert.That(typed ? view.DisposeViewSafely() : ((IView)view).DisposeViewSafely(), Is.Null);
            Assert.That(view.DisposeCalls, Is.EqualTo(1));
        }

        [Test]
        public void SafeLifecycleHelpersReturnThePreviousModelOnSuccess()
        {
            var view = new CallbackTestView();
            var previous = new DisposableLookupModel();
            var replacement = new DisposableLookupModel();
            view.Initialize(previous);

            Assert.That(view.ReinitializeSafely(replacement), Is.SameAs(previous));
            Assert.That(view.DeinitializeViewSafely(), Is.SameAs(replacement));
            Assert.That(view.ViewModel, Is.Null);

            view.Initialize(previous);
            Assert.That(((IView)view).DisposeViewSafely(), Is.SameAs(previous));
            Assert.That(view.ViewModel, Is.Null);
        }

        [Test]
        public void ViewsWithoutAModelReturnNull()
        {
            var view = new CallbackTestView();
            Assert.That(view.DeinitializeView(), Is.Null);
            Assert.That(view.Reinitialize(null), Is.Null);
            Assert.That(((IView)view).DisposeView(), Is.Null);
            Assert.That(new DisposableCallbackView().DisposeView(), Is.Null);
        }

        [Test]
        public void NullReplacementModelsDeinitializeTheView()
        {
            var view = new CallbackTestView();
            var model = new DisposableLookupModel();
            view.Initialize(model);
            Assert.That(view.Reinitialize(null), Is.SameAs(model));
            Assert.That(view.ViewModel, Is.Null);
        }

        [Test]
        public void AsBinderAttributesRejectInvalidTypesAndNullArgumentArrays()
        {
            Assert.Throws<ArgumentNullException>(() => new AsBinderAttribute(null));
            Assert.Throws<ArgumentNullException>(() => new AsBinderAttribute(typeof(CallbackTestBinder), null));
            Assert.Throws<ArgumentException>(() => new AsBinderAttribute(typeof(string)));
            Assert.Throws<ArgumentException>(() => new AsBinderAttribute(typeof(Binder)));
            Assert.Throws<ArgumentException>(() => new AsBinderAttribute(typeof(GenericCallbackBinder<>)));
            Assert.DoesNotThrow(() => new AsBinderAttribute(typeof(CallbackTestBinder), (object)null));
        }

        [Test]
        public void GeneratedAsBinderConstructorsPreserveLiteralsAndViewMemberArguments()
        {
            var view = new LiteralCallbackTestView();
            view.Initialize(new DisposableLookupModel());
            var binder = LiteralCallbackTestBinder.LastCreated;
            Assert.That(binder.Text, Is.EqualTo("quote\" slash\\ newline\n"));
            Assert.That(binder.Weight, Is.EqualTo(1.25f));
            Assert.That(binder.Converter, Is.SameAs(LiteralCallbackTestView.Converter));
            view.Deinitialize();
            Assert.That(binder.IsBound, Is.False);
        }

        private static void ExpectBindingFailure() =>
            LogAssert.Expect(LogType.Exception, new Regex(@"\[BindSafely\].*InvalidOperationException"));
    }

    internal sealed class CallbackTestBinder : Binder, IBinder<int>
    {
        public Action Attaching;
        public Action Bound;
        public Action Unbinding;
        public Action Unbound;
        public int UnboundCalls;

        public void SetValue(int value) { }

        protected override void OnAttach() =>
            Attaching?.Invoke();

        protected override void OnBound() =>
            Bound?.Invoke();

        protected override void OnUnbinding() =>
            Unbinding?.Invoke();

        protected override void OnUnbound()
        {
            UnboundCalls++;
            Unbound?.Invoke();
        }
    }

    internal class GenericCallbackBinder<T> : Binder { }

    [View(AutoBinderFields = false)]
    internal partial class CallbackTestView
    {
        public readonly CallbackTestBinder Value = new();
        public Action Initializing;
        public Action Deinitializing;
        public Action Deinitialized;
        public int DeinitializingCalls;

        partial void OnInitializingInternal(IViewModel viewModel) =>
            Initializing?.Invoke();

        partial void OnDeinitializingInternal()
        {
            DeinitializingCalls++;
            Deinitializing?.Invoke();
        }

        partial void OnDeinitializedInternal() =>
            Deinitialized?.Invoke();
    }

    [View(AutoBinderFields = false)]
    internal partial class TypedCallbackTestView : CallbackTestView, IView<DisposableLookupModel>
    {
        public Action TypedInitializing;

        partial void OnInitializingInternal(IViewModel viewModel) =>
            TypedInitializing?.Invoke();
    }

    internal sealed class DisposableCallbackView : CallbackTestView, IDisposable
    {
        public int DisposeCalls;

        public void Dispose()
        {
            DisposeCalls++;
            Deinitialize();
        }
    }

    [View(AutoBinderFields = false)]
    internal partial class LiteralCallbackTestView
    {
        public static readonly object Converter = new();

        [AsBinder(typeof(LiteralCallbackTestBinder), "quote\" slash\\ newline\n", 1.25f, 1u, nameof(Converter))]
        private int _value;
    }

    internal sealed class LiteralCallbackTestBinder : Binder, IBinder<int>
    {
        public static LiteralCallbackTestBinder LastCreated;
        public readonly string Text;
        public readonly float Weight;
        public readonly object Converter;

        public LiteralCallbackTestBinder(
            int target,
            string text,
            float weight,
            uint flags,
            object converter)
        {
            Text = text;
            Weight = weight;
            Converter = converter;
            LastCreated = this;
        }

        public LiteralCallbackTestBinder(
            int target,
            string text,
            double weight,
            int flags,
            object converter)
        {
            throw new InvalidOperationException("The argument types selected the wrong constructor.");
        }

        public void SetValue(int value) { }
    }

    internal sealed class DisposableLookupModel : IViewModel, IDisposable
    {
        private readonly OneWayStructBindableMember<int> _value = new(1);
        public int DisposeCalls;

        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters)
        {
            parameters.EnsureValid();
            return parameters.Id == "Value" ? new FindBindableMemberResult(_value) : default;
        }

        public void Dispose() =>
            DisposeCalls++;
    }
}
