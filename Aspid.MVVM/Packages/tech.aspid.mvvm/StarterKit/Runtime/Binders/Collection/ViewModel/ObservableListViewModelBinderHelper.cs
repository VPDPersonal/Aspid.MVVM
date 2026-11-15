using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    internal static class ObservableListViewModelBinderHelper
    {
        public static void OnAdded<T>(List<T> views, IViewFactory<T> viewFactory, IViewModel newItem, int index)
            where T : MonoBehaviour, IView
        {
            if (index < 0 || index > views.Count) throw new ArgumentOutOfRangeException(nameof(index));
            var view = viewFactory.Create(newItem);

            try
            {
                view.gameObject.SetActive(newItem is not null);
                view.transform.SetSiblingIndex(index);
                views.Insert(index, view);
            }
            catch (Exception exception)
            {
                Rollback.Run(exception, (view, viewFactory, index),
                    static state => Release(state.view, state.viewFactory, state.index),
                    $"{typeof(T).FullName}: insert View at index {index}");
                throw;
            }
        }

        public static void OnRemoved<T>(List<T> views, IViewFactory<T> viewFactory, int oldStartingIndex)
            where T : MonoBehaviour, IView
        {
            var view = views[oldStartingIndex];
            views.RemoveAt(oldStartingIndex);
            Release(view, viewFactory, oldStartingIndex);
        }

        public static void OnReplaced<T>(List<T> views, IViewModel newItem, int index)
            where T : MonoBehaviour, IView
        {
            var view = views[index];
            view.Deinitialize();
            view.gameObject.SetActive(false);

            if (newItem is null) return;
            view.Initialize(newItem);
            view.gameObject.SetActive(true);
        }

        public static void OnMoved<T>(List<T> views, int oldStartingIndex, int newStartingIndex)
            where T : MonoBehaviour, IView
        {
            var view = views[oldStartingIndex];
            views.RemoveAt(oldStartingIndex);
            views.Insert(newStartingIndex, view);
            view.transform.SetSiblingIndex(newStartingIndex);
        }

        private static void Release<T>(T view, IViewFactory<T> factory, int index)
            where T : MonoBehaviour, IView
        {
            try
            {
                factory.Release(view);
            }
            catch (Exception exception)
            {
                var context = $"Factory {factory.GetType().FullName}: release View {typeof(T).FullName} at index {index}";
                Rollback.Run(exception, view, static item =>
                {
                    if (!item) return;
                    try
                    {
                        item.gameObject.SetActive(false);
                    }
                    finally
                    {
                        item.DestroyViewAndGameObject();
                    }
                }, context);
                throw new InvalidOperationException(context + ". Emergency View cleanup completed.", exception);
            }
        }

        public static void OnReset<T>(List<T> views, IViewFactory<T> viewFactory)
            where T : MonoBehaviour, IView
        {
            var failures = new CleanupExceptions();

            while (views.Count > 0)
            {
                var index = views.Count - 1;
                var view = views[index];
                views.RemoveAt(index);

                try
                {
                    Release(view, viewFactory, index);
                }
                catch (Exception exception)
                {
                    failures.Add(exception);
                }
            }

            failures.ThrowIfAny();
        }
    }
}
