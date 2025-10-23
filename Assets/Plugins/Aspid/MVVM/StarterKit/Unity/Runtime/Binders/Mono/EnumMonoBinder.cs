using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public abstract partial class EnumMonoBinder<T> : MonoBinder, IBinder<Enum>
    {
        [SerializeField] private EnumValues<T> _enumValues;
        
        [BinderLog]
        public void SetValue(Enum value) =>
            SetValue(_enumValues.GetValue(value));
        
        protected abstract void SetValue(T value);
        
        protected override void OnUnbound() =>
            _enumValues.Deinitialize();
    }
    
    public abstract partial class EnumMonoBinder<TComponent, T> : ComponentMonoBinder<TComponent>, IBinder<Enum>
        where TComponent : Component
    {
        [SerializeField] private EnumValues<T> _enumValues;
        
        [BinderLog]
        public void SetValue(Enum value) =>
            SetValue(_enumValues.GetValue(value));
        
        protected abstract void SetValue(T value);
        
        protected override void OnUnbound() =>
            _enumValues.Deinitialize();
    }
}