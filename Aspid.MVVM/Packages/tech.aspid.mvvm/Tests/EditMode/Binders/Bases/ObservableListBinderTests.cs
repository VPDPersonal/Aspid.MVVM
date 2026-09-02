using NUnit.Framework;
using Aspid.MVVM.StarterKit;
using Aspid.Collections.Observable;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Tests for <see cref="ObservableListViewModelMonoBinder{TView, TFactory}"/>.
    /// </summary>
    [TestFixture]
    public sealed class ObservableListBinderTests : SceneFixture
    {
        /// <summary>
        /// The factory parents each new view last in the hierarchy, so an insert anywhere but the end left the
        /// visual order — and any LayoutGroup driven by it — out of step with the model.
        /// </summary>
        [Test]
        public void ObservableListViewModelMonoBinder_InsertAtTheFront_PutsTheViewFirstInTheHierarchy()
        {
            var parent = Spawn().transform;
            var binder = Spawn<TestObservableListBinder>();
            binder.UseFactory(new StubViewFactory(parent));

            var list = new ObservableList<IViewModel> { new StubViewModel(), new StubViewModel() };
            binder.SetValue(list);

            var inserted = new StubViewModel();
            list.Insert(0, inserted);

            Assert.AreEqual(3, parent.childCount);
            Assert.AreSame(
                inserted,
                parent.GetChild(0).GetComponent<StubView>().ViewModel,
                "The item inserted at the front did not become first in the hierarchy");
        }
    }

    internal sealed class TestObservableListBinder : ObservableListViewModelMonoBinder<StubView, StubViewFactory>
    {
        public void UseFactory(StubViewFactory factory) =>
            typeof(ObservableListViewModelMonoBinder<StubView, StubViewFactory>)
                .GetField("_viewFactory", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                .SetValue(this, factory);
    }
}
