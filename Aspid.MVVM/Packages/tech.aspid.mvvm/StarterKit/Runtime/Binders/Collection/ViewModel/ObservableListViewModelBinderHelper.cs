using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// View-list bookkeeping shared by <see cref="ObservableListViewModelBinder{TView}"/> and
    /// <see cref="ObservableListViewModelMonoBinder{TView}"/>.
    /// </summary>
    internal static class ObservableListViewModelBinderHelper
    {
        public static void OnAdded<T>(List<T> views, IViewFactory<T> viewFactory, IViewModel newItem, int index)
            where T : MonoBehaviour, IView
        {
            var view = viewFactory.Create(newItem);
            views.Insert(index, view);

            // The factory appends the view last in the hierarchy; keep the visual order equal to the list order.
            view.transform.SetSiblingIndex(index);
        }

        public static void OnRemoved<T>(List<T> views, IViewFactory<T> viewFactory, int oldStartingIndex)
            where T : MonoBehaviour, IView
        {
            viewFactory.Release(views[oldStartingIndex]);
            views.RemoveAt(oldStartingIndex);
        }

        public static void OnReplaced<T>(List<T> views, IViewModel newItem, int index)
            where T : MonoBehaviour, IView
        {
            views[index].Deinitialize();

            if (newItem is not null)
                views[index].Initialize(newItem);
        }

        public static void OnMoved<T>(List<T> views, int oldStartingIndex, int newStartingIndex)
            where T : MonoBehaviour, IView
        {
            var view = views[oldStartingIndex];

            views.RemoveAt(oldStartingIndex);
            views.Insert(newStartingIndex, view);

            view.transform.SetSiblingIndex(newStartingIndex);
        }

        public static void OnReset<T>(List<T> views, IViewFactory<T> viewFactory)
            where T : MonoBehaviour, IView
        {
            foreach (var view in views)
                viewFactory.Release(view);

            views.Clear();
        }
    }
}
