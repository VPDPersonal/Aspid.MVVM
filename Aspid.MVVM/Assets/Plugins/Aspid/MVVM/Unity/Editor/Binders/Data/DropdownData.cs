using System.Linq;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public readonly struct DropdownData
    {
        public readonly int Index;
        public readonly List<string> Choices;

        public DropdownData(List<string> choices, int index)
        {
            Index = index;
            Choices = choices;
        }

        public static DropdownData CreateIdDropdownData(MonoBinderEditor editor)
        {
            const string noneValue = "No Id";
            
            var id = editor.IdProperty.stringValue;
            var view = editor.ViewProperty.objectReferenceValue;
            
            if (!view) return new DropdownData(new List<string> { noneValue }, 0);

            var choices = BinderEditorUtilities.GetIds(editor.TargetAsMonoBinder, (IView)view);
            choices.Insert(0, null);
            choices.Insert(0, noneValue);
                
            return string.IsNullOrWhiteSpace(id)
                ? new DropdownData(choices, 0)
                : new DropdownData(choices, choices.IndexOf(id));
        }
        
        public static DropdownData CreateViewDropdownData(MonoBinderEditor editor)
        {
            const string noneValue = "No View";

            var views = BinderEditorUtilities.GetViews(editor.TargetAsMonoBinder);
            // TODO Aspid remove MonoView
            var viewName = BinderEditorUtilities.GetViewName(editor.ViewProperty.objectReferenceValue as MonoView);
            
            if (views.Count is 0) return new DropdownData(new List<string> { noneValue }, 0);
            
            var choices = views.Select(view => view.name).ToList();
            choices.Insert(0, null);
            choices.Insert(0, noneValue);
                
            return string.IsNullOrWhiteSpace(viewName)
                ? new DropdownData(choices, 0)
                : new DropdownData(choices, choices.IndexOf(viewName));
        }
    }
}