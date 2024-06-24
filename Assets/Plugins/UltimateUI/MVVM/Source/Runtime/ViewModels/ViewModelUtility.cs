using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    public static class ViewModelUtility
    {
        public static void Bind<T>(T defaultValue, ref Action<T> changed, IReadOnlyCollection<IBinder> binders)
        {
            foreach (var binder in binders)
            {
                switch (binder)
                {
                    case IBinder<T> specificBinder:
                        Bind(defaultValue, specificBinder.SetValue, ref changed);
                        break;
                    
                    case IAnyBinder anyBinder: 
                        Bind(defaultValue, anyBinder.SetValue, ref changed);
                        break;
                    
                    default:
                        BindBySubtype(defaultValue, binder, ref changed);
                        break;
                }
            }
        }
        
        private static void Bind<T>(T defaultValue, Action<T> setValueMethod, ref Action<T> changed)
        {
            setValueMethod(defaultValue);
            changed += setValueMethod;
        }
      
        private static void BindBySubtype<T>(T defaultValue, IBinder binder, ref Action<T> changed)
        {
            var binderType = binder.GetType();
            foreach (var i in binderType.GetInterfaces())
            {
                if (!i.IsGenericType || i.GetGenericTypeDefinition() != typeof(IBinder<>)) continue;
                
                var genericArgument = i.GetGenericArguments()[0];
                if (!typeof(T).IsAssignableFrom(genericArgument)) continue;
                
                var setValueMethod = i.GetMethod("SetValue");
                if (setValueMethod != null)
                {
                    setValueMethod.Invoke(binder, new object[] { defaultValue });
                    
                    // Creating a compatible delegate
                    var actionType = typeof(Action<>).MakeGenericType(typeof(T));
                    var action = Delegate.CreateDelegate(actionType, binder, setValueMethod);
                    
                    if (action is Action<T> typedAction)
                        changed += typedAction;
                }
                break;
            }
        }
        
        public static void Unbind<T>(ref Action<T> changed, IReadOnlyCollection<IBinder> binders)
        {
            foreach (var binder in binders)
            {
                switch (binder)
                {
                    case IBinder<T> specificBinder: changed -= specificBinder.SetValue; break;
                    case IAnyBinder anyBinder: changed -= anyBinder.SetValue; break;
                }
            }
        }
        
        public static bool SetField<T>(ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue)) return false;
            
            field = newValue;
            return true;
        }
        
        public static bool SetField<T>(ref T field, T newValue, IEqualityComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            if (comparer.Equals(field, newValue)) return false;
            
            field = newValue;
            return true;
        }
        
        public static bool SetField<T>(T oldValue, T newValue, Action<T> callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            if (EqualityComparer<T>.Default.Equals(oldValue, newValue)) return false;
            
            callback(newValue);
            return true;
        }
        
        public static bool SetField<T>(T oldValue, T newValue, Action<T> callback, IEqualityComparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            if (comparer.Equals(oldValue, newValue)) return false;
            
            callback(newValue);
            return true;
        }
    }
}