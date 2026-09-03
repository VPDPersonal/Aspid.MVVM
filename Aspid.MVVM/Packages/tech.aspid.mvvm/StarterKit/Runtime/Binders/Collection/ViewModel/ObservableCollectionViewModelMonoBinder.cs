using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ObservableCollectionViewModelMonoBinder{TView}"/> over <see cref="MonoView"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collection/Observable Collection Binder – ViewModel")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Collection/Observable Collection Binder – ViewModel")]
    public class ObservableCollectionViewModelMonoBinder : ObservableCollectionViewModelMonoBinder<MonoView> { }

    /// <summary>
    /// <see cref="ObservableCollectionMonoBinder{T}"/> that creates a view per ViewModel and releases it when the ViewModel leaves.
    /// Views are keyed by ViewModel, so a duplicate member is shown once.
    /// </summary>
    /// <typeparam name="TView">The type of view created per member.</typeparam>
    public abstract class ObservableCollectionViewModelMonoBinder<TView> : ObservableCollectionMonoBinder<IViewModel>
        where TView : MonoBehaviour, IView
    {
        [Tooltip("Creates a view per member. Required.")]
        [SerializeReference] private IViewFactory<TView> _viewFactory;

        private Dictionary<IViewModel, TView> _views;

        private Dictionary<IViewModel, TView> Views => _views ??= new Dictionary<IViewModel, TView>();

        /// <inheritdoc/>
        protected override void OnAdded(IViewModel newItem)
        {
            if (newItem is null) return;
            if (!HasFactory()) return;
            if (Views.ContainsKey(newItem)) return;

            Views.Add(newItem, _viewFactory.Create(newItem));
        }

        /// <inheritdoc/>
        protected override void OnRemoved(IViewModel oldItem)
        {
            if (oldItem is null) return;
            if (!Views.TryGetValue(oldItem, out var view)) return;

            view.Deinitialize();
            Views.Remove(oldItem);

            if (HasFactory()) _viewFactory.Release(view);
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
                consequence: "No view is created for the collection.");

            return false;
        }
    }
}
