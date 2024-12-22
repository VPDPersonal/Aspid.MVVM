using System;
using UnityEngine;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Utilities;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    public abstract partial class EnumGroupMonoBinder<TElement> : MonoBinder, IBinder<Enum>
    {
        [Header("Enum")]
        [SerializeField] private EnumValues<TElement> _enumValues;
        
        private bool _initialized;
        
        [BinderLog]
        public void SetValue(Enum value)
        {
            Initialize(value);
            
            foreach (var enumValue in _enumValues)
            {
                if (enumValue.Key is null)
                    throw new NullReferenceException("Key is null");
                
                if (!enumValue.Key.Equals(value)) SetDefaultValue(enumValue.Value);
                else SetSelectedValue(enumValue.Value);
            }
        }

        protected override void OnUnbound() =>
            _initialized = false;

        protected abstract void SetDefaultValue(TElement element);
        
        protected abstract void SetSelectedValue(TElement element);

        private void Initialize(Enum value)
        {
            if (_initialized) return;

            foreach (var enumValue in _enumValues)
                enumValue.Initialize(value.GetType());
            
            _initialized = true;
        }
    }
    
    public abstract partial class EnumGroupMonoBinder<TEnum, TElement> : MonoBinder, IBinder<TEnum> 
        where TEnum : Enum
    {
        [Header("Enum")]
        [SerializeField] private EnumValues<TEnum, TElement> _enumValues;
        
        [BinderLog]
        public void SetValue(TEnum value)
        {
            foreach (var enumValue in _enumValues)
            {
                if (enumValue.Key is null)
                    throw new NullReferenceException("Key is null");
                
                if (enumValue.Key is not null && !enumValue.Key.Equals(value)) SetDefaultValue(enumValue.Value);
                else SetSelectedValue(enumValue.Value);
            }
        }

        protected abstract void SetDefaultValue(TElement element);
        
        protected abstract void SetSelectedValue(TElement element);
    }
}