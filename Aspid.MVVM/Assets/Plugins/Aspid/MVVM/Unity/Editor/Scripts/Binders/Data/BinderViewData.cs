#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public readonly struct BinderViewData
    {
        public readonly IView View;
        public readonly string Name;

        public BinderViewData(IView view)
        {
            View = view;
            Name = GetViewName(view as Component);
        }
        
        public static string GetViewName(Component? view)
        {
            if (!view) return string.Empty;
            if (view is not IView) throw new InvalidCastException("View is not IView");
            
            var type = view.GetType();
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