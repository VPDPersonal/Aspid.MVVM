using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Records what <see cref="ObservableCollectionMonoBinder{T}"/> dispatches, which is all this binder is
    /// responsible for.
    /// </summary>
    internal sealed class ProbeCollectionBinder : ObservableCollectionMonoBinder<string>
    {
        public readonly List<string> Added = new();
        public readonly List<string> Removed = new();

        protected override void OnAdded(string newItem) => Added.Add(newItem);

        protected override void OnRemoved(string oldItem) => Removed.Add(oldItem);

        protected override void OnReset() { }
    }
}
