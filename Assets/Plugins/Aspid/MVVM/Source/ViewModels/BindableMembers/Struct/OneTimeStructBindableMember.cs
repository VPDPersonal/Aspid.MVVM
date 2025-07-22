namespace Aspid.MVVM;

/// <summary>
/// Represents a bindable member event that provides a single value in a one-time binding operation.
/// </summary>
/// <typeparam name="T">The type of the value to be bound.</typeparam>
public sealed class OneTimeStructBindableMember<T> : OneTimeStructBindableMember<T, ValueType>
    where T : struct
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OneTimeStructBindableMember{T}"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value to be bound in the event.</param>
    public OneTimeStructBindableMember(T value) 
        : base(value) { }
}

/// <summary>
/// Represents a bindable member event that provides a single value in a one-time binding operation.
/// </summary>
/// <typeparam name="T">The type of the value to be bound.</typeparam>
/// <typeparam name="TBoxed">Boxed type</typeparam>
public abstract class OneTimeStructBindableMember<T, TBoxed> : OneTimeStructBindableMember, IReadOnlyValueBindableMember<T>
    where T : struct, TBoxed
    where TBoxed : class
{
    /// <summary>
    /// Gets or sets the current value.
    /// </summary>
    public T Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OneTimeStructBindableMember{T,TBoxed}"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value to be bound in the event.</param>
    protected OneTimeStructBindableMember(T value)
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
            if (binder.Mode is not (BindMode.OneWay or BindMode.OneTime))
                throw new InvalidOperationException($"Mode must be OneWay or OneTime. Mode = {{{binder.Mode}}}");

            switch (binder)
            {
                case IBinder<T> specificBinder: specificBinder.SetValue(Value); break;
                case IBinder<TBoxed> structBinder: structBinder.SetValue(Value); break;
                case IAnyBinder anyBinder: anyBinder.SetValue(Value); break;
                default: throw BinderInvalidCastException.Struct<T, TBoxed>(binder);
            }
            
            return null;
        }
    }
}
    
public abstract class OneTimeStructBindableMember
{
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
    protected static readonly Unity.Profiling.ProfilerMarker AddMarker = new("OneTimeStructEvent.Add");
#endif
}