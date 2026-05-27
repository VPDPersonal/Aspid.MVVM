using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a bindable member event that provides a single value in a one-time binding operation.
    /// </summary>
    /// <typeparam name="T">The type of the value to be bound.</typeparam>
    public sealed class OneTimeBindableMember<T> : OneTimeBindableMember, IReadOnlyValueBindableMember<T>
    {
        /// <summary>
        /// Gets the current value.
        /// </summary>
        public T? Value { get; private set; }

        /// <summary>
        /// Gets the binding mode for this member.
        /// </summary>
        public BindMode Mode => BindMode.OneTime;

        private OneTimeBindableMember(T? value)
        {
            Value = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds a one-time binding to the specified binder with the associated value.
        /// </summary>
        /// <param name="binder">The binder to be used for the binding.</param>
        /// <returns>
        /// Always returns <c>null</c> because removal of this binding is not supported.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binding mode is either <see cref="BindMode.OneWayToSource"/> or <see cref="BindMode.OneTime"/> <see cref="BindMode.None"/>.
        /// </exception>
        IBinderRemover? IBinderAdder.Add(IBinder binder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (AddMarker.Auto())
#endif
            {
                binder.Mode.ThrowExceptionIfNotOne();

                switch (binder)
                {
                    case IBinder<T> specificBinder:
                        specificBinder.SetValue(Value);
                        break;

                    case IAnyBinder anyBinder:
                        anyBinder.SetValue(Value);
                        break;

                    default: throw BinderInvalidCastException.Class<T>(binder);
                }
            
                return null;
            }
        }

        /// <summary>
        /// Creates a new instance configured with the provided value for one-time binding.
        /// </summary>
        /// <param name="value">The value to be provided to the binder.</param>
        /// <returns>A new <see cref="OneTimeBindableMember{T}"/> instance configured with the specified value.</returns>
        public static OneTimeBindableMember<T> Get(T value)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (GetMarker.Auto())
#endif
            {
                return new(value);
            }
        }
    }
    
    public abstract class OneTimeBindableMember
    {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
        protected static readonly Unity.Profiling.ProfilerMarker AddMarker = new("OneTimeBindableMember.Add");
        protected static readonly Unity.Profiling.ProfilerMarker GetMarker = new("OneTimeBindableMember.Get");
#endif
    }
}