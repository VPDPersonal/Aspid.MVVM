using System;
using UnityEngine;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Utilities;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    public abstract partial class EnumMonoBinder<T> : MonoBinder, IBinder<Enum>
    {
        [SerializeField] private T _defaultValue;
        [SerializeField] private bool _allowDefaultValueWhenNoValue;
        [SerializeField] private EnumValue<T>[] _values;
        
        private bool _isEnumTypeSet;
        
        protected override void OnUnbound() =>
            _isEnumTypeSet = false;
        
        [BinderLog]
        public void SetValue(Enum enumValue)
        {
            if (!_isEnumTypeSet)
            {
                foreach (var value in _values)
                    value.SetType(enumValue.GetType());
                
                _isEnumTypeSet = true;
            }

            foreach (var value in _values)
            {
                if (!value.Key!.Equals(enumValue)) continue;
                
                SetValue(value.Value);
                return;
            }

            if (_allowDefaultValueWhenNoValue) 
                SetValue(_defaultValue);
        }

        protected abstract void SetValue(T value);
    }
    
    public abstract partial class EnumMonoBinder<TComponent, T> : ComponentMonoBinder<TComponent>, IBinder<Enum>
        where TComponent : Component
    {
        [Header("Parameters")]
        [SerializeField] private T _defaultValue;
        [SerializeField] private bool _allowDefaultValueWhenNoValue;
        [SerializeField] private EnumValue<T>[] _values;
        
        private bool _isEnumTypeSet;
        
        protected override void OnUnbound() =>
            _isEnumTypeSet = false;
        
        [BinderLog]
        public void SetValue(Enum enumValue)
        {
            if (!_isEnumTypeSet)
            {
                foreach (var value in _values)
                    value.SetType(enumValue.GetType());
                
                _isEnumTypeSet = true;
            }

            foreach (var value in _values)
            {
                if (!value.Key!.Equals(enumValue)) continue;
                
                SetValue(value.Value);
                return;
            }

            if (_allowDefaultValueWhenNoValue) 
                SetValue(_defaultValue);
        }

        protected abstract void SetValue(T value);
    }
}