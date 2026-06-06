#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using System;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{ 
    /// <summary>
    /// <see cref="ComponentMonoBinder{LocalizeStringEvent}"/> that updates a named Smart String variable
    /// on a <see cref="LocalizeStringEvent"/> and calls <see cref="LocalizeStringEvent.RefreshString"/>
    /// when the bound ViewModel value changes.
    /// Supports numeric, boolean, string, and <see cref="Object"/> value types via
    /// <see cref="INumberBinder"/> and additional <see cref="IBinder{T}"/> implementations.
    /// </summary>
    [AddBinderContextMenu(typeof(LocalizeStringEvent), serializePropertyNames: "m_StringReference")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Variable")]
    public partial class LocalizeStringEventVariableMonoBinder : ComponentMonoBinder<LocalizeStringEvent>,
        INumberBinder,
        IBinder<bool>, 
        IBinder<string>,
        IBinder<Object>
    {
        [SerializeField] private string _variableName;
        
        /// <summary>
        /// Updates the named <see cref="BoolVariable"/> to the specified value and calls <see cref="LocalizeStringEvent.RefreshString"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(bool value)
        {
            GetSpecificVariable<BoolVariable>().Value = value;
            CachedComponent.RefreshString();
        }

        /// <summary>
        /// Updates the named <see cref="StringVariable"/> to the specified value and calls <see cref="LocalizeStringEvent.RefreshString"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(string value)
        {
            GetSpecificVariable<StringVariable>().Value = value;
            CachedComponent.RefreshString();
        }

        /// <summary>
        /// Updates the named <see cref="ObjectVariable"/> to the specified value and calls <see cref="LocalizeStringEvent.RefreshString"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(Object value)
        {
            GetSpecificVariable<ObjectVariable>().Value = value;
            CachedComponent.RefreshString();
        }

        /// <summary>
        /// Updates the named <see cref="IntVariable"/> to the specified value and calls <see cref="LocalizeStringEvent.RefreshString"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(int value)
        {
            GetSpecificVariable<IntVariable>().Value = value;
            CachedComponent.RefreshString();
        }
        
        /// <summary>
        /// Updates the named <see cref="UIntVariable"/> to the specified value and calls <see cref="LocalizeStringEvent.RefreshString"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(uint value)
        {
            GetSpecificVariable<UIntVariable>().Value = value;
            CachedComponent.RefreshString();
        }

        /// <summary>
        /// Updates the named <see cref="LongVariable"/> to the specified value and calls <see cref="LocalizeStringEvent.RefreshString"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(long value)
        {
            GetSpecificVariable<LongVariable>().Value = value;
            CachedComponent.RefreshString();
        }
        
        /// <summary>
        /// Updates the named <see cref="ULongVariable"/> to the specified value and calls <see cref="LocalizeStringEvent.RefreshString"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(ulong value)
        {
            GetSpecificVariable<ULongVariable>().Value = value;
            CachedComponent.RefreshString();
        }
        
        /// <summary>
        /// Updates the named <see cref="ByteVariable"/> to the specified value and calls <see cref="LocalizeStringEvent.RefreshString"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(byte value)
        {
            GetSpecificVariable<ByteVariable>().Value = value;
            CachedComponent.RefreshString();
        }
        
        /// <summary>
        /// Updates the named <see cref="SByteVariable"/> to the specified value and calls <see cref="LocalizeStringEvent.RefreshString"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(sbyte value)
        {
            GetSpecificVariable<SByteVariable>().Value = value;
            CachedComponent.RefreshString();
        }
        
        /// <summary>
        /// Updates the named <see cref="ShortVariable"/> to the specified value and calls <see cref="LocalizeStringEvent.RefreshString"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(short value)
        {
            GetSpecificVariable<ShortVariable>().Value = value;
            CachedComponent.RefreshString();
        }
        
        /// <summary>
        /// Updates the named <see cref="UShortVariable"/> to the specified value and calls <see cref="LocalizeStringEvent.RefreshString"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(ushort value)
        {
            GetSpecificVariable<UShortVariable>().Value = value;
            CachedComponent.RefreshString();
        }

        /// <summary>
        /// Updates the named <see cref="FloatVariable"/> to the specified value and calls <see cref="LocalizeStringEvent.RefreshString"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(float value)
        {
            GetSpecificVariable<FloatVariable>().Value = value;
            CachedComponent.RefreshString();
        }

        /// <summary>
        /// Updates the named <see cref="DoubleVariable"/> to the specified value and calls <see cref="LocalizeStringEvent.RefreshString"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(double value)
        {
            GetSpecificVariable<DoubleVariable>().Value = value;
            CachedComponent.RefreshString();
        }

        /// <summary>
        /// Returns the named variable of type <typeparamref name="T"/> from the component's string reference,
        /// adding a new instance under <see cref="_variableName"/> if no variable with that name exists yet.
        /// </summary>
        protected T GetSpecificVariable<T>()
            where T : IVariable, new()
        {
            IVariable variable = null;
            var stringReference = CachedComponent.StringReference;

            if (!(stringReference?.TryGetValue(_variableName, out variable) ?? false))
            {
                variable = new T();
                stringReference?.Add(_variableName, variable);
            }
            
            return variable is T specificVariable 
                ? specificVariable
                : throw new InvalidCastException();
        }
    }
}
#endif