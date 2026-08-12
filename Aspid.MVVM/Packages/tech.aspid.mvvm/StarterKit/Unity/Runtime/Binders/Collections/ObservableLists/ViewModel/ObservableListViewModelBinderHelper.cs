using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Shared per-item view-list bookkeeping used by both
    /// <see cref="ObservableListViewModelBinder{T, TViewFactory}"/> and
    /// <c>ObservableListViewModelMonoBinder&lt;T, TViewFactory&gt;</c>,
    /// which differ only in their base binder type.
    /// </summary>
    internal static class ObservableListViewModelBinderHelper
    {
        public static void OnAdded<T>(List<T> views, IViewFactory<T> viewFactory, IViewModel newItem, int newStartingIndex)
            where T : MonoBehaviour, IView
        {
            var view = viewFactory.Create(newItem);

            views.Insert(newStartingIndex, view);

            // The factory appends the new object last in the hierarchy, so an insert anywhere but the end would
            // leave the visual order — and therefore any LayoutGroup — out of step with the model. OnMove below
            // already does this.
            view.transform.SetSiblingIndex(newStartingIndex);
        }

        public static void OnRemoved<T>(List<T> views, IViewFactory<T> viewFactory, int oldStartingIndex)
            where T : MonoBehaviour, IView
        {
            viewFactory.Release(views[oldStartingIndex]);
            views.RemoveAt(oldStartingIndex);
        }

        public static void OnReplace<T>(List<T> views, IViewModel newItem, int newStartingIndex)
            where T : MonoBehaviour, IView
        {
            views[newStartingIndex].Deinitialize();

            if (newItem is not null)
                views[newStartingIndex].Initialize(newItem);
        }

        public static void OnMove<T>(List<T> views, int oldStartingIndex, int newStartingIndex)
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
