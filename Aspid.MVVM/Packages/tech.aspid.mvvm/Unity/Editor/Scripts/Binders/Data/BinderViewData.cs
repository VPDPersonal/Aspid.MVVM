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
        /// Initializes a new entry for the given view and computes its label.
        /// </summary>
        /// <param name="view">The view this entry stands for.</param>
        public BinderViewData(IView view)
        {
            View = view;
            Name = GetViewName(view as Component);
        }
        
        /// <summary>
        /// Builds the label for a view: its object name, its type, and an index when the object carries more
        /// than one view of that type.
        /// </summary>
        /// <param name="view">The view to label, or <see langword="null"/>.</param>
        /// <returns>The label, or an empty string when <paramref name="view"/> is null or destroyed.</returns>
        /// <exception cref="System.InvalidCastException">
        /// Thrown when <paramref name="view"/> is a component that does not implement <see cref="IView"/>.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the view cannot be found among the components of its own GameObject, which means it was
        /// removed between the lookup and this call.
        /// </exception>
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