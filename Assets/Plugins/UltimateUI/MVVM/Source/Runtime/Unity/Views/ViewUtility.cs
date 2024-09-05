#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UltimateUI.MVVM.Extensions;

namespace UltimateUI.MVVM.Unity.Views
{
    public static class ViewUtility
    {
        private const BindingFlags BindingFlags =
            System.Reflection.BindingFlags.Instance
            | System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.NonPublic;

        public static void ValidateMonoBinders(MonoView view)
        {
            var type = view.GetType();
            var fields = GetMonoBinderFields(type, BindingFlags);

            foreach (var field in fields)
            {
                var isArray = field.FieldType.IsArray;
                var isRequire = Attribute.IsDefined(field, typeof(RequireBinder));

                var binders = isArray
                    ? (MonoBinder[])field.GetValue(view)
                    : new[] { (MonoBinder)field.GetValue(view) };

                if (isRequire)
                {
                    var requiredTypes = field.GetCustomAttributes(typeof(RequireBinder), false)
                        .Select(attribute => ((RequireBinder)attribute).Type);

                    binders = binders.Where(binder => 
                        {
                            var interfaces = binder.GetType()
                                .GetInterfaces();

                            return interfaces.Any(i =>
                                i.IsGenericType
                                && i.GetGenericTypeDefinition() == typeof(IBinder<>)
                                && requiredTypes.Any(requiredType =>
                                    requiredType == i.GetGenericArguments()[0]));
                        })
                        .ToArray();
                }

                var name = GetPropertyName(field.Name);
                foreach (var binder in binders)
                {
                    binder.Id = name;
                    binder.View = view;
                }

                if (binders.Length == 0) field.SetValue(view, null);
                else field.SetValue(view, isArray ? binders : binders[0]);
            }
        }
        
        public static void FindAllBindersInChildren(MonoView view)
        {
            var type = view.GetType();
            var fields = GetMonoBinderFields(type, BindingFlags);
            var bindersOnScene = view.GetComponentsInChildren<MonoBinder>()
                .Where(binder => binder.View == view).ToArray();
            
            foreach (var field in fields)
            {
                var name = GetPropertyName(field.Name);
                var binders = bindersOnScene.Where(binder => 
                    name == binder.Id);
        
                if (field.FieldType.IsArray) field.SetValue(view, binders.ToArray());
                else field.SetValue(view, binders.FirstOrDefault());
            }
        
            ValidateMonoBinders(view);
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

        private static IEnumerable<FieldInfo> GetMonoBinderFields(Type type, BindingFlags bindingFlags)
        {
            return  type.GetFieldInfosIncludingBaseClasses(bindingFlags).Where(field => 
            {
                var fieldType = field.FieldType;
                return typeof(MonoBinder).IsAssignableFrom(fieldType) 
                    || typeof(MonoBinder[]).IsAssignableFrom(fieldType);
            });
        }
    }
}
#endif