namespace Aspid.MVVM.ViewModels
{
    /// <summary>
    /// Interface for removing a binder from the ViewModel.
    /// </summary>
    public interface IRemoveBinderFromViewModel
    {
        /// <summary>
        /// Removes the specified binder from the ViewModel.
        /// </summary>
        /// <param name="binder">The binder to be removed from the ViewModel.</param>
        public void RemoveBinder(IBinder binder);
    }
}