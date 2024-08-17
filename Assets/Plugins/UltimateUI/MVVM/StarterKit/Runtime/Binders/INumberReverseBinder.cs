// ReSharper disable once CheckNamespace

using System;

namespace UltimateUI.MVVM.StarterKit.Binders
{
    public interface INumberReverseBinder : IReverseBinder<int>, IReverseBinder<long>, IReverseBinder<float>, IReverseBinder<double>
    {
        public event Action<int> IntValueChanged;
        public event Action<long> LongValueChanged;
        public event Action<float> FloatValueChanged;
        public event Action<double> DoubleValueChanged;
        
        event Action<int> IReverseBinder<int>.ValueChanged
        {
            add => IntValueChanged += value;
            remove => IntValueChanged -= value;
        }
        
        event Action<long> IReverseBinder<long>.ValueChanged
        {
            add => LongValueChanged += value;
            remove => LongValueChanged -= value;
        }
        
        event Action<float> IReverseBinder<float>.ValueChanged
        {
            add => FloatValueChanged += value;
            remove => FloatValueChanged -= value;
        }
        
        event Action<double> IReverseBinder<double>.ValueChanged
        {
            add => DoubleValueChanged += value;
            remove => DoubleValueChanged -= value;
        }
    }
}