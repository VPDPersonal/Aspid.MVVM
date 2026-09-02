using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="CollectionViewModelMonoBinder{T}"/> that uses <see cref="MonoView"/> as the view type.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collection/Collection Binder – ViewModel")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Collection/Collection Binder – ViewModel")]
    public class CollectionViewModelMonoBinder : CollectionViewModelMonoBinder<MonoView> { }

    /// <summary>
    /// <see cref="CollectionMonoBinder{T}"/> that distributes bound <see cref="IViewModel"/> values across a fixed
    /// array of pre-instantiated <typeparamref name="T"/> view objects, activating and initializing each view in
    /// order and deactivating any excess views.
    /// </summary>
    /// <typeparam name="T">The type of pre-instantiated <see cref="MonoBehaviour"/> view objects in the collection.</typeparam>
    public abstract class CollectionViewModelMonoBinder<T> : CollectionMonoBinder<IViewModel>
        where T : MonoBehaviour, IView
    {
        [Tooltip("Views the items are shown in, in order. Extra items beyond this many are hidden.")]
        [SerializeField] private T[] _views;

        protected override void OnAdded(IReadOnlyCollection<IViewModel> values)
        {
            var index = 0;
            
            foreach (var value in values)
            {
                // The serialized view list is fixed; a collection longer than it must not run past the end.
                if (index >= _views.Length) break;

                _views[index].gameObject.SetActive(true);
                _views[index].Initialize(value);

                index++;
            }
            
            for (var i = index; i < _views.Length; i++)
                _views[i].gameObject.SetActive(false);
        }

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