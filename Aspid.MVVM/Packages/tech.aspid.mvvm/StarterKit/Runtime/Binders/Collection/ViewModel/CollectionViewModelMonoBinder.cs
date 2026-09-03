using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="CollectionViewModelMonoBinder{TView}"/> over <see cref="MonoView"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collection/Collection Binder – ViewModel")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Collection/Collection Binder – ViewModel")]
    public class CollectionViewModelMonoBinder : CollectionViewModelMonoBinder<MonoView> { }

    /// <summary>
    /// <see cref="CollectionMonoBinder{T}"/> that shows bound ViewModels in a fixed set of pre-placed views, in order.
    /// Views beyond the item count are deactivated; items beyond the view count are not shown.
    /// </summary>
    /// <typeparam name="TView">The type of the pre-placed views.</typeparam>
    public abstract class CollectionViewModelMonoBinder<TView> : CollectionMonoBinder<IViewModel>
        where TView : MonoBehaviour, IView
    {
        [Tooltip("Views the items are shown in, in order. Extra items are not shown.")]
        [SerializeField] private TView[] _views;

        /// <inheritdoc/>
        protected override void OnAdded(IReadOnlyCollection<IViewModel> values)
        {
            var index = 0;

            foreach (var value in values)
            {
                if (index >= _views.Length) break;

                _views[index].gameObject.SetActive(true);
                _views[index].Initialize(value);

                index++;
            }

            for (var i = index; i < _views.Length; i++)
                _views[i].gameObject.SetActive(false);
        }

        /// <inheritdoc/>
        protected override void OnReset()
        {
            foreach (var view in _views)
            {
                view.Deinitialize();
                view.gameObject.SetActive(false);
            }
        }
    }
}
