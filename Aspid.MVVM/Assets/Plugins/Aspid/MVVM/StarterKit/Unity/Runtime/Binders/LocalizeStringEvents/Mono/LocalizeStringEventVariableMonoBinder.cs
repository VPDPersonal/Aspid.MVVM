#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using System;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{ 
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder - Variable")]
    [AddPropertyContextMenu(typeof(LocalizeStringEvent), "m_StringReference")]
    [AddComponentContextMenu(typeof(LocalizeStringEvent),"Add LocalizeStringEvent Binder/LocalizeStringEvent Binder - Variable")]
    public partial class LocalizeStringEventVariableMonoBinder : ComponentMonoBinder<LocalizeStringEvent>,
        IBinder<bool>, IBinder<string>, IBinder<Object>, INumberBinder
    {
        [SerializeField] private string _variableName;
        
        [BinderLog]
        public void SetValue(bool value)
        {
            GetSpecificVariable<BoolVariable>().Value = value;
            CachedComponent.RefreshString();
        }

        [BinderLog]
        public void SetValue(string value)
        {
            GetSpecificVariable<StringVariable>().Value = value;
            CachedComponent.RefreshString();
        }

        [BinderLog]
        public void SetValue(Object value)
        {
            GetSpecificVariable<ObjectVariable>().Value = value;
            CachedComponent.RefreshString();
        }

        [BinderLog]
        public void SetValue(int value)
        {
            GetSpecificVariable<IntVariable>().Value = value;
            CachedComponent.RefreshString();
        }
        
        [BinderLog]
        public void SetValue(uint value)
        {
            GetSpecificVariable<UIntVariable>().Value = value;
            CachedComponent.RefreshString();
        }

        [BinderLog]
        public void SetValue(long value)
        {
            GetSpecificVariable<LongVariable>().Value = value;
            CachedComponent.RefreshString();
        }
        
        [BinderLog]
        public void SetValue(ulong value)
        {
            GetSpecificVariable<ULongVariable>().Value = value;
            CachedComponent.RefreshString();
        }
        
        [BinderLog]
        public void SetValue(byte value)
        {
            GetSpecificVariable<ByteVariable>().Value = value;
            CachedComponent.RefreshString();
        }
        
        [BinderLog]
        public void SetValue(sbyte value)
        {
            GetSpecificVariable<SByteVariable>().Value = value;
            CachedComponent.RefreshString();
        }
        
        [BinderLog]
        public void SetValue(short value)
        {
            GetSpecificVariable<ShortVariable>().Value = value;
            CachedComponent.RefreshString();
        }
        
        [BinderLog]
        public void SetValue(ushort value)
        {
            GetSpecificVariable<UShortVariable>().Value = value;
            CachedComponent.RefreshString();
        }

        [BinderLog]
        public void SetValue(float value)
        {
            GetSpecificVariable<FloatVariable>().Value = value;
            CachedComponent.RefreshString();
        }

        [BinderLog]
        public void SetValue(double value)
        {
            GetSpecificVariable<DoubleVariable>().Value = value;
            CachedComponent.RefreshString();
        }

        protected T GetSpecificVariable<T>()
            where T : IVariable, new()
        {
            IVariable variable = null;
            var stringReference = CachedComponent.StringReference;

            if (stringReference?.TryGetValue(_variableName, out variable) ?? false)
            {
                variable = new T();
                stringReference.Add(_variableName, variable);
            }
            
            return variable is T specificVariable 
                ? specificVariable
                : throw new InvalidCastException();
        }
    }
}
#endif