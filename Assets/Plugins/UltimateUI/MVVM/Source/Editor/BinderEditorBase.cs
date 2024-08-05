using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.Extensions;
using System.Collections.Generic;
using UltimateUI.MVVM.Unity.Views;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Unity
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
            
            return binderFields.Select(field => ViewUtility.GetPropertyNameFromFieldName(field.Name)).ToArray();
        }
    }
}