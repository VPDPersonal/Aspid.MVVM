using System;
using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    public abstract partial class EnumGroupMonoBinder<TElement> : MonoBinder, IBinder<Enum>
    {
        [Header("Enum")]
        [SerializeField] private EnumValues<TElement> _enumValues;
        
        private bool _initialized;
        
        [BinderLog]
        public void SetValue(Enum value)
        {
            _enumValues.Initialize(value);
            
            foreach (var enumValue in _enumValues)
            {
                if (enumValue.Key is null)
                    throw new NullReferenceException("Key is null");
                
                if (!_enumValues.Equals(value, enumValue.Key)) SetDefaultValue(enumValue.Value);
                else SetSelectedValue(enumValue.Value);
            }
        }

        protected abstract void SetDefaultValue(TElement element);
        
        protected abstract void SetSelectedValue(TElement element);
        
        protected override void OnUnbound() =>
            _enumValues.Deinitialize();
    }
    
    public abstract partial class EnumGroupMonoBinder<TEnum, TElement> : MonoBinder, IBinder<TEnum> 
        where TEnum : Enum
    {
        [Header("Enum")]
        [SerializeField] private EnumValues<TEnum, TElement> _enumValues;
        
        [BinderLog]
        public void SetValue(TEnum value)
        {
            _enumValues.Initialize(value);
            
            foreach (var enumValue in _enumValues)
            {
                if (enumValue.Key is null)
                    throw new NullReferenceException("Key is null");
                
                if (!_enumValues.Equals(value, enumValue.Key)) SetDefaultValue(enumValue.Value);
                else SetSelectedValue(enumValue.Value);
            }
        }

        protected abstract void SetDefaultValue(TElement element);
        
        protected abstract void SetSelectedValue(TElement element);
        
        protected override void OnUnbound() =>
            _enumValues.Deinitialize();
    }
}