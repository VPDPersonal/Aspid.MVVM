using System;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;

namespace Aspid.MVVM
{
    public static class ViewModelEditorExtensions
    {
        /// <summary>
        /// Forcibly invokes all Changed events (e.g., "valueChangedEvent") for fields marked with 
        /// <see cref="BindAttribute"/> in the Unity editor.
        /// </summary>
        /// <param name="viewModel">The ViewModel in which to trigger events.</param>
        /// <remarks>
        /// This method only works in the Unity editor (compilation condition: UNITY_EDITOR).
        /// Uses reflection to find fields with the <see cref="BindAttribute"/> and their corresponding events.
        /// Useful for testing/debugging data bindings in the editor.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="viewModel"/> is null.</exception>
#if UNITY_2022_1_OR_NEWER
        [Conditional("UNITY_EDITOR")]
#else
        [Conditional("DEBUG")]
#endif
        public static void InvokeAllChangedEventsDebug(this IViewModel viewModel)
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            var bindFields = new List<(Type type, FieldInfo field)>();
            for (var type = viewModel.GetType(); type is not null; type = type.BaseType)
            {
                var fields = type.GetFields(bindingFlags);
                bindFields.AddRange(fields.Where(field =>
                {
                    if (field.IsInitOnly) return false;

                    foreach (var attribute in field.GetCustomAttributes<BindAttribute>())
                    {
                        if (attribute.Mode is BindMode.TwoWay or BindMode.OneWay)
                            return true;
                    }
                    
                    if (field.GetCustomAttributes<OneWayBindAttribute>().Any()) return true;
                    if (field.GetCustomAttributes<TwoWayBindAttribute>().Any()) return true;
                    
                    return false;
                }).Select(field => (type, field)));
                
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