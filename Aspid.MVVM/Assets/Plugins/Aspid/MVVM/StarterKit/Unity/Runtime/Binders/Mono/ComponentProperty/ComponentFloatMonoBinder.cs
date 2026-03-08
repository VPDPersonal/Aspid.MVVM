using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base MonoBehaviour binder for binding a <see langword="float"/> property on a Unity <see cref="UnityEngine.Component"/>.
    /// Implements <see cref="INumberBinder"/> to accept int, long, float, and double values.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value is sent back to the ViewModel.
    /// </summary>
    public abstract partial class ComponentFloatMonoBinder<TComponent> : ComponentMonoBinder<TComponent, float, Converter>,
        INumberBinder,
        INumberReverseBinder
        where TComponent : Component
    {
        public event Action<int> IntValueChanged;
        public event Action<long> LongValueChanged;
        public event Action<float> FloatValueChanged;
        public event Action<double> DoubleValueChanged;

        [BinderLog]
        public void SetValue(int value) =>
            base.SetValue(value);

        [BinderLog]
        public void SetValue(long value) =>
            base.SetValue(value);

        [BinderLog]
        public void SetValue(double value) =>
            base.SetValue((float)value);
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
            {
                var value = GetConvertedValue(Property);
                
                IntValueChanged?.Invoke((int)value);
                LongValueChanged?.Invoke((long)value);
                FloatValueChanged?.Invoke(value);
                DoubleValueChanged?.Invoke(value);
            }
        }
    }
}