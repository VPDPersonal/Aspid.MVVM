using Aspid.MVVM.StarterKit;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Records what <see cref="ObservableDictionaryMonoBinder{TKey, TValue}"/> dispatches, which is all this half
    /// of the collection domain is responsible for.
    /// </summary>
    internal sealed class ProbeDictionaryBinder : ObservableDictionaryMonoBinder<string, string>
    {
        public readonly List<KeyValuePair<string, string>> Added = new();
        public readonly List<KeyValuePair<string, string>> Removed = new();

        public int Resets { get; private set; }

        protected override void OnAdded(KeyValuePair<string, string> newItem) => Added.Add(newItem);

        protected override void OnAdded(IReadOnlyList<KeyValuePair<string, string>> newItems) => Added.AddRange(newItems);

        protected override void OnRemoved(KeyValuePair<string, string> oldItem) => Removed.Add(oldItem);

        protected override void OnRemoved(IReadOnlyList<KeyValuePair<string, string>> oldItems) => Removed.AddRange(oldItems);

        protected override void OnReplaced(KeyValuePair<string, string> oldItem, KeyValuePair<string, string> newItem)
        {
            Removed.Add(oldItem);
            Added.Add(newItem);
        }

        protected override void OnReset() => Resets++;
    }
}
