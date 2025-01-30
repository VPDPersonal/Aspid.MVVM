using System;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using Aspid.MVVM.Generation;
using System.Collections.Generic;

namespace Aspid.MVVM
{
    public static class ViewModelEditorExtensions
    {
        [Conditional("UNITY_EDITOR")]
        public static void InvokeAllChangedEventsEditor(this IViewModel viewModel)
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            var bindFields = new List<(Type type, FieldInfo field)>();
            for (var type = viewModel.GetType(); type is not null; type = type.BaseType)
            {
                bindFields.AddRange(type.GetFields(bindingFlags)
                    .Where(field => field.GetCustomAttributes<BindAttribute>().Any()).Select(field => (type, field)));
            }

            foreach (var bindField in bindFields)
            {
                var bindFieldName = bindField.field.GetBinderId();
                var firstCharBindFieldName = char.ToLower(bindFieldName.First());
                bindFieldName = firstCharBindFieldName + bindFieldName.Remove(0, 1);
                
                var eventFieldName = $"__{bindFieldName}ChangedEvent";
                var eventField = bindField.type.GetField(eventFieldName, bindingFlags);

                var eventInstance = eventField?.GetValue(viewModel);
                if (eventInstance is null) continue;
                
                var eventInvokeMethod = eventField?.FieldType.GetMethod("Invoke");
                eventInvokeMethod?.Invoke(eventInstance, new[] { bindField.field.GetValue(viewModel) });
            }
        }
    }
}