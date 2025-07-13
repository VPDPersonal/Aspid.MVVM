namespace Aspid.MVVM;

/// <summary>
/// Represents a bindable member event that provides a single value in a one-time binding operation.
/// </summary>
/// <typeparam name="T">The type of the value to be bound.</typeparam>
public sealed class OneTimeEnumBindableMember<T> : OneTimeStructBindableMember<T, Enum>
    where T : struct, Enum
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OneTimeEnumBindableMember{T}"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value to be bound in the event.</param>
    public OneTimeEnumBindableMember(T value) 
        : base(value) { }
}