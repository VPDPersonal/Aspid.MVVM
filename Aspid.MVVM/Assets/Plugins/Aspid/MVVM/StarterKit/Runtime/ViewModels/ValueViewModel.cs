#nullable disable
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [ViewModel]
    public partial class ValueViewModel<T>
    {
        private T _value;
        
        [TwoWayBind]
        public T Value
        {
            get => _value;
            set
            {
                if (CheckEquality && EqualityComparer<T>.Default.Equals(_value, value)) return;
                
                _value = value;
                OnValuePropertyChanged();
            }
        }
        
        public bool CheckEquality { get; }
        
        public ValueViewModel(bool checkEquality = true)
            : this(default, checkEquality) { }

        public ValueViewModel(T value, bool checkEquality = true)
        {
            _value = value;
            CheckEquality = checkEquality;
        }
    }
    
    [ViewModel]
    public partial class ValueViewModel<T1, T2>
    {
        private T1 _value1;
        private T2 _value2;
        
        [TwoWayBind]
        public T1 Value1
        {
            get => _value1;
            set
            {
                if (CheckEquality && EqualityComparer<T1>.Default.Equals(_value1, value)) return;
                
                _value1 = value;
                OnValue1PropertyChanged();
            }
        }
        
        [TwoWayBind]
        public T2 Value2
        {
            get => _value2;
            set
            {
                if (CheckEquality && EqualityComparer<T2>.Default.Equals(_value2, value)) return;
                
                _value2 = value;
                OnValue2PropertyChanged();
            }
        }
        
        public bool CheckEquality { get; }
        
        public ValueViewModel(bool checkEquality = true)
            : this(default, default, checkEquality) { }
        
        public ValueViewModel((T1, T2) values, bool checkEquality = true)
            : this(values.Item1, values.Item2, checkEquality) { }

        public ValueViewModel(T1 value1, T2 value2, bool checkEquality = true)
        {
            _value1 = value1;
            _value2 = value2;
            CheckEquality = checkEquality;
        }
    }
    
    [ViewModel]
    public partial class ValueViewModel<T1, T2, T3>
    {
        private T1 _value1;
        private T2 _value2;
        private T3 _value3;
        
        [TwoWayBind]
        public T1 Value1
        {
            get => _value1;
            set
            {
                if (CheckEquality && EqualityComparer<T1>.Default.Equals(_value1, value)) return;
                
                _value1 = value;
                OnValue1PropertyChanged();
            }
        }
        
        [TwoWayBind]
        public T2 Value2
        {
            get => _value2;
            set
            {
                if (CheckEquality && EqualityComparer<T2>.Default.Equals(_value2, value)) return;
                
                _value2 = value;
                OnValue2PropertyChanged();
            }
        }
        
        [TwoWayBind]
        public T3 Value3
        {
            get => _value3;
            set
            {
                if (CheckEquality && EqualityComparer<T3>.Default.Equals(_value3, value)) return;
                
                _value3 = value;
                OnValue3PropertyChanged();
            }
        }
        
        public bool CheckEquality { get; }
        
        public ValueViewModel(bool checkEquality = true)
            : this(default, default, default, checkEquality) { }
        
        public ValueViewModel((T1, T2, T3) values, bool checkEquality = true)
            : this(values.Item1, values.Item2, values.Item3, checkEquality) { }

        public ValueViewModel(T1 value1, T2 value2, T3 value3, bool checkEquality = true)
        {
            _value1 = value1;
            _value2 = value2;
            _value3 = value3;
            CheckEquality = checkEquality;
        }
    }
    
    [ViewModel]
    public partial class ValueViewModel<T1, T2, T3, T4>
    {
        private T1 _value1;
        private T2 _value2;
        private T3 _value3;
        private T4 _value4;
        
        [TwoWayBind]
        public T1 Value1
        {
            get => _value1;
            set
            {
                if (CheckEquality && EqualityComparer<T1>.Default.Equals(_value1, value)) return;
                
                _value1 = value;
                OnValue1PropertyChanged();
            }
        }
        
        [TwoWayBind]
        public T2 Value2
        {
            get => _value2;
            set
            {
                if (CheckEquality && EqualityComparer<T2>.Default.Equals(_value2, value)) return;
                
                _value2 = value;
                OnValue2PropertyChanged();
            }
        }
        
        [TwoWayBind]
        public T3 Value3
        {
            get => _value3;
            set
            {
                if (CheckEquality && EqualityComparer<T3>.Default.Equals(_value3, value)) return;
                
                _value3 = value;
                OnValue3PropertyChanged();
            }
        }
        
        [TwoWayBind]
        public T4 Value4
        {
            get => _value4;
            set
            {
                if (CheckEquality && EqualityComparer<T4>.Default.Equals(_value4, value)) return;
                
                _value4 = value;
                OnValue4PropertyChanged();
            }
        }
        
        public bool CheckEquality { get; }
        
        public ValueViewModel(bool checkEquality = true)
            : this(default, default, default, default, checkEquality) { }
        
        public ValueViewModel((T1, T2, T3, T4) values, bool checkEquality = true)
            : this(values.Item1, values.Item2, values.Item3, values.Item4, checkEquality) { }

        public ValueViewModel(T1 value1, T2 value2, T3 value3, T4 value4, bool checkEquality = true)
        {
            _value1 = value1;
            _value2 = value2;
            _value3 = value3;
            _value4 = value4;
            CheckEquality = checkEquality;
        }
    }
}