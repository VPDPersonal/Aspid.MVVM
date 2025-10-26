using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a bindable member event that supports one-way-to-source bindings.
    /// </summary>
    /// <typeparam name="T">The type of the value to be handled in the bindable member event.</typeparam>
    public sealed class OneWayToSourceBindableMember<T> : OneWayToSourceBindableMember, IReadOnlyBindableMember<T>, IBinderRemover
    {
        /// <summary>
        /// Event triggered when the value changes.
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
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="setValue"/> is <c>null</c>.</exception>
        public OneWayToSourceBindableMember(Action<T?> setValue)
        {
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <inheritdoc />
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
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (AddMarker.Auto())
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

        /// <inheritdoc />
        /// <summary>
        /// Removes the binder's subscription from the event.
        /// </summary>
        /// <param name="binder">The binder to unsubscribe from the event.</param>
        void IBinderRemover.Remove(IBinder binder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (RemoveMarker.Auto())
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
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (OnValueChangedMarker.Auto())
#endif
            {
                Value = value;
                _setValue(value);
                Changed?.Invoke(value);
            }
        }
        
        private void OnObjectValueChanged(object value)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (OnObjectValueChangedMarker.Auto())
#endif
            {
                if (value is not T specificValue)
                    throw new ArgumentException("Value must be of type " + typeof(T).FullName);

                OnValueChanged(specificValue);
            }
        }
    }
    
    public abstract class OneWayToSourceBindableMember
    {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
        protected static readonly Unity.Profiling.ProfilerMarker AddMarker = new("OneWayToSourceBindableMember.Add");
        protected static readonly Unity.Profiling.ProfilerMarker RemoveMarker = new("OneWayToSourceBindableMember.Remove");
        protected static readonly Unity.Profiling.ProfilerMarker OnValueChangedMarker = new("OneWayToSourceBindableMember.OnValueChanged");
        protected static readonly Unity.Profiling.ProfilerMarker OnObjectValueChangedMarker = new("OneWayToSourceBindableMember.OnObjectValueChanged");
#endif
    }
}