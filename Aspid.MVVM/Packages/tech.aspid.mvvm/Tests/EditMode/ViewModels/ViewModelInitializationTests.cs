using System;
using System.Collections.Generic;
using Aspid.MVVM.StarterKit;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    [TestFixture]
    internal sealed class ViewModelInitializationTests
    {
        [Test]
        public void SingleViewReceivesTheSameModelOnce()
        {
            var model = new EmptyViewModel();
            var view = new RecordingView();
            model.InitializeView(view);
            Assert.That(view.ViewModel, Is.SameAs(model));
            Assert.That(view.Calls, Is.EqualTo(1));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void CollectionsInitializeViewsInOrder(int kind)
        {
            var model = new EmptyViewModel();
            var order = new List<IView>();
            var first = new RecordingView { Order = order };
            var second = new RecordingView { Order = order };
            switch (kind)
            {
                case 0: model.InitializeViews(new IView[] { first, second }); break;
                case 1: model.InitializeViews(new List<IView> { first, second }); break;
                case 2: model.InitializeViews(Enumerate(first, second)); break;
                case 3: model.InitializeViews(new List<RecordingView> { first, second }); break;
                case 4: model.InitializeViews(first, second); break;
                case 5: model.InitializeViews(new[] { first, second }.AsSpan()); break;
                case 6: model.InitializeViews((ReadOnlySpan<RecordingView>)new[] { first, second }); break;
            }

            Assert.That(order, Is.EqualTo(new IView[] { first, second }));
            Assert.That(first.ViewModel, Is.SameAs(model));
            Assert.That(second.ViewModel, Is.SameAs(model));
            Assert.That(first.Calls, Is.EqualTo(1));
            Assert.That(second.Calls, Is.EqualTo(1));
        }

        [Test]
        public void NullConditionalCallsSkipViewsAndArgumentEvaluation()
        {
            IViewModel model = null;
            var view = new RecordingView();
            model?.InitializeView(view);
            model?.InitializeViews(new[] { view });
            model?.InitializeViews(new List<IView> { view });
            model?.InitializeViews(Enumerate(view));
            model?.InitializeView(ThrowOnEvaluation());
            Assert.That(view.Calls, Is.Zero);
        }

        [Test]
        public void DirectCallsPassTheModelThroughToTheView()
        {
            IViewModel model = null;
            var view = new RecordingView();
            model.InitializeView(view);
            model.InitializeViews(Enumerate(view));
            model.InitializeViews(Array.Empty<IView>());

            Assert.That(view.Calls, Is.EqualTo(2));
            Assert.That(view.ViewModel, Is.Null);
        }

        [Test]
        public void NullTargetsAreRejectedAndEmptyCollectionsAreAccepted()
        {
            var model = new EmptyViewModel();
            Assert.Throws<NullReferenceException>(() => model.InitializeView(null));
            Assert.Throws<ArgumentNullException>(() => model.InitializeViews((IView[])null));
            Assert.Throws<ArgumentNullException>(() => model.InitializeViews((List<IView>)null));
            Assert.Throws<ArgumentNullException>(() => model.InitializeViews((IEnumerable<IView>)null));
            model.InitializeViews(Array.Empty<IView>());
            model.InitializeViews(new List<IView>());
            model.InitializeViews(Enumerate());
        }

        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        public void FailureStopsInitializationWithoutRollingBackEarlierViews(int kind, bool nullElement)
        {
            var model = new EmptyViewModel();
            var first = new RecordingView();
            var failure = new InvalidOperationException("initialize");
            var broken = nullElement ? null : new RecordingView { Failure = failure };
            var last = new RecordingView();
            var views = new IView[] { first, broken, last };
            var exception = Assert.Catch(() =>
            {
                switch (kind)
                {
                    case 0: model.InitializeViews(views); break;
                    case 1: model.InitializeViews(new List<IView>(views)); break;
                    case 2: model.InitializeViews(Enumerate(views)); break;
                }
            });

            if (nullElement)
                Assert.That(exception, Is.TypeOf<NullReferenceException>());
            else
                Assert.That(exception, Is.SameAs(failure));
            Assert.That(first.ViewModel, Is.SameAs(model));
            Assert.That(last.Calls, Is.Zero);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void SafeBatchSkipsMissingViewsAndStopsAtFailure(int kind)
        {
            var model = new EmptyViewModel();
            var destroyed = ScriptableObject.CreateInstance<StubScriptableView>();
            Object.DestroyImmediate(destroyed);
            var broken = new RecordingView { Failure = new InvalidOperationException("safe initialization") };
            var last = new RecordingView();
            var first = new RecordingView();
            var views = new IView[] { destroyed, null, first, broken, last };
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                switch (kind)
                {
                    case 0: model.InitializeViewsSafely(views); break;
                    case 1: model.InitializeViewsSafely(new List<IView>(views)); break;
                    case 2: model.InitializeViewsSafely(Enumerate(views)); break;
                }
            });

            Assert.That(exception, Is.SameAs(broken.Failure));
            Assert.That(first.ViewModel, Is.SameAs(model));
            Assert.That(destroyed.ViewModel, Is.Null);
            Assert.That(broken.Calls, Is.EqualTo(1));
            Assert.That(last.Calls, Is.Zero);
        }

        [Test]
        public void SafeBatchPropagatesEnumerationFailure()
        {
            var model = new EmptyViewModel();
            var view = new RecordingView();
            var failure = Assert.Throws<InvalidOperationException>(() => model.InitializeViewsSafely(EnumerateWithFailure(view)));
            Assert.That(failure.Message, Is.EqualTo("safe enumeration"));
            Assert.That(view.ViewModel, Is.SameAs(model));
        }

        [Test]
        public void ReinitializationReturnsThePreviousModelAndAcceptsAMissingReplacement()
        {
            var first = new EmptyViewModel();
            var replacement = new EmptyViewModel();
            var view = new RecordingView();
            first.InitializeView(view);

            Assert.That(replacement.ReinitializeView(view), Is.SameAs(first));
            Assert.That(view.ViewModel, Is.SameAs(replacement));

            IViewModel missing = null;
            missing.ReinitializeViewsSafely(new IView[] { null, view }.AsSpan());
            Assert.That(view.ViewModel, Is.Null);
        }

        private static IEnumerable<IView> EnumerateWithFailure(IView view)
        {
            yield return view;
            throw new InvalidOperationException("safe enumeration");
        }

        private static IView ThrowOnEvaluation() =>
            throw new InvalidOperationException("argument evaluated");

        private static IEnumerable<IView> Enumerate(params IView[] views)
        {
            foreach (var view in views)
                yield return view;
        }

        private sealed class RecordingView : IView
        {
            public int Calls;
            public Exception Failure;
            public List<IView> Order;

            public IViewModel ViewModel { get; private set; }

            public void Initialize(IViewModel viewModel)
            {
                Calls++;
                if (Failure is not null)
                    throw Failure;
                ViewModel = viewModel;
                Order?.Add(this);
            }

            public void Deinitialize() =>
                ViewModel = null;
        }
    }
}
