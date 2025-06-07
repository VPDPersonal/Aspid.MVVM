namespace Aspid.MVVM
{
    /// <summary>
    /// Interface for adding event bindings to a bindable member.
    /// </summary>
    public interface IBindableMemberEventAdder
    {
        /// <summary>
        /// Adds a binding to the bindable member using the specified binder.
        /// </summary>
        /// <param name="binder">The binder to be used for adding the binding.</param>
        /// <returns>
        /// An <see cref="IBindableMemberEventRemover"/> that can remove the added binding, 
        /// or <c>null</c> if the binding could not be added.
        /// </returns>
        public IBindableMemberEventRemover? Add(IBinder binder);
    }

}