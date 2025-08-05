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
}