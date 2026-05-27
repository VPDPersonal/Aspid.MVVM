#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED                                                                                                                                                                                                                    
#define PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Sealed <see cref="IBinderAdder"/> that forwards View-side value changes back to the ViewModel through a
    /// captured setter <see cref="Action{T}"/>; additionally exposes the latest <see cref="Value"/> and a
    /// <see cref="Changed"/> event. Accepts only <see cref="BindMode.OneWayToSource"/> and
    /// <see cref="BindMode.TwoWay"/> reverse binders.
    /// </summary>
    /// <typeparam name="T">The reference type of the bound value.</typeparam>
    public sealed class OneWayToSourceBindableMember<T> : IReadOnlyBindableMember<T>, IBinderRemover
    {
        /// <summary>
        /// Raised when the value changes.
        /// </summary>
        public event Action<T?>? Changed;
        
        private readonly Action<T?> _setValue;
        
        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        public T? Value { get; private set; }
        
        /// <summary>
        /// Gets the binding mode for this member.
        /// </summary>
        public BindMode Mode => BindMode.OneWayToSource;    

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayToSourceBindableMember{T}"/> class with the specified value setter action.
        /// </summary>
        /// <param name="setValue">
        /// The action used to set the value when the event is triggered.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="setValue"/> is <see langword="null"/>.</exception>
        public OneWayToSourceBindableMember(Action<T?> setValue)
        {
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Adds a binder to the event if it supports reverse binding for one-way-to-source or two-way modes.
        /// </summary>
        /// <param name="binder">The binder to bind to the event.</param>
        /// <returns>Returns itself to allow unsubscription later.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the <paramref name="binder"/> does not have a valid binding mode or is not of type <see cref="IReverseBinder{T}"/>.
        /// </exception>
        IBinderRemover IBinderAdder.Add(IBinder binder)
        {
#if PROFILER
            using (this.Marker())
#endif
            {
                binder.Mode.ThrowExceptionIfNotTwo();

                switch (binder)
                {
                    case IReverseBinder<T> reverseBinder: reverseBinder.ValueChanged += OnValueChanged; break;
                    case IAnyReverseBinder anyReverseBinder: anyReverseBinder.ValueChanged += OnObjectValueChanged; break;
                    default: throw ReverseBinderInvalidCastException<T>.Class(binder);
                }

                return this;
            }
        }

        /// <summary>
        /// Removes the binder's subscription from the event.
        /// </summary>
        /// <param name="binder">The binder to unsubscribe from the event.</param>
        void IBinderRemover.Remove(IBinder binder)
        {
#if PROFILER
            using (this.Marker())
#endif
            {
                switch (binder)
                {
                    case IReverseBinder<T> reverseBinder: reverseBinder.ValueChanged -= OnValueChanged; break;
                    case IAnyReverseBinder anyReverseBinder: anyReverseBinder.ValueChanged -= OnObjectValueChanged; break;
                    default: throw ReverseBinderInvalidCastException<T>.Class(binder);
                }
            }
        }

        private void OnValueChanged(T? value)
        {
#if PROFILER
            using (this.Marker())
#endif
            {
                Value = value;
                _setValue(value);
                Changed?.Invoke(value);
            }
        }
        
        private void OnObjectValueChanged(object value)
        {
#if PROFILER
            using (this.Marker())
#endif
            {
                if (value is null)
                    OnValueChanged(default);
                else if (value is not T specificValue)
                    throw new ArgumentException("Value must be of type " + typeof(T).FullName);
                else
                    OnValueChanged(specificValue);
            }
        }
    }
}
