using System;
using UnityEngine;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Utilities;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    public abstract partial class EnumMonoBinder<T> : MonoBinder, IBinder<Enum>
    {
        [Header("Enum")]
        [SerializeField] private EnumValues<T> _enumValues;
        
        [BinderLog]
        public void SetValue(Enum value) =>
            _enumValues.GetValue(value);
        
        protected abstract void SetValue(T value);
    }
    
    public abstract partial class EnumMonoBinder<T, TEnum> : MonoBinder, IBinder<TEnum>
        where TEnum : Enum
    {
        [Header("Enum")]
        [SerializeField] private EnumValues<TEnum, T> _enumValues;
        
        [BinderLog]
        public void SetValue(TEnum value) =>
            _enumValues.GetValue(value);
        
        protected abstract void SetValue(T value);
    }
}