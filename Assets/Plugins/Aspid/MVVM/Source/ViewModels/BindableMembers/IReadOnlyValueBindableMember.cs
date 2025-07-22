namespace Aspid.MVVM;

/// <summary>
/// Represents a bindable member that exposes a read-only value and allows binders to be added.
/// </summary>
/// <typeparam name="T">The type of the value being exposed.</typeparam>
public interface IReadOnlyValueBindableMember<out T> : IBinderAdder
{
    /// <summary>
    /// Gets the current value of the bindable member.
    /// </summary>
    public T? Value { get; }
}