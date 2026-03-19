#if !ASPID_MVVM_EDITOR_DISABLED
using System.Linq;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Immutable data structure carrying the selected index and available choices for a dropdown field
    /// in the <see cref="MonoBinderVisualElement"/> inspector.
    /// </summary>
    public readonly struct BinderDropdownData
    {
        public readonly int Index;
        public readonly bool HasPrevious;
        public readonly List<string> Choices;

        private BinderDropdownData(List<string> choices, int index, bool hasPrevious = false)
        {
            Index = index;
            Choices = choices;
            HasPrevious = hasPrevious;
        }

        public bool Contains(string value) =>
            !string.IsNullOrWhiteSpace(value)
            && Choices.Contains(value);

        public static BinderDropdownData CreateIdDropdownData(MonoBinderEditor editor)
        {
            const string noneValue = "No Id";

            var view = editor.ViewProperty.Value;
            var id = editor.IdProperty.Value;

            if (view is null)
            {
                var choices = new List<string> { noneValue };
                var hasPrevious = !string.IsNullOrWhiteSpace(editor.IdProperty.PreviousValue);
                return new BinderDropdownData(choices, index: 0, hasPrevious);
            }
            else
            {
                var choices = BinderEditorUtilities
                    .GetIds(editor.TargetAsMonoBinder, view)
                    .Select(data => data.Id)
                    .ToList();

                choices.Insert(index: 0, null);
                choices.Insert(index: 0, noneValue);

                if (!string.IsNullOrWhiteSpace(id))
                    return new BinderDropdownData(choices, choices.IndexOf(id));

                var previousId = editor.IdProperty.PreviousValue;
                var hasPrevious = !string.IsNullOrWhiteSpace(previousId);
                return new BinderDropdownData(choices, index: 0, hasPrevious);
            }
        }

        public static BinderDropdownData CreateViewDropdownData(MonoBinderEditor editor)
        {
            const string noneValue = "No View";

            var views = BinderEditorUtilities.GetViews(editor.TargetAsMonoBinder);
            var viewName = BinderViewData.GetViewName(editor.ViewProperty.Value as MonoView);
            var hasPrevious = !string.IsNullOrWhiteSpace(editor.ViewProperty.PreviousName);

            if (views.Count is 0)
                return new BinderDropdownData(choices: new List<string> { noneValue }, index: 0, hasPrevious);

            var choices = views
                .Select(view => view.Name)
                .ToList();

            choices.Insert(index: 0, null);
            choices.Insert(index: 0, noneValue);

            if (!string.IsNullOrWhiteSpace(viewName))
                return new BinderDropdownData(choices, index: choices.IndexOf(viewName));
            
            return new BinderDropdownData(choices, index: 0, hasPrevious);
        }
    }
}
#endif