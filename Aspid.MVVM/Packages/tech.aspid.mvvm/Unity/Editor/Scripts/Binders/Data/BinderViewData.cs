#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// One entry of the view dropdown: a view this binder could attach to, with the label shown for it.
    /// </summary>
    public readonly struct BinderViewData
    {
        /// <summary>
        /// The view itself.
        /// </summary>
        public readonly IView View;
        /// <summary>
        /// The label shown for the view in the dropdown.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Creates a dropdown entry with the View label.
        /// </summary>
        /// <param name="view">The View represented by the entry.</param>
        public BinderViewData(IView view)
        {
            View = view;
            Name = GetViewName(view as Component);
        }
        
        /// <summary>
        /// Formats a View label with its object name, type and duplicate index.
        /// </summary>
        /// <param name="view">The View component; <see langword="null"/> for an empty label.</param>
        /// <returns>The label; an empty string when <paramref name="view"/> is <see langword="null"/> or destroyed.</returns>
        /// <exception cref="System.InvalidCastException"><paramref name="view"/> does not implement <see cref="IView"/>.</exception>
        /// <exception cref="System.InvalidOperationException"><paramref name="view"/> is absent from its GameObject components.</exception>
        public static string GetViewName(Component? view)
        {
            if (!view) return string.Empty;
            if (view is not IView) throw new InvalidCastException("View is not IView");
            
            var type = view.GetType();
            var typeName = type.Name;

            var views = view.GetComponents(type);
            if (views.Length is 1) return $"{view.name} ({typeName})";
            
            var index = 0;
	        
            foreach (var component in views)
            {
                if (component.GetType() != type) continue;

                index++;
                if (component == view) return $"{view.name} ({typeName} ({index}))";
            }

            throw new InvalidOperationException(
                $"View component not found in hierarchy. View: {view?.name}, Type: {type?.Name}");
        }
    }
}