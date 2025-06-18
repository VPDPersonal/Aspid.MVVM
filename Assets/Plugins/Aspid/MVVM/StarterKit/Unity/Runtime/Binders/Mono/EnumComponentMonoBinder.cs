using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    public abstract partial class EnumComponentMonoBinder<TComponent, T> : ComponentMonoBinder<TComponent>, IBinder<Enum>
        where TComponent : Component
    {
        [Header("Enum")]
        [SerializeField] private EnumValues<T> _enumValues;
        
        [BinderLog]
        public void SetValue(Enum value) =>
            SetValue(_enumValues.GetValue(value));
        
        protected abstract void SetValue(T value);
        
        protected override void OnUnbound() =>
            _enumValues.Deinitialize();
    }
    
    public abstract partial class EnumComponentMonoBinder<TComponent, TEnum, T> : ComponentMonoBinder<TComponent>, IBinder<TEnum>
        where TEnum : Enum
        where TComponent : Component
    {
        [Header("Enum")]
        [SerializeField] private EnumValues<TEnum, T> _enumValues;
        
        [BinderLog]
        public void SetValue(TEnum value) =>
            SetValue(_enumValues.GetValue(value));
        
        protected abstract void SetValue(T value);
        
        protected override void OnUnbound() =>
            _enumValues.Deinitialize();
    }
}