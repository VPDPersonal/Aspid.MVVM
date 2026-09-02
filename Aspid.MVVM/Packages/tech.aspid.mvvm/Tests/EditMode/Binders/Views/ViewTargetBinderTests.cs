using System;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests <see cref="ViewTargetBinder{TView}"/> through <see cref="MonoViewBinder{TView}"/>: the constructor's
    /// mode and target guards, and view initialization on bind/unbind.
    /// </summary>
    [TestFixture]
    public sealed class ViewTargetBinderTests : SceneFixture
    {
        [Test]
        public void Constructor_WithOneWayMode_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new MonoViewBinder<StubView>(Spawn<StubView>(), BindMode.OneWay));
        }

        [Test]
        public void Constructor_WithOneTimeMode_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new MonoViewBinder<StubView>(Spawn<StubView>(), BindMode.OneTime));
        }

        [Test]
        public void Constructor_WithTwoWayMode_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => new MonoViewBinder<StubView>(Spawn<StubView>(), BindMode.TwoWay));
        }

        [Test]
        public void Constructor_WithOneWayToSourceMode_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(
                () => new MonoViewBinder<StubView>(Spawn<StubView>(), BindMode.OneWayToSource));
        }

        [Test]
        public void Constructor_WithNullTarget_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new MonoViewBinder<StubView>(null, BindMode.OneWay));
        }

        [Test]
        public void SetValue_WithAViewModel_InitializesTheView()
        {
            var view = Spawn<StubView>();
            var binder = new MonoViewBinder<StubView>(view, BindMode.OneWay);
            var viewModel = new StubViewModel();

            binder.SetValue(viewModel);

            Assert.AreSame(viewModel, view.ViewModel, "The view was not initialized with the ViewModel");
        }

        [Test]
        public void SetValue_WithNull_DeinitializesTheView()
        {
            var view = Spawn<StubView>();
            var binder = new MonoViewBinder<StubView>(view, BindMode.OneWay);
            binder.SetValue(new StubViewModel());

            binder.SetValue(null);

            Assert.IsNull(view.ViewModel, "A null value did not deinitialize the view");
        }

        [Test]
        public void Unbind_DeinitializesTheView()
        {
            var view = Spawn<StubView>();
            var binder = new MonoViewBinder<StubView>(view, BindMode.OneWay);
            var viewModel = new StubViewModel();

            binder.Bind(new OneWayBindableMember<IViewModel>(viewModel));
            Assert.IsNotNull(view.ViewModel, "The view was not initialized on bind");

            binder.Unbind();

            Assert.IsNull(view.ViewModel, "Unbind did not deinitialize the view");
        }
    }
}
