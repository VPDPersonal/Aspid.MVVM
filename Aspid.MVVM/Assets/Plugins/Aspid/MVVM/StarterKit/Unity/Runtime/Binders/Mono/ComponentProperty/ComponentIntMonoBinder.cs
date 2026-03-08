using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base MonoBehaviour binder for binding an <see langword="int"/> property on a Unity <see cref="UnityEngine.Component"/>.
    /// Implements <see cref="INumberBinder"/> to accept int, long, float, and double values.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value is sent back to the ViewModel.
    /// </summary>
    public abstract partial class ComponentIntMonoBinder<TComponent> : ComponentMonoBinder<TComponent, int, Converter>,
        INumberBinder,
        INumberReverseBinder
        where TComponent : Component
    {
        public event Action<int> IntValueChanged;
        public event Action<long> LongValueChanged;
        public event Action<float> FloatValueChanged;
        public event Action<double> DoubleValueChanged;

        [BinderLog]
        public void SetValue(long value) =>
            base.SetValue((int)value);
        
        [BinderLog]
        public void SetValue(float value) =>
            base.SetValue((int)value);

        [BinderLog]
        public void SetValue(double value) =>
            base.SetValue((int)value);
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
            {
                var value = GetConvertedValue(Property);
                
                IntValueChanged?.Invoke(value);
                LongValueChanged?.Invoke(value);
                FloatValueChanged?.Invoke(value);
                DoubleValueChanged?.Invoke(value);
            }
        }
    }
}