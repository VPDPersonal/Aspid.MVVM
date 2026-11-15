// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Defines a binder that attaches to a bindable member of a ViewModel.
    /// </summary>
    public interface IBinder
    {
        /// <summary>
        /// Gets a value indicating whether binding is complete and cleanup has not started.
        /// </summary>
        /// <remarks>A <see cref="BindMode.OneTime"/> binding remains bound until <see cref="Unbind"/>.</remarks>
        public bool IsBound { get; }

        /// <summary>
        /// Gets the binding mode; <see cref="BindMode.OneWay"/> unless the implementing type provides its own.
        /// </summary>
        public BindMode Mode => BindMode.OneWay;

        /// <summary>
        /// Attaches the binder to the member and owner specified by <paramref name="context"/>.
        /// </summary>
        /// <remarks>
        /// Installation failures release partially acquired resources. Completion notifications run after the binding is
        /// established; their exceptions do not roll it back, and their callbacks may release or replace it.
        /// </remarks>
        /// <param name="context">The bindable member and the owner acquiring the binding.</param>
        public void Bind(in BindContext context);

        /// <summary>
        /// Detaches the binder when <paramref name="context"/> identifies its owner by reference equality.
        /// </summary>
        /// <remarks>
        /// Does nothing for an unbound binder or a different owner. Cleanup releases ownership even when it throws;
        /// completion callbacks may establish a new binding.
        /// </remarks>
        /// <param name="context">The owner releasing the binding.</param>
        public void Unbind(in UnbindContext context);
    }

    /// <summary>
    /// <see cref="IBinder"/> that receives values of <typeparamref name="T"/> from the ViewModel.
    /// </summary>
    /// <typeparam name="T">The bound value type.</typeparam>
    public interface IBinder<in T> : IBinder
    {
        /// <summary>
        /// Applies <paramref name="value"/> to the View.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(T? value);
    }
}
