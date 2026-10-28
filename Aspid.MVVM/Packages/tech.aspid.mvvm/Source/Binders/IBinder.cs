// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Defines a binder that attaches to a bindable member of a ViewModel.
    /// </summary>
    public interface IBinder
    {
        /// <summary>
        /// Gets the binding mode; <see cref="BindMode.OneWay"/> unless the implementing type provides its own.
        /// </summary>
        public BindMode Mode => BindMode.OneWay;

        /// <summary>
        /// Attaches the binder to the bindable member behind <paramref name="binderAdder"/>.
        /// </summary>
        /// <param name="binderAdder">The bindable member to attach to.</param>
        public void Bind(IBinderAdder binderAdder);

        /// <summary>
        /// Detaches the binder from its bindable member.
        /// </summary>
        public void Unbind();
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
