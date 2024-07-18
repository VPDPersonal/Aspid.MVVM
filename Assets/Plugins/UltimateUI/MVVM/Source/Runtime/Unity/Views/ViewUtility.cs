using System;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    public static class ViewUtility
    {
        [Conditional("UNITY_EDITOR")]
        public static void ValidateBinders(IView view)
        {
            var type = view.GetType();
            
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var fields = type.GetFields(bindingFlags).Where(field =>
            {
                var fieldType = field.FieldType;
                return fieldType == typeof(MonoBinder[]);
            });
            
            foreach (var field in fields)
            {
                var binders = (MonoBinder[])field.GetValue(view);
                
                if (Attribute.IsDefined(field, typeof(RequireBinder)))
                {
                    var requiredTypes = field.GetCustomAttributes(typeof(RequireBinder), false).
                        Select(attribute => ((RequireBinder)attribute).Type);

                    binders = binders.Where(binder =>
                    {
                        var interfaces = binder.GetType().GetInterfaces();
                        return interfaces.Any(i =>
                            // i == typeof(IAnyBinder) ||
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IBinder<>) &&
                            requiredTypes.Any(requiredType => requiredType == i.GetGenericArguments()[0])
                        );
                    }).ToArray();
                }
                
#if UNITY_EDITOR
                // foreach (var binder in binders)
                //     binder.Id = field.Name;
#endif
                
                field.SetValue(view, binders);
            }
        }

#if UNITY_EDITOR
        public static void FindAllBinders(IView view, IReadOnlyCollection<MonoBinder> bindersOnScene)
        {
            var type = view.GetType();
            
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic ;
            var fields = GetFieldInfosIncludingBaseClasses(type, bindingFlags).Where(field =>
            {
                var fieldType = field.FieldType;
                return fieldType == typeof(MonoBinder[]);
            });
            
            foreach (var field in fields)
            {
                var binders = bindersOnScene.Where(binderOnScene => field.Name == binderOnScene.Id).ToArray();
                field.SetValue(view, binders);
            }
            
            ValidateBinders(view);
        }
        
        private static FieldInfo[] GetFieldInfosIncludingBaseClasses(Type type, BindingFlags bindingFlags)
        {
            if (type.BaseType == typeof(object)) return type.GetFields(bindingFlags);

            var currentType = type;
            var fieldInfoList = new List<FieldInfo>();
            
            while (currentType != typeof(object))
            {
                if (currentType == null) break;
                
                fieldInfoList.AddRange(currentType.GetFields(bindingFlags));
                currentType = currentType.BaseType;
            }
            
            return fieldInfoList.ToArray();
        }
#endif
    }
}