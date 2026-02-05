#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class LocalizeStringEventVariableBinder : TargetBinder<LocalizeStringEvent>,
        IBinder<bool>, IBinder<string>, IBinder<Object>, INumberBinder
    {
        [SerializeField] private string _variableName;
        
        public LocalizeStringEventVariableBinder(LocalizeStringEvent target, string variableName, BindMode mode = BindMode.OneWay) 
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _variableName = variableName;
        }

        public void SetValue(bool value)
        {
            GetSpecificVariable<BoolVariable>().Value = value;
            Target.RefreshString();
        }

        public void SetValue(string? value)
        {
            GetSpecificVariable<StringVariable>().Value = value;
            Target.RefreshString();
        }

        public void SetValue(Object? value)
        {
            GetSpecificVariable<ObjectVariable>().Value = value;
            Target.RefreshString();
        }

        public void SetValue(int value)
        {
            GetSpecificVariable<IntVariable>().Value = value;
            Target.RefreshString();
        }
        
        public void SetValue(uint value)
        {
            GetSpecificVariable<UIntVariable>().Value = value;
            Target.RefreshString();
        }

        public void SetValue(long value)
        {
            GetSpecificVariable<LongVariable>().Value = value;
            Target.RefreshString();
        }
        
        public void SetValue(ulong value)
        {
            GetSpecificVariable<ULongVariable>().Value = value;
            Target.RefreshString();
        }
        
        public void SetValue(byte value)
        {
            GetSpecificVariable<ByteVariable>().Value = value;
            Target.RefreshString();
        }
        
        public void SetValue(sbyte value)
        {
            GetSpecificVariable<SByteVariable>().Value = value;
            Target.RefreshString();
        }
        
        public void SetValue(short value)
        {
            GetSpecificVariable<ShortVariable>().Value = value;
            Target.RefreshString();
        }
        
        public void SetValue(ushort value)
        {
            GetSpecificVariable<UShortVariable>().Value = value;
            Target.RefreshString();
        }

        public void SetValue(float value)
        {
            GetSpecificVariable<FloatVariable>().Value = value;
            Target.RefreshString();
        }

        public void SetValue(double value)
        {
            GetSpecificVariable<DoubleVariable>().Value = value;
            Target.RefreshString();
        }

        protected T GetSpecificVariable<T>()
            where T : IVariable, new()
        {
            IVariable? variable = null;
            var stringReference = Target.StringReference;

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