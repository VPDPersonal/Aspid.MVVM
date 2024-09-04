using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Reflection;
using UltimateUI.MVVM.Views;
using System.Collections.Generic;
using UltimateUI.MVVM.Extensions;
using UltimateUI.MVVM.Unity.Views;

namespace UltimateUI.MVVM
{
    public abstract class BinderEditorBase<TBinder> : Editor
        where TBinder : Component, IBinder
    { 
        protected TBinder Binder => (TBinder)target;
        
        protected MonoView[] GetViewList()
        {
            var views = new List<MonoView>(1);
            
            for (var parent = Binder.transform; parent; parent = parent.parent)
            {
                if (parent.TryGetComponent<MonoView>(out var view))
                    views.Add(view);
            }

            return views.ToArray();
        }

        protected string[] GetIdList(IView view)
        {
            if (view == null) return null;
            
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var binderFields = view.GetType().GetMonoBinderFields(bindingFlags).ToList();
            
            var ids = binderFields.Select(field => ViewUtility.GetPropertyName(field.Name)).ToList();
            ids.Insert(0, "None");
            return ids.ToArray();
        }
    }
}