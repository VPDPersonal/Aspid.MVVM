using System;
using System.Collections.Generic;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    internal sealed class ViewLifecycleContractTests
    {
        [TestCase(false)]
        [TestCase(true)]
        public void FailedOuterBindingRollsBackDeclaredNestedBindings(bool collection)
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
            var failure = Assert.Throws<InvalidOperationException>(() => view.Initialize(new DisposableLookupModel()));

            Assert.That(failure.Message, Is.EqualTo("outer"));
            Assert.That(view.ViewModel, Is.Null);
            Assert.That(outer.IsBound || nested.IsBound, Is.False);
            outer.Attaching = null;
            outer.Bind(new BindContext(member, outer));
            view.Deinitialize();
            Assert.That(nested.IsBound, Is.False);
            Assert.That(outer.IsBound, Is.True);
            outer.Unbind(new UnbindContext(outer));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void InitializationRollbackReleasesBindingAfterCompletionFailure(bool collection)
        {
            var binder = new CallbackTestBinder { Bound = () => throw new InvalidOperationException("notification") };
            IView view = collection
                ? new FixedArrayBindingView { Value = new IBinder[] { binder } }
                : new FixedScalarBindingView { Value = binder };
            var failure = Assert.Throws<InvalidOperationException>(() => view.Initialize(new FixedBindingModel()));

            Assert.That(failure.Message, Is.EqualTo("notification"));
            Assert.That(view.ViewModel, Is.Null);
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
            Assert.That(missing?.Reinitialize(new DisposableLookupModel()), Is.Null);
            Assert.That(missing?.DisposeView(), Is.Null);
            Assert.That(missingDisposable?.DisposeView(), Is.Null);
            Assert.That(missing?.DestroyView(), Is.Null);
            Assert.That(missing?.DestroyViewAndGameObject(), Is.Null);
        }

        [Test]
        public void SafeLifecycleHelpersSkipNullViews()
        {
            IView missing = null;
            DisposableCallbackView missingDisposable = null;

            var model = new DisposableLookupModel();
            missing.InitializeSafely(model);
            Assert.That(missing.ReinitializeSafely(model), Is.Null);
            Assert.That(missing.DeinitializeViewSafely(), Is.Null);
            Assert.That(missing.DisposeViewSafely(), Is.Null);
            Assert.That(missingDisposable.DisposeViewSafely(), Is.Null);
        }

        [Test]
        public void InitializeSafelyPropagatesFailureAndAllowsRetry()
        {
            var view = new CallbackTestView();
            var model = new DisposableLookupModel();
            var failure = new InvalidOperationException("safe initialization");
            view.Initializing = () => throw failure;

            Assert.That(Assert.Throws<InvalidOperationException>(() => view.InitializeSafely(model)), Is.SameAs(failure));
            Assert.That(view.ViewModel, Is.Null);

            view.Initializing = null;
            view.InitializeSafely(model);
            view.InitializeSafely(null);
            Assert.That(view.ViewModel, Is.SameAs(model));
        }

        [Test]
        public void ReinitializeSafelyPropagatesInitializationFailure()
        {
            var view = new CallbackTestView();
            view.Initialize(new DisposableLookupModel());
            var failure = new InvalidOperationException("safe initialization");
            view.Initializing = () => throw failure;

            Assert.That(Assert.Throws<InvalidOperationException>(() => view.ReinitializeSafely(new DisposableLookupModel())), Is.SameAs(failure));
            Assert.That(view.ViewModel, Is.Null);
        }

        [Test]
        public void SafeDeinitializationPropagatesFailureAndStopsReinitialization()
        {
            var model = new DisposableLookupModel();
            var view = new CallbackTestView();
            var failure = new InvalidOperationException("safe deinitialization");
            view.Deinitialized = () => throw failure;

            view.Initialize(model);
            Assert.That(Assert.Throws<InvalidOperationException>(() => view.ReinitializeSafely(new DisposableLookupModel())), Is.SameAs(failure));
            Assert.That(view.ViewModel, Is.Null);

            view.Initialize(model);
            Assert.That(Assert.Throws<InvalidOperationException>(() => view.DeinitializeViewSafely()), Is.SameAs(failure));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void DisposeViewSafelyPropagatesFailureAndInvokesDisposeOnce(bool typed)
        {
            var view = new DisposableCallbackView();
            view.Initialize(new DisposableLookupModel());
            var failure = new InvalidOperationException("safe disposal");
            view.Deinitialized = () => throw failure;

            var failureThrown = Assert.Throws<InvalidOperationException>(() =>
            {
                if (typed) view.DisposeViewSafely();
                else ((IView)view).DisposeViewSafely();
            });
            Assert.That(failureThrown, Is.SameAs(failure));
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
        public void DeinitializeViewReturnsThePreviousModel()
        {
            var view = new CallbackTestView();
            var model = new DisposableLookupModel();
            view.Initialize(model);

            Assert.That(view.DeinitializeView(), Is.SameAs(model));
            Assert.That(view.ViewModel, Is.Null);
        }

        [Test]
        public void ViewsWithoutAModelReturnNull()
        {
            var view = new CallbackTestView();
            Assert.That(view.DeinitializeView(), Is.Null);
            Assert.That(view.DeinitializeViewSafely(), Is.Null);
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
        public void InitializeViewsStopsAtNullEntry()
        {
            var first = new CallbackTestView();
            var last = new CallbackTestView();
            var model = new DisposableLookupModel();
            var views = new[] { first, null, last };

            Assert.Throws<NullReferenceException>(() => views.InitializeViews(model));
            Assert.That(first.ViewModel, Is.SameAs(model));
            Assert.That(last.ViewModel, Is.Null);
        }

        [Test]
        public void InitializeViewsSafelyStopsAtFirstViewFailure()
        {
            var model = new DisposableLookupModel();
            var last = new CallbackTestView();
            var failure = new InvalidOperationException("Initialization failed.");
            Assert.That(Assert.Throws<InvalidOperationException>(() => Enumerate().InitializeViewsSafely(model)), Is.SameAs(failure));
            Assert.That(last.ViewModel, Is.Null);

            IEnumerable<IView> Enumerate()
            {
                yield return null;
                yield return new CallbackTestView { Initializing = () => throw failure };
                yield return last;
            }
        }

        [Test]
        public void ReinitializeViewsStopsAtFirstFailure()
        {
            var original = new DisposableLookupModel();
            var replacement = new DisposableLookupModel();
            var first = new CallbackTestView();
            var failure = new InvalidOperationException("Reinitialization failed.");
            var failing = new CallbackTestView();
            var last = new CallbackTestView();
            var views = new List<CallbackTestView> { first, failing, last };
            views.InitializeViews(original);
            failing.Initializing = () => throw failure;

            Assert.That(Assert.Throws<InvalidOperationException>(() => views.ReinitializeViews(replacement)), Is.SameAs(failure));
            Assert.That(first.ViewModel, Is.SameAs(replacement));
            Assert.That(failing.ViewModel, Is.Null);
            Assert.That(last.ViewModel, Is.SameAs(original));
        }

        [Test]
        public void ReinitializeViewsSafelyStopsAtFirstViewFailure()
        {
            var original = new DisposableLookupModel();
            var replacement = new DisposableLookupModel();
            var failing = new CallbackTestView();
            var last = new CallbackTestView();
            var views = new[] { null, failing, last };
            views.InitializeViewsSafely(original);
            var failure = new InvalidOperationException("Safe reinitialization failed.");
            failing.Initializing = () => throw failure;

            Assert.That(Assert.Throws<InvalidOperationException>(() => views.ReinitializeViewsSafely(replacement)), Is.SameAs(failure));

            Assert.That(failing.ViewModel, Is.Null);
            Assert.That(last.ViewModel, Is.SameAs(original));
        }

        [Test]
        public void ReinitializeViewsSafelyWithNullModelDeinitializesAndPropagatesEnumerationFailure()
        {
            var view = new CallbackTestView();
            view.Initialize(new DisposableLookupModel());
            var failure = new InvalidOperationException("Enumeration failed.");

            Assert.That(Assert.Throws<InvalidOperationException>(() => Enumerate().ReinitializeViewsSafely(null)), Is.SameAs(failure));
            Assert.That(view.ViewModel, Is.Null);

            IEnumerable<IView> Enumerate()
            {
                yield return view;
                throw failure;
            }
        }

        [Test]
        public void DeinitializeViewsContinuesAfterFailureAndRethrowsTheOriginalException()
        {
            var failure = new InvalidOperationException("first");
            var first = new CallbackTestView { Deinitialized = () => throw failure };
            var last = new CallbackTestView();
            var views = new List<CallbackTestView> { first, last };
            views.InitializeViews(new DisposableLookupModel());

            Assert.That(Assert.Throws<InvalidOperationException>(() => views.DeinitializeViews()), Is.SameAs(failure));
            Assert.That(last.ViewModel, Is.Null);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void DeinitializeViewsPreservesCleanupFailureWhenListChanges(bool safely)
        {
            var failure = new InvalidOperationException("Cleanup failed.");
            var first = new CallbackTestView { Deinitialized = () => throw failure };
            var second = new CallbackTestView();
            var views = new List<CallbackTestView> { first, second };
            second.Deinitialized = views.Clear;
            views.InitializeViews(new DisposableLookupModel());

            var aggregate = Assert.Throws<AggregateException>(() =>
            {
                if (safely) views.DeinitializeViewsSafely();
                else views.DeinitializeViews();
            });

            Assert.That(aggregate.InnerExceptions, Has.Count.EqualTo(2));
            Assert.That(aggregate.InnerExceptions[0], Is.SameAs(failure));
            Assert.That(aggregate.InnerExceptions[1], Is.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void DeinitializeViewsCollectsViewAndEnumerationFailuresInOrder()
        {
            var first = new InvalidOperationException("first");
            var second = new InvalidOperationException("second");
            var enumeration = new InvalidOperationException("enumeration");
            var views = new[]
            {
                new CallbackTestView { Deinitialized = () => throw first },
                new CallbackTestView { Deinitialized = () => throw second },
                new CallbackTestView(),
            };
            views.InitializeViews(new DisposableLookupModel());

            var aggregate = Assert.Throws<AggregateException>(() => EnumerateViews().DeinitializeViews());
            Assert.That(aggregate.InnerExceptions, Is.EqualTo(new[] { first, second, enumeration }));
            Assert.That(views[2].ViewModel, Is.Null);

            IEnumerable<CallbackTestView> EnumerateViews()
            {
                foreach (var view in views)
                    yield return view;

                throw enumeration;
            }
        }

        [Test]
        public void DeinitializeViewsContinuesAfterNullEntry()
        {
            var last = new CallbackTestView();
            last.Initialize(new DisposableLookupModel());
            var views = new CallbackTestView[] { null, last };

            Assert.Throws<NullReferenceException>(() => views.DeinitializeViews());
            Assert.That(last.ViewModel, Is.Null);
        }

        [Test]
        public void DeinitializeViewsSafelyContinuesAfterFailureAndRethrowsIt()
        {
            var failure = new InvalidOperationException("Cleanup failed.");
            var failing = new CallbackTestView { Deinitialized = () => throw failure };
            var last = new CallbackTestView();
            var views = new[] { null, failing, last };
            views.InitializeViewsSafely(new DisposableLookupModel());

            Assert.That(Assert.Throws<InvalidOperationException>(() => views.DeinitializeViewsSafely()), Is.SameAs(failure));
            Assert.That(last.ViewModel, Is.Null);
        }

        [Test]
        public void DeinitializeViewsSafelyPropagatesEnumerationFailureAfterProcessingEarlierViews()
        {
            var view = new CallbackTestView();
            view.Initialize(new DisposableLookupModel());

            var failure = new InvalidOperationException("Enumeration failed.");
            Assert.That(Assert.Throws<InvalidOperationException>(() => EnumerateViews().DeinitializeViewsSafely()), Is.SameAs(failure));

            Assert.That(view.ViewModel, Is.Null);

            IEnumerable<IView> EnumerateViews()
            {
                yield return view;
                throw failure;
            }
        }

        [Test]
        public void DeinitializeViewsCollectsReadOnlyCollectionFailuresTogether()
        {
            var failure = new InvalidOperationException("Cleanup failed.");
            var view = new CallbackTestView { Deinitialized = () => throw failure };
            view.Initialize(new DisposableLookupModel());
            IReadOnlyCollection<IView> views = new FailingViewCollection(view);

            var aggregate = Assert.Throws<AggregateException>(() => views.DeinitializeViews());

            Assert.That(aggregate.InnerExceptions, Has.Count.EqualTo(3));
            Assert.That(aggregate.InnerExceptions[0], Is.SameAs(failure));
            Assert.That(aggregate.InnerExceptions[1].Message, Is.EqualTo("Traversal failed."));
            Assert.That(aggregate.InnerExceptions[2].Message, Is.EqualTo("Disposal failed."));
            Assert.That(view.ViewModel, Is.Null);
        }

        [TestCase("array")]
        [TestCase("span")]
        [TestCase("read-only span")]
        [TestCase("list")]
        [TestCase("sequence")]
        public void DisposeViewsCleansUpLaterViewsBeforeRethrowing(string collection)
        {
            var failure = new InvalidOperationException("Dispose failed.");
            var first = new DisposableCallbackView { Deinitialized = () => throw failure };
            var last = new DisposableCallbackView();
            var plain = new CallbackTestView();
            IView[] views = { first, last, plain };
            views.InitializeViews(new DisposableLookupModel());

            var thrown = Assert.Throws<InvalidOperationException>(() =>
            {
                switch (collection)
                {
                    case "array": views.DisposeViews(); break;
                    case "span": views.AsSpan().DisposeViews(); break;
                    case "read-only span": new ReadOnlySpan<IView>(views).DisposeViews(); break;
                    case "list": new List<IView>(views).DisposeViews(); break;
                    case "sequence": new List<IView>(views).AsReadOnly().DisposeViews(); break;
                    default: throw new ArgumentOutOfRangeException(nameof(collection));
                }
            });

            Assert.That(thrown, Is.SameAs(failure));
            Assert.That(first.DisposeCalls, Is.EqualTo(1));
            Assert.That(last.DisposeCalls, Is.EqualTo(1));
            Assert.That(plain.DeinitializingCalls, Is.EqualTo(1));
        }

        [TestCase(nameof(ViewExtensions.DisposeViews))]
        [TestCase(nameof(ViewExtensions.DeinitializeViewsSafely))]
        [TestCase(nameof(ViewExtensions.DisposeViewsSafely))]
        [TestCase(nameof(ViewExtensions.DestroyViews))]
        [TestCase(nameof(ViewExtensions.DestroyViewsSafely))]
        [TestCase(nameof(ViewExtensions.DestroyViewsAndGameObjects))]
        [TestCase(nameof(ViewExtensions.DestroyViewsAndGameObjectsSafely))]
        public void CleanupAggregatesViewTraversalAndDisposalFailuresInOrder(string method)
        {
            var view = new CallbackTestView();
            view.Initialize(new DisposableLookupModel());
            IReadOnlyCollection<IView> views = new FailingViewCollection(view);
            var failure = new InvalidOperationException("Cleanup failed.");
            view.Deinitialized = () => throw failure;
            TestDelegate cleanup = method switch
            {
                nameof(ViewExtensions.DeinitializeViewsSafely) => () => views.DeinitializeViewsSafely(),
                nameof(ViewExtensions.DisposeViews) => () => views.DisposeViews(),
                nameof(ViewExtensions.DisposeViewsSafely) => () => views.DisposeViewsSafely(),
                nameof(ViewExtensions.DestroyViews) => () => views.DestroyViews(),
                nameof(ViewExtensions.DestroyViewsSafely) => () => views.DestroyViewsSafely(),
                nameof(ViewExtensions.DestroyViewsAndGameObjects) => () => views.DestroyViewsAndGameObjects(),
                nameof(ViewExtensions.DestroyViewsAndGameObjectsSafely) => () => views.DestroyViewsAndGameObjectsSafely(),
                _ => throw new ArgumentOutOfRangeException(nameof(method)),
            };

            var aggregate = Assert.Throws<AggregateException>(cleanup);
            Assert.That(aggregate.InnerExceptions, Has.Count.EqualTo(3));
            Assert.That(aggregate.InnerExceptions[0], Is.SameAs(failure));
            Assert.That(aggregate.InnerExceptions[1].Message, Is.EqualTo("Traversal failed."));
            Assert.That(aggregate.InnerExceptions[2].Message, Is.EqualTo("Disposal failed."));

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

        private sealed class FailingViewCollection : IReadOnlyCollection<IView>, IEnumerator<IView>
        {
            private bool _visited;

            public IView Current { get; }

            public FailingViewCollection(IView view) =>
                Current = view;

            public int Count => 1;

            object System.Collections.IEnumerator.Current => Current;

            public IEnumerator<IView> GetEnumerator() =>
                this;

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() =>
                GetEnumerator();

            public bool MoveNext()
            {
                if (_visited)
                    throw new InvalidOperationException("Traversal failed.");

                _visited = true;
                return true;
            }

            public void Reset() =>
                throw new NotSupportedException();

            public void Dispose() =>
                throw new InvalidOperationException("Disposal failed.");
        }
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
