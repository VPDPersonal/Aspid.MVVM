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
            
            var binderFields = ViewUtility.GetMonoBinderValidableFields(view.GetType()).ToList();
            var ids = binderFields.Select(field => ViewUtility.GetIdName(field.Name)).ToList();
            
            ids.Insert(0, "No Id");
            ids.Insert(1, null);
            return ids.ToArray();
        }
    }
}