#nullable enable
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IViewModel"/> that holds a single bindable value of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value exposed by <see cref="Value"/>.</typeparam>
    [ViewModel]
    [Serializable]
    public partial class ValueViewModel<T>
    {
        [SerializeField] private T? _value;
        
        /// <summary>
        /// Indicates whether equality checks are performed before raising change notifications.
        /// </summary>
        public bool CheckEquality { get; }

        /// <param name="checkEquality">When <see langword="true"/>, skips change notification if the new value equals the current one.</param>
        public ValueViewModel(bool checkEquality = true)
            : this(default, checkEquality) { }

        /// <param name="value">The initial value.</param>
        /// <param name="checkEquality">When <see langword="true"/>, skips change notification if the new value equals the current one.</param>
        public ValueViewModel(
            T? value,
            bool checkEquality = true)
        {
            _value = value;
            CheckEquality = checkEquality;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [TwoWayBind]
        public T? Value
        {
            get => _value;
            set
            {
                if (CheckEquality && EqualityComparer<T?>.Default.Equals(_value, value)) return;

                _value = value;
                OnValuePropertyChanged();
            }
        }
    }

    /// <summary>
    /// <see cref="IViewModel"/> that holds two independent bindable values of types <typeparamref name="T1"/> and <typeparamref name="T2"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the value exposed by <see cref="Value1"/>.</typeparam>
    /// <typeparam name="T2">The type of the value exposed by <see cref="Value2"/>.</typeparam>
    [ViewModel]
    [Serializable]
    public partial class ValueViewModel<T1, T2>
    {
        [SerializeField] private T1? _value1;
        [SerializeField] private T2? _value2;

        /// <summary>
        /// Indicates whether equality checks are performed before raising change notifications.
        /// </summary>
        public bool CheckEquality { get; }

        /// <param name="checkEquality">When <see langword="true"/>, skips change notification if the new value equals the current one.</param>
        public ValueViewModel(bool checkEquality = true)
            : this(default, default, checkEquality) { }

        /// <param name="values">The tuple containing the initial values for <see cref="Value1"/> and <see cref="Value2"/>.</param>
        /// <param name="checkEquality">When <see langword="true"/>, skips change notification if the new value equals the current one.</param>
        public ValueViewModel(
            (T1?, T2?) values,
            bool checkEquality = true)
            : this(values.Item1, values.Item2, checkEquality) { }

        /// <param name="value1">The initial value for <see cref="Value1"/>.</param>
        /// <param name="value2">The initial value for <see cref="Value2"/>.</param>
        /// <param name="checkEquality">When <see langword="true"/>, skips change notification if the new value equals the current one.</param>
        public ValueViewModel(
            T1? value1,
            T2? value2,
            bool checkEquality = true)
        {
            _value1 = value1;
            _value2 = value2;
            CheckEquality = checkEquality;
        }

        /// <summary>
        /// Gets or sets the first value.
        /// </summary>
        [TwoWayBind]
        public T1? Value1
        {
            get => _value1;
            set
            {
                if (CheckEquality && EqualityComparer<T1?>.Default.Equals(_value1, value)) return;

                _value1 = value;
                OnValue1PropertyChanged();
            }
        }
        
        /// <summary>
        /// Gets or sets the second value.
        /// </summary>
        [TwoWayBind]
        public T2? Value2
        {
            get => _value2;
            set
            {
                if (CheckEquality && EqualityComparer<T2?>.Default.Equals(_value2, value)) return;

                _value2 = value;
                OnValue2PropertyChanged();
            }
        }
    }

    /// <summary>
    /// <see cref="IViewModel"/> that holds three independent bindable values of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, and <typeparamref name="T3"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the value exposed by <see cref="Value1"/>.</typeparam>
    /// <typeparam name="T2">The type of the value exposed by <see cref="Value2"/>.</typeparam>
    /// <typeparam name="T3">The type of the value exposed by <see cref="Value3"/>.</typeparam>
    [ViewModel]
    [Serializable]
    public partial class ValueViewModel<T1, T2, T3>
    {
        [SerializeField] private T1? _value1;
        [SerializeField] private T2? _value2;
        [SerializeField] private T3? _value3;

        /// <summary>
        /// Indicates whether equality checks are performed before raising change notifications.
        /// </summary>
        public bool CheckEquality { get; }

        /// <param name="checkEquality">When <see langword="true"/>, skips change notification if the new value equals the current one.</param>
        public ValueViewModel(bool checkEquality = true)
            : this(default, default, default, checkEquality) { }

        /// <param name="values">The tuple containing the initial values for <see cref="Value1"/>, <see cref="Value2"/>, and <see cref="Value3"/>.</param>
        /// <param name="checkEquality">When <see langword="true"/>, skips change notification if the new value equals the current one.</param>
        public ValueViewModel(
            (T1?, T2?, T3?) values,
            bool checkEquality = true)
            : this(values.Item1, values.Item2, values.Item3, checkEquality) { }

        /// <param name="value1">The initial value for <see cref="Value1"/>.</param>
        /// <param name="value2">The initial value for <see cref="Value2"/>.</param>
        /// <param name="value3">The initial value for <see cref="Value3"/>.</param>
        /// <param name="checkEquality">When <see langword="true"/>, skips change notification if the new value equals the current one.</param>
        public ValueViewModel(
            T1? value1,
            T2? value2,
            T3? value3,
            bool checkEquality = true)
        {
            _value1 = value1;
            _value2 = value2;
            _value3 = value3;
            CheckEquality = checkEquality;
        }

        /// <summary>
        /// Gets or sets the first value.
        /// </summary>
        [TwoWayBind]
        public T1? Value1
        {
            get => _value1;
            set
            {
                if (CheckEquality && EqualityComparer<T1?>.Default.Equals(_value1, value)) return;

                _value1 = value;
                OnValue1PropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the second value.
        /// </summary>
        [TwoWayBind]
        public T2? Value2
        {
            get => _value2;
            set
            {
                if (CheckEquality && EqualityComparer<T2?>.Default.Equals(_value2, value)) return;

                _value2 = value;
                OnValue2PropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the third value.
        /// </summary>
        [TwoWayBind]
        public T3? Value3
        {
            get => _value3;
            set
            {
                if (CheckEquality && EqualityComparer<T3?>.Default.Equals(_value3, value)) return;

                _value3 = value;
                OnValue3PropertyChanged();
            }
        }
    }

    /// <summary>
    /// <see cref="IViewModel"/> that holds four independent bindable values of types <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, and <typeparamref name="T4"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the value exposed by <see cref="Value1"/>.</typeparam>
    /// <typeparam name="T2">The type of the value exposed by <see cref="Value2"/>.</typeparam>
    /// <typeparam name="T3">The type of the value exposed by <see cref="Value3"/>.</typeparam>
    /// <typeparam name="T4">The type of the value exposed by <see cref="Value4"/>.</typeparam>
    [ViewModel]
    [Serializable]
    public partial class ValueViewModel<T1, T2, T3, T4>
    {
        [SerializeField] private T1? _value1;
        [SerializeField] private T2? _value2;
        [SerializeField] private T3? _value3;
        [SerializeField] private T4? _value4;

        /// <summary>
        /// Indicates whether equality checks are performed before raising change notifications.
        /// </summary>
        public bool CheckEquality { get; }

        /// <param name="checkEquality">When <see langword="true"/>, skips change notification if the new value equals the current one.</param>
        public ValueViewModel(bool checkEquality = true)
            : this(default, default, default, default, checkEquality) { }

        /// <param name="values">The tuple containing the initial values for <see cref="Value1"/>, <see cref="Value2"/>, <see cref="Value3"/>, and <see cref="Value4"/>.</param>
        /// <param name="checkEquality">When <see langword="true"/>, skips change notification if the new value equals the current one.</param>
        public ValueViewModel(
            (T1?, T2?, T3?, T4?) values,
            bool checkEquality = true)
            : this(values.Item1, values.Item2, values.Item3, values.Item4, checkEquality) { }

        /// <param name="value1">The initial value for <see cref="Value1"/>.</param>
        /// <param name="value2">The initial value for <see cref="Value2"/>.</param>
        /// <param name="value3">The initial value for <see cref="Value3"/>.</param>
        /// <param name="value4">The initial value for <see cref="Value4"/>.</param>
        /// <param name="checkEquality">When <see langword="true"/>, skips change notification if the new value equals the current one.</param>
        public ValueViewModel(
            T1? value1,
            T2? value2,
            T3? value3,
            T4? value4,
            bool checkEquality = true)
        {
            _value1 = value1;
            _value2 = value2;
            _value3 = value3;
            _value4 = value4;
            CheckEquality = checkEquality;
        }

        /// <summary>
        /// Gets or sets the first value.
        /// </summary>
        [TwoWayBind]
        public T1? Value1
        {
            get => _value1;
            set
            {
                if (CheckEquality && EqualityComparer<T1?>.Default.Equals(_value1, value)) return;

                _value1 = value;
                OnValue1PropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the second value.
        /// </summary>
        [TwoWayBind]
        public T2? Value2
        {
            get => _value2;
            set
            {
                if (CheckEquality && EqualityComparer<T2?>.Default.Equals(_value2, value)) return;

                _value2 = value;
                OnValue2PropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the third value.
        /// </summary>
        [TwoWayBind]
        public T3? Value3
        {
            get => _value3;
            set
            {
                if (CheckEquality && EqualityComparer<T3?>.Default.Equals(_value3, value)) return;

                _value3 = value;
                OnValue3PropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the fourth value.
        /// </summary>
        [TwoWayBind]
        public T4? Value4
        {
            get => _value4;
            set
            {
                if (CheckEquality && EqualityComparer<T4?>.Default.Equals(_value4, value)) return;

                _value4 = value;
                OnValue4PropertyChanged();
            }
        }
    }
}
