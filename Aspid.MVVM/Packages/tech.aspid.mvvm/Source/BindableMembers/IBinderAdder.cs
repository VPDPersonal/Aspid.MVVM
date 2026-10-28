// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Defines the side of a bindable member that accepts binders.
    /// </summary>
    public interface IBinderAdder
    {
        /// <summary>
        /// Gets the binding mode of the member.
        /// </summary>
        public BindMode Mode { get; }
        
        /// <summary>
        /// Subscribes <paramref name="binder"/> according to its <see cref="IBinder.Mode"/> and, for a mode that reaches the View,
        /// pushes the current value to it.
        /// </summary>
        /// <param name="binder">The binder to add.</param>
        /// <returns>
        /// The remover that detaches <paramref name="binder"/>; <see langword="null"/> when nothing stays subscribed,
        /// as for <see cref="BindMode.OneTime"/>.
        /// </returns>
        public IBinderRemover? Add(IBinder binder);
    }
}
