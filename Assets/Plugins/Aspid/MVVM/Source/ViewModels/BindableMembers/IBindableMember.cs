namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a bindable member that allows setting a value and notifies listeners when the value changes.
    /// </summary>
    /// <typeparam name="T">The type of the value being bound.</typeparam>
    public interface IBindableMember<T> : IReadOnlyBindableMember<T>
    {
        /// <summary>
        /// Sets the value of the bindable member.
        /// </summary>
        public new T? Value { set; }
    }
}