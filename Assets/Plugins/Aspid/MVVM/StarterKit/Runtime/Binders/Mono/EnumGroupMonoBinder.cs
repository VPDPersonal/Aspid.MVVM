using System;
using UnityEngine;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Utilities;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    public abstract partial class EnumGroupMonoBinder<TComponent> : MonoBinder, IBinder<Enum>
    {
        [SerializeField] private EnumValue<TComponent>[] _components;
        
        private bool _isEnumTypeSet;
        
        protected override void OnUnbound() =>
            _isEnumTypeSet = false;
        
        [BinderLog]
        public void SetValue(Enum value)
        {
            if (!_isEnumTypeSet)
            {
                foreach (var component in _components)
                    component.SetType(value.GetType());
                
                _isEnumTypeSet = true;
            }

            foreach (var component in _components)
            {
                if (!component.Key!.Equals(value)) SetDefaultValue(component.Value);
                else SetSelectedValue(component.Value);
            }
        }

        protected abstract void SetDefaultValue(TComponent component);
        
        protected abstract void SetSelectedValue(TComponent component);
    }
}