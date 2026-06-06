// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Base interface for binder implementations.
    /// Defines the binding lifecycle — binding to and unbinding from an <see cref="IViewModel"/>.
    /// </summary>
    public interface IBinder
    {
        /// <summary>
        /// Gets the binding mode that determines the direction of data flow.
        /// Default is <see cref="BindMode.OneWay"/>.
        /// </summary>
        public BindMode Mode => BindMode.OneWay;

        /// <summary>
        /// Binds this binder using the specified <see cref="IBinderAdder"/>.
        /// </summary>
        /// <param name="binderAdder">The binder adder that registers this binder with the ViewModel.</param>
        public void Bind(IBinderAdder binderAdder);

        /// <summary>
        /// Unbinds this binder from the bound <see cref="IViewModel"/>.
        /// </summary>
        public void Unbind();
    }

    /// <summary>
    /// Extends <see cref="IBinder"/> with the ability to receive typed values from the ViewModel.
    /// </summary>
    /// <typeparam name="T">The type of value that can be received from the ViewModel.</typeparam>
    public interface IBinder<in T> : IBinder
    {
        /// <summary>
        /// Sets the bound property to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(T? value);
    }
}
