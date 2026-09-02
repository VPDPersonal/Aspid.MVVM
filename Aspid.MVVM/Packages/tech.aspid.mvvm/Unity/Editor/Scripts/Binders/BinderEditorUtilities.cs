#nullable enable
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Editor utility methods for resolving available binder IDs and parent views for a given <see cref="MonoBinder"/> component.
    /// </summary>
    public static class BinderEditorUtilities
    {
        /// <summary>
        /// Collects every view on the binder's own GameObject and on each of its ancestors.
        /// </summary>
        /// <typeparam name="T">The binder component type.</typeparam>
        /// <param name="binder">The binder whose parent views are wanted.</param>
        /// <returns>The views, nearest first; empty when the hierarchy holds none.</returns>
        public static List<BinderViewData> GetViews<T>(T binder)
            where T : Component, IBinder
        {
            var result = new List<BinderViewData>();
            
            for (var parent = binder.transform; parent; parent = parent.parent)
            {
                var views = parent.GetComponents<IView>();
                result.AddRange(collection: views.Select(view => new BinderViewData(view)));
            }

            return result;
        }
        
        /// <summary>
        /// Collects the ids of the fields on <paramref name="view"/> that this binder could fill.
        /// </summary>
        /// <typeparam name="T">The binder component type.</typeparam>
        /// <param name="binder">The binder to match against the view's fields.</param>
        /// <param name="view">The view whose fields are examined.</param>
        /// <returns>The matching ids; empty when the view has no field this binder fits.</returns>
        /// <remarks>
        /// An array field matches when the binder fits its element type, since the binder would be added to it.
        /// </remarks>
        public static List<BinderIdData> GetIds<T>(T binder, IView view)
            where T : Component, IBinder
        {
            return view
                .GetRequireBinderFields()
                .Where(field =>
                {
                    if (!field.IsBinderMatchRequiredType(binder)) return false;
                    
                    var fieldType = !field.FieldType.IsArray
                        ? field.FieldType
                        : field.FieldType.GetElementType();

                    return fieldType?.IsInstanceOfType(binder) ?? false;
                })
                .Select(field => new BinderIdData(field.Id))
                .ToList();
        }
    }
}