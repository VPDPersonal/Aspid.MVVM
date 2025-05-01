using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Aspid.MVVM.Unity
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
            if (view is null) return new List<string>();
            
            var fields = view.GetMonoBinderValidableFields();
            
            var ids = fields
                .Where(field =>
                {
                    var requiredTypes = field.GetRequiredTypes().ToArray();

                    if (requiredTypes.Any())
                        return requiredTypes.IsBinderMatchRequiredType(Binder);
            
                    var fieldType = !field.FieldType.IsArray
                        ? field.FieldType
                        : field.FieldType.GetElementType();
                    
                    return fieldType?.IsAssignableFrom(Binder.GetType()) ?? false;
                })
                .Select(field => field.GetBinderId())
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