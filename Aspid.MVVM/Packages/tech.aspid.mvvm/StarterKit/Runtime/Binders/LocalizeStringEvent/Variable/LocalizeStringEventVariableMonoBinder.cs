#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that writes the bound value into a named Smart String variable of
    /// a <see cref="LocalizeStringEvent"/> and refreshes the string.
    /// </summary>
    /// <remarks>
    /// A missing variable is created with the type of the bound value. A variable of another type is reported and
    /// left unchanged.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(LocalizeStringEvent), serializePropertyNames: "m_StringReference")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Variable")]
    public partial class LocalizeStringEventVariableMonoBinder : ComponentMonoBinder<LocalizeStringEvent>,
        INumberBinder,
        IBinder<bool>,
        IBinder<string>,
        IBinder<Object>
    {
        [Tooltip("Local variable of the string reference that receives the value.")]
        [SerializeField] private string _variableName;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(bool value) =>
            Set<BoolVariable, bool>(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(string value) =>
            Set<StringVariable, string>(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(Object value) =>
            Set<ObjectVariable, Object>(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(int value) =>
            Set<IntVariable, int>(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(uint value) =>
            Set<UIntVariable, uint>(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(long value) =>
            Set<LongVariable, long>(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(ulong value) =>
            Set<ULongVariable, ulong>(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(byte value) =>
            Set<ByteVariable, byte>(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(sbyte value) =>
            Set<SByteVariable, sbyte>(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(short value) =>
            Set<ShortVariable, short>(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(ushort value) =>
            Set<UShortVariable, ushort>(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(float value) =>
            Set<FloatVariable, float>(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(double value) =>
            Set<DoubleVariable, double>(value);

        private void Set<TVariable, T>(T value)
            where TVariable : Variable<T>, new()
        {
            if (string.IsNullOrWhiteSpace(_variableName))
            {
                this.LogError(
                    problem: "no variable name is set",
                    consequence: "The value is not applied.");

                return;
            }

            var stringReference = CachedComponent.StringReference;

            if (!stringReference.TryGetValue(_variableName, out var variable))
            {
                variable = new TVariable();
                stringReference.Add(_variableName, variable);
            }

            if (variable is not TVariable typed)
            {
                this.LogError(
                    problem: $"the variable {_variableName.Describe()} is a {variable.GetType().Name}, " +
                             $"not a {typeof(TVariable).Name}",
                    consequence: "The value is not applied.");

                return;
            }

            typed.Value = value;
            CachedComponent.RefreshString();
        }
    }
}
#endif
