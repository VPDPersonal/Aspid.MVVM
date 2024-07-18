using System;
using System.ComponentModel;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public readonly ref struct BindData<T>
    {
        public readonly bool IsRead;
        public readonly bool IsWrite;
        
        public readonly Func<T>? Getter;
        public readonly Action<T>? Setter;
        public readonly INotifyPropertyChanged? Notify;

        private BindData(Action<T> setter)
        {
            IsRead = false;
            IsWrite = true;

            Setter = setter;
            Getter = null;
            Notify = null;
        }
        
        private BindData(Func<T> getter, INotifyPropertyChanged? notify = null)
        {
            IsRead = true;
            IsWrite = false;

            Setter = null;
            Getter = getter;
            Notify = notify;
        }
        
        private BindData(Func<T> getter, Action<T> setter, INotifyPropertyChanged? notify = null)
        {
            IsRead = true;
            IsWrite = true;

            Getter = getter;
            Setter = setter;
            Notify = notify;
        }
        
        public static BindData<T> WriteOnly(Action<T> setter) => 
            new(setter);

        public static BindData<T> ReadOnly(Func<T> getter, INotifyPropertyChanged? notify = null) => 
            new(getter, notify);

        public static BindData<T> FullAccess(Func<T> getter, Action<T> setter, INotifyPropertyChanged? notify = null) =>
            new(getter, setter, notify);
    }
}