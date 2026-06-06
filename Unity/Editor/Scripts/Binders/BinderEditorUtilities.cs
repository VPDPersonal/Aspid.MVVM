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