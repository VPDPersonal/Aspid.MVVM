using System.Linq;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

namespace Aspid.MVVM.Mono
{
    public abstract class BinderEditorBase<TBinder> : UnityEditor.Editor
        where TBinder : Component, IBinder
    { 
        protected TBinder Binder => (TBinder)target;
        
        protected List<(string name, MonoView view)> GetViewList()
        {
            var viewList = new List<(string name, MonoView view)>();
            
            for (var parent = Binder.transform; parent; parent = parent.parent)
            {
	            var views = parent.GetComponents<MonoView>();

	            foreach (var view in views)
	            {
		            var viewName = GetViewName(view);
		            viewList.Add((viewName, view));
	            }
            }

            return viewList;
        }

        protected List<string> GetIdList(IView view)
        {
            if (view == null) return new List<string>();
            
            var binderFields = ViewUtility.GetMonoBinderValidableFields(view.GetType()).ToList();
            
            var ids = binderFields
                .Where(field =>
                {
                    if (field.GetCustomAttribute<RequireBinderAttribute>() is { } attribute)
                    {
                        var type = attribute.Type;
                        return Binder.GetType().GetInterfaces().Any(@interface =>
                        {
                            if (!@interface.IsGenericType) return false;
                            if (@interface.GetGenericTypeDefinition() != typeof(IBinder<>) 
                                && @interface.GetGenericTypeDefinition() != typeof(IReverseBinder<>)) return false;
                            
                            return @interface.GetGenericArguments()[0].IsAssignableFrom(type);
                        });
                    }
                    
                    return Binder.GetType().IsAssignableFrom(field.FieldType);
                })
                .Select(field => ViewUtility.GetIdName(field.Name))
                .ToList();

            return ids;
        }

        protected static string GetViewName<T>(T view)
			where T : Component, IView
        {
	        if (view is null) return null;
	        
	        var type = view.GetType();
	        var typeName = type.Name;
	        var views = view.GetComponents(type);
	        
	        switch (views.Length)
	        {
		        case 0: return null;
		        case 1: return $"{views[0].name} ({typeName})";
		        default:
			        {
				        var index = 0;
	        
				        foreach (var component in views)
				        {
					        if (component.GetType() != type) continue;
		        
					        index++;
					        if (component == view) return $"{view.name} ({typeName} ({index}))";
				        }
				        
				        return null;
			        }
	        }
        }
    }
}