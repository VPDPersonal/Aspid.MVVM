using System.Linq;
using UnityEngine;
using System.Reflection;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.Unity.Views;
using System.Collections.Generic;

namespace Aspid.UI.MVVM.Unity
{
    public abstract class BinderEditorBase<TBinder> : UnityEditor.Editor
        where TBinder : Component, IBinder
    { 
        protected TBinder Binder => (TBinder)target;
        
        protected List<MonoView> GetViewList()
        {
            var views = new List<MonoView>(1);
            
            for (var parent = Binder.transform; parent; parent = parent.parent)
            {
                if (parent.TryGetComponent<MonoView>(out var view))
                    views.Add(view);
            }

            return views;
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
                            if (@interface.GetGenericTypeDefinition() != typeof(IBinder<>)) return false;
                            
                            return @interface.GetGenericArguments()[0].IsAssignableFrom(type);
                        });
                    }
                    
                    return Binder.GetType().IsAssignableFrom(field.FieldType);
                })
                .Select(field => ViewUtility.GetIdName(field.Name))
                .ToList();

            return ids;
        }
    }
}