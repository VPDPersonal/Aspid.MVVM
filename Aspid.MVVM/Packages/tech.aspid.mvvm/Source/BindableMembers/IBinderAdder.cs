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
        /// Attaches <paramref name="binder"/> using the binding directions supported by both the binder and the member.
        /// </summary>
        /// <remarks>
        /// When the effective mode reaches the View, pushes the current value to the binder during attachment.
        /// </remarks>
        /// <param name="binder">The binder to add.</param>
        /// <returns>
        /// The remover that detaches <paramref name="binder"/>; <see langword="null"/> when nothing stays subscribed,
        /// as for <see cref="BindMode.OneTime"/>.
        /// </returns>
        public IBinderRemover? Add(IBinder binder);
    }
}
