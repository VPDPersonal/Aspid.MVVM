#nullable enable
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides utility methods for binder View and identifier choices.
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
        /// Collects identifiers of View fields that accept the binder, including array element slots.
        /// </summary>
        /// <typeparam name="T">The binder component type.</typeparam>
        /// <param name="binder">The binder to match.</param>
        /// <param name="view">The View whose fields are inspected.</param>
        /// <returns>The matching identifiers; an empty list when none match.</returns>
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