#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using NUnit.Framework;
using UltimateUI.MVVM.Extensions;
using UnityEngine;

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
            var fields = GetMonoBinderFields(type);
            
            foreach (var field in fields)
            {
                var isArray = field.FieldType.IsArray;
                var isRequire = Attribute.IsDefined(field, typeof(RequireBinder));

                var binders = isArray
                    ? (MonoBinder[])field.GetValue(view)
                    : new[] { (MonoBinder)field.GetValue(view) };
                if (binders == null || binders.Length == 0) continue;

                if (isRequire)
                {
                    var requiredTypes = field.GetCustomAttributes(typeof(RequireBinder), false)
                        .Select(attribute => ((RequireBinder)attribute).Type);

                    binders = binders.Where(binder =>
                        {
                            if (!binder) return true;

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
                    if (!binder) continue;
                    
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
            var fields = GetMonoBinderFields(type);
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

        public static void RemoveMonoBinderIfNotExist(MonoView view, MonoBinder binder, string id)
        {
            var field = GetMonoBinderFields(view.GetType())
                .FirstOrDefault(field => GetPropertyName(field.Name) == id);
            
            if (field == null) return;
            
            var isArray = field.FieldType.IsArray;
            var viewBinders = isArray
                ? (MonoBinder[])field.GetValue(view)
                : new[] { (MonoBinder)field.GetValue(view) };
            
            if (viewBinders == null) return;
            
            if (!isArray && viewBinders.Any())
            {
                field.SetValue(view, null);
                return;
            }

            viewBinders = viewBinders.Where(viewBinder => viewBinder.GetInstanceID() != binder.GetInstanceID()).ToArray();
            field.SetValue(view, viewBinders.Any() ? viewBinders : null);
        }
        
        public static void SetMonoBinderIfNotExist(MonoView view, MonoBinder binder, string id)
        {
            var field = GetMonoBinderFields(view.GetType())
                .FirstOrDefault(field => GetPropertyName(field.Name) == id);
            
            if (field == null) return;
            
            var isArray = field.FieldType.IsArray;
            var viewBinders = isArray
                ? (MonoBinder[])field.GetValue(view)
                : new[] { (MonoBinder)field.GetValue(view) };

            if (viewBinders == null)
            {
                if (!isArray) field.SetValue(view, binder);
                else field.SetValue(view, new[] { binder });
                return;
            }
            
            if (viewBinders.Any(viewBinder => viewBinder.GetInstanceID() == binder.GetInstanceID()))
            {
                return;
            }
            
            if (!isArray && viewBinders.Any())
            {
                viewBinders[0].Id = null;
                field.SetValue(view, binder);
                return;
            }
            
            Array.Resize(ref viewBinders, viewBinders.Length + 1);
            viewBinders[^1] = binder;
            field.SetValue(view, viewBinders);
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

        public static IEnumerable<FieldInfo> GetMonoBinderFields(Type type)
        {
            return  type.GetFieldInfosIncludingBaseClasses(BindingFlags).Where(field => 
            {
                var fieldType = field.FieldType;
                return typeof(MonoBinder).IsAssignableFrom(fieldType) 
                    || typeof(MonoBinder[]).IsAssignableFrom(fieldType);
            });
        }
    }
}
#endif