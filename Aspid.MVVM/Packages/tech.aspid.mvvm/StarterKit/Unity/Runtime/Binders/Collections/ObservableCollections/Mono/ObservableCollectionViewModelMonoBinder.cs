using UnityEngine;
using System.Collections.Generic;
using ViewFactory = Aspid.MVVM.StarterKit.IViewFactory<Aspid.MVVM.MonoView>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ObservableCollectionViewModelMonoBinder{TView, TViewFactory}"/> over <see cref="MonoView"/>.
    /// </summary>
    /// <remarks>
    /// The ready component for a set, a queue or a stack of ViewModels: one view per member, created and released as
    /// members come and go.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Collection/Observable Collection Binder – ViewModel")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Collection/Observable Collection Binder – ViewModel")]
    public class ObservableCollectionViewModelMonoBinder : ObservableCollectionViewModelMonoBinder<MonoView, ViewFactory> { }

    /// <summary>
    /// <see cref="ObservableCollectionMonoBinder{T}"/> that creates a view per member of a set, a queue or a stack and
    /// releases it when the member leaves.
    /// </summary>
    /// <remarks>
    /// Views are held by the ViewModel they show rather than by index, because none of these three collections has an
    /// index that survives a change — a queue renumbers on every dequeue.
    /// <para/>
    /// A member that appears twice is shown once: a set cannot hold a duplicate at all, and for a queue or a stack the
    /// alternative is two views bound to one ViewModel, which is worse than a missing one.
    /// </remarks>
    /// <typeparam name="TView">The type of view created for each member.</typeparam>
    /// <typeparam name="TViewFactory">The factory that creates and releases views.</typeparam>
    public abstract class ObservableCollectionViewModelMonoBinder<TView, TViewFactory> : ObservableCollectionMonoBinder<IViewModel>
        where TView : MonoBehaviour, IView
        where TViewFactory : IViewFactory<TView>
    {
        [Tooltip("Creates a view per collection member. Required — nothing is shown without it.")]
        [SerializeReference] private TViewFactory _viewFactory;

        private Dictionary<IViewModel, TView> _views;

        private Dictionary<IViewModel, TView> Views => _views ??= new Dictionary<IViewModel, TView>();

        /// <inheritdoc/>
        protected override void OnAdded(IViewModel newItem)
        {
            if (newItem is null) return;
            if (!IsUsable()) return;
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

            if (IsUsable()) _viewFactory.Release(view);
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
