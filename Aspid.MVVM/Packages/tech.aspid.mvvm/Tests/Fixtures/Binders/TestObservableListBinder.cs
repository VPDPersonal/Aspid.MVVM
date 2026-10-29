using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// An <see cref="ObservableListViewModelMonoBinder{TView}"/> over <see cref="StubView"/> whose view factory a test sets directly.
    /// </summary>
    public sealed class TestObservableListBinder : ObservableListViewModelMonoBinder<StubView>
    {
        public void UseFactory(StubViewFactory factory) =>
            typeof(ObservableListViewModelMonoBinder<StubView>)
                .GetField("_viewFactory", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                .SetValue(this, factory);
    }
}
