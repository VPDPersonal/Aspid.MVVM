using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A <see cref="CollectionViewModelMonoBinder{TView}"/> over <see cref="StubView"/> whose views a test sets directly.
    /// </summary>
    public sealed class TestCollectionViewModelBinder : CollectionViewModelMonoBinder<StubView>
    {
        public void SetViews(params StubView[] views) =>
            typeof(CollectionViewModelMonoBinder<StubView>)
                .GetField("_views", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                .SetValue(this, views);
    }
}
