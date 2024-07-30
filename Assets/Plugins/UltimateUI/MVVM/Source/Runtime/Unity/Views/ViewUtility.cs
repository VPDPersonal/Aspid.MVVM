using System;
using System.Linq;
using System.Reflection;
using UltimateUI.MVVM.Extensions;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Unity.Views
{
    public static class ViewUtility
    {
        private const BindingFlags BindingFlags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic;
  
#if UNITY_EDITOR
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
                
                var name = GetPropertyNameFromFieldName(field.Name);
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
            var bindersOnScene = view.GetComponentsInChildren<MonoBinder>();

            foreach (var field in fields)
            {
                var name = GetPropertyNameFromFieldName(field.Name);
                var binders = bindersOnScene.Where(binder =>
                    binder.View == view && name == binder.Id).ToArray();

                if (field.FieldType.IsArray) field.SetValue(view, binders);
                else field.SetValue(view, binders.First());
            }

            ValidateBinders(view);
        }
#endif

        public static string GetPropertyNameFromFieldName(string name)
        {
            var prefixCount = GetPrefixCount();
            name = name.Remove(0, prefixCount);

            var firstSymbol = name[0];
            if (char.IsLower(firstSymbol))
            {
                name = name.Remove(0, 1);
                name = char.ToUpper(firstSymbol) + name;
            }
        
            return name;
            
            int GetPrefixCount() =>
                name.StartsWith("_") ? 1 : name.StartsWith("m_") ? 2 : 0;
        }
    }
}