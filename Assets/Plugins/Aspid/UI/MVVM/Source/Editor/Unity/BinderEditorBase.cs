using System.Linq;
using UnityEngine;
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