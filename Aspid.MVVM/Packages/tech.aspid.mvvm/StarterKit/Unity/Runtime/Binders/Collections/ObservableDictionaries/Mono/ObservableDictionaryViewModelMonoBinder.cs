using UnityEngine;
using System.Collections.Generic;
using ViewFactory = Aspid.MVVM.StarterKit.IViewFactoryWithKey<Aspid.MVVM.MonoView>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ObservableDictionaryViewModelMonoBinder{TKey, TViewModel, TView, TViewFactory}"/> over
    /// <see cref="MonoView"/>, keyed by <see langword="string"/>.
    /// </summary>
    /// <remarks>
    /// The ready component for the common case: a dictionary of ViewModels keyed by an id, each shown by a view the
    /// factory creates and releases under that key.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Collection/Observable Dictionary Binder – ViewModel")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Collection/Observable Dictionary Binder – ViewModel")]
    public class ObservableDictionaryViewModelMonoBinder : ObservableDictionaryViewModelMonoBinder<string, IViewModel, MonoView, ViewFactory> { }

    /// <summary>
    /// <see cref="ObservableDictionaryMonoBinder{TKey, TValue}"/> that creates a view per dictionary entry and releases
    /// it when the entry goes away.
    /// </summary>
    /// <remarks>
    /// The MonoBehaviour half the dictionary domain was missing: the list domain had one and the dictionary domain did
    /// not, so a dictionary could be shown from a View's own field and not from a component dropped next to the objects
    /// it drives.
    /// <para/>
    /// Views are created and released through the factory by key, so a replacement under an existing key reuses nothing
    /// and leaks nothing: the old view is released and a new one is created. A missing factory is reported once rather
    /// than on every entry.
    /// </remarks>
    /// <typeparam name="TKey">The type of the dictionary's keys.</typeparam>
    /// <typeparam name="TViewModel">The type of ViewModel stored as the dictionary's values.</typeparam>
    /// <typeparam name="TView">The type of view created for each entry.</typeparam>
    /// <typeparam name="TViewFactory">The factory that creates and releases views by key.</typeparam>
    public abstract class ObservableDictionaryViewModelMonoBinder<TKey, TViewModel, TView, TViewFactory>
        : ObservableDictionaryMonoBinder<TKey, TViewModel>
        where TViewModel : IViewModel
        where TView : MonoBehaviour, IView
        where TViewFactory : IViewFactoryWithKey<TView>
    {
        [Tooltip("Creates and releases a view for each entry, keyed by the dictionary's key. Required — nothing is shown without it.")]
        [SerializeReference] private TViewFactory _viewFactory;

        private Dictionary<TKey, TView> _views;

        private Dictionary<TKey, TView> Views => _views ??= new Dictionary<TKey, TView>();

        /// <inheritdoc/>
        protected override void OnAdded(KeyValuePair<TKey, TViewModel> newItem)
        {
            if (!IsUsable()) return;
            if (Views.ContainsKey(newItem.Key)) return;

            // Фабрика создаёт view сразу со ViewModel и ключом — та же сигнатура, что у сериализуемого
            // близнеца, чтобы у одной и той же фабрики оба биндера вели себя одинаково.
            var view = _viewFactory.Create(newItem.Value, newItem.Key);
            Views.Add(newItem.Key, view);
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

            if (IsUsable()) _viewFactory.Release(view);
        }

        /// <inheritdoc/>
        protected override void OnRemoved(IReadOnlyList<KeyValuePair<TKey, TViewModel>> oldItems)
        {
            foreach (var item in oldItems)
                OnRemoved(item);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// The old view is released and a new one created rather than the existing view re-initialized: a factory may
        /// hand out a different prefab for a different ViewModel, and reusing the old one would show the wrong thing.
        /// </remarks>
        protected override void OnReplace(KeyValuePair<TKey, TViewModel> oldItem, KeyValuePair<TKey, TViewModel> newItem)
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
                if (IsUsable()) _viewFactory.Release(pair.Value);
            }

            _views.Clear();
        }

        private bool IsUsable()
        {
            if (_viewFactory is not null) return true;

            Debug.LogError($"[{GetType().Name}] No view factory assigned.", context: this);
            return false;
        }
    }
}
