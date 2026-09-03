using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ObservableDictionaryViewModelMonoBinder{TKey, TViewModel, TView}"/> over <see cref="MonoView"/>, keyed by <see langword="string"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collection/Observable Dictionary Binder – ViewModel")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Collection/Observable Dictionary Binder – ViewModel")]
    public class ObservableDictionaryViewModelMonoBinder : ObservableDictionaryViewModelMonoBinder<string, IViewModel, MonoView> { }

    /// <summary>
    /// <see cref="ObservableDictionaryMonoBinder{TKey, TValue}"/> that creates a view per entry through a keyed factory
    /// and releases it when the entry leaves. A replacement releases the old view and creates a new one.
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
    /// <typeparam name="TViewModel">The type of ViewModel stored as values.</typeparam>
    /// <typeparam name="TView">The type of view created per entry.</typeparam>
    public abstract class ObservableDictionaryViewModelMonoBinder<TKey, TViewModel, TView>
        : ObservableDictionaryMonoBinder<TKey, TViewModel>
        where TViewModel : IViewModel
        where TView : MonoBehaviour, IView
    {
        [Tooltip("Creates a view per entry by key. Required.")]
        [SerializeReference] private IViewFactoryWithKey<TView> _viewFactory;

        private Dictionary<TKey, TView> _views;

        private Dictionary<TKey, TView> Views => _views ??= new Dictionary<TKey, TView>();

        /// <inheritdoc/>
        protected override void OnAdded(KeyValuePair<TKey, TViewModel> newItem)
        {
            if (!HasFactory()) return;
            if (Views.ContainsKey(newItem.Key)) return;

            Views.Add(newItem.Key, _viewFactory.Create(newItem.Value, newItem.Key));
        }

        /// <inheritdoc/>
        protected override void OnAdded(IReadOnlyList<KeyValuePair<TKey, TViewModel>> newItems)
        {
            foreach (var item in newItems)
                OnAdded(item);
        }

        /// <inheritdoc/>
        protected override void OnRemoved(KeyValuePair<TKey, TViewModel> oldItem)
        {
            if (!Views.TryGetValue(oldItem.Key, out var view)) return;

            view.Deinitialize();
            Views.Remove(oldItem.Key);

            if (HasFactory()) _viewFactory.Release(view);
        }

        /// <inheritdoc/>
        protected override void OnRemoved(IReadOnlyList<KeyValuePair<TKey, TViewModel>> oldItems)
        {
            foreach (var item in oldItems)
                OnRemoved(item);
        }

        /// <inheritdoc/>
        protected override void OnReplaced(KeyValuePair<TKey, TViewModel> oldItem, KeyValuePair<TKey, TViewModel> newItem)
        {
            OnRemoved(oldItem);
            OnAdded(newItem);
        }

        /// <inheritdoc/>
        protected override void OnReset()
        {
            if (_views is null) return;

            foreach (var pair in _views)
            {
                pair.Value.Deinitialize();
                if (HasFactory()) _viewFactory.Release(pair.Value);
            }

            _views.Clear();
        }

        private bool HasFactory()
        {
            if (_viewFactory is not null) return true;

            this.LogError(
                problem: "no view factory is assigned",
                consequence: "No view is created for the dictionary.");

            return false;
        }
    }
}
