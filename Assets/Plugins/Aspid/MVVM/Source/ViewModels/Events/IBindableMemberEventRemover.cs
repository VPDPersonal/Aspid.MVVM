namespace Aspid.MVVM
{
    /// <summary>
    /// Interface for removing event bindings from a bindable member.
    /// </summary>
    public interface IBindableMemberEventRemover
    {
        /// <summary>
        /// Removes the binding associated with the specified binder.
        /// </summary>
        /// <param name="binder">The binder whose binding needs to be removed.</param>
        public void Remove(IBinder binder);
    }

}