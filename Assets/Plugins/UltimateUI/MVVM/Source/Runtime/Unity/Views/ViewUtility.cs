#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UltimateUI.MVVM.Extensions;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Unity.Views
{
    public static class ViewUtility
    {
        private const BindingFlags BindingFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic;
        
        public static void ValidateBinders(MonoView view)
        {
            var type = view.GetType();
            var fields = type.GetMonoBinderFields(BindingFlags);
            
            foreach (var field in fields)
            {
                // TODO support single binder
                var binders = (MonoBinder[])field.GetValue(view);
                
                if (Attribute.IsDefined(field, typeof(RequireBinder)))
                {
                    
                    var requiredTypes = field.GetCustomAttributes(typeof(RequireBinder), false).
                        Select(attribute => ((RequireBinder)attribute).Type);

                    binders = binders.Where(binder =>
                    {
                        var interfaces = binder.GetType().GetInterfaces();
                        return interfaces.Any(i =>
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IBinder<>) &&
                            requiredTypes.Any(requiredType => requiredType == i.GetGenericArguments()[0])
                        );
                    }).ToArray();
                }
                
                var name = GetPropertyName(field.Name);
                foreach (var binder in binders)
                {
                    binder.Id = name;
                    binder.View = view;
                }
                
                field.SetValue(view, binders);
            }
        }
        
        public static void FindAllBindersInChildren(MonoView view)
        {
            var type = view.GetType();
            var fields = type.GetMonoBinderFields(BindingFlags);
            var bindersOnScene = view.GetComponentsInChildren<MonoBinder>()
                .Where(binder => binder.View == view).ToArray();
            
            foreach (var field in fields)
            {
                var name = GetPropertyName(field.Name);
                var binders = bindersOnScene.Where(binder => 
                    name == binder.Id);

                if (field.FieldType.IsArray) field.SetValue(view, binders.ToArray());
                else field.SetValue(view, binders.First());
            }

            ValidateBinders(view);
        }

        public static string GetPropertyName(string fieldName)
        {
            var prefixCount = GetPrefixCount();
            fieldName = fieldName.Remove(0, prefixCount);

            var firstSymbol = fieldName[0];
            if (char.IsLower(firstSymbol))
            {
                fieldName = fieldName.Remove(0, 1);
                fieldName = char.ToUpper(firstSymbol) + fieldName;
            }
        
            return fieldName;
            
            int GetPrefixCount() =>
                fieldName.StartsWith("_") ? 1 : fieldName.StartsWith("m_") ? 2 : 0;
        }

        private static MonoBinder[]? ValidateBindersByType(FieldInfo binderField, params MonoBinder[]? binders)
        {
            var requiredTypes = GetRequiredTypes(binderField);
            
            return binders?.Where(monoBinder =>
            {
                var interfaces = monoBinder.GetType().GetInterfaces();
                                
                return interfaces.Any(@interface =>
                    @interface.IsGenericType
                    && @interface.GetGenericTypeDefinition() == typeof(IBinder<>) 
                    && requiredTypes.Any(requiredType => requiredType == @interface.GetGenericArguments()[0]));
            }).ToArray();
        }

        private static MonoBinder[] ValidateBindersById(MonoView view, FieldInfo binderField, params MonoBinder[] binders)
        {
            var propertyName = GetPropertyName(binderField.Name);

            foreach (var binder in binders)
            {
                binder.View = view;
                binder.Id = propertyName;
            }

            return binders;
        }
        
        private static IEnumerable<Type> GetRequiredTypes(FieldInfo binderField) => 
            binderField.GetCustomAttributes(typeof(RequireBinder), false).
                Select(attribute => ((RequireBinder)attribute).Type);
    }
}
#endif