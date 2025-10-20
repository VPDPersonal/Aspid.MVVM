#nullable enable
using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static class BinderEditorUtilities
    {
        public static List<string> GetIds<T>(T binder, IView view)
            where T : Component, IBinder
        {
            return view.GetMonoBinderValidableFields()
                .Where(field =>
                {
                    var requiredTypes = field.GetRequiredTypes();
                    if (!requiredTypes.IsBinderMatchRequiredType(binder)) return false;
                    
                    var fieldType = !field.FieldType.IsArray
                        ? field.FieldType
                        : field.FieldType.GetElementType();

                    return fieldType?.IsInstanceOfType(binder) ?? false;
                })
                .Select(field => field.GetBinderId())
                .ToList();
        }
        
        public static List<(string name, IView view)> GetViews<T>(T binder)
            where T : Component, IBinder
        {
            var result = new List<(string name, IView view)>();
            
            for (var parent = binder.transform; parent; parent = parent.parent)
            {
                var views = parent.GetComponents<IView>();
                result.AddRange(views.Select(view => (GetViewName((Component)view), view)));
            }

            return result;
        }
        
        public static string GetViewName(Component? view)
        {
            if (!view) return string.Empty;
            if (view is not IView) throw new InvalidCastException("View is not IView");
            
            var type = view!.GetType();
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
            
            throw new Exception();
        }
    }
}