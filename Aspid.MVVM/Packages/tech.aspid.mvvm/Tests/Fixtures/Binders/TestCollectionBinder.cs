using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A <see cref="CollectionMonoBinder{T}"/> that records the values it applies.
    /// </summary>
    public sealed class TestCollectionBinder : CollectionMonoBinder<string>
    {
        public List<string> Applied { get; } = new();

        protected override void OnAdded(IReadOnlyCollection<string> values) =>
            Applied.AddRange(values);

        protected override void OnReset() =>
            Applied.Clear();
    }
}
