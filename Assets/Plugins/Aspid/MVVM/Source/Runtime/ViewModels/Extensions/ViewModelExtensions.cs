using System;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    /// <summary>
    /// Provides extension methods for the <see cref="IViewModel"/> interface.
    /// </summary>
    public static class ViewModelExtensions
    {
        /// <summary>
        /// Adds the specified binder to the ViewModel property specified in the binding parameters.
        /// </summary>
        /// <param name="parameters">
        /// The parameters that contain the ViewModel and the component ID (property name), where the component ID matches
        /// the property name in the ViewModel.
        /// </param>
        /// <param name="binder">The binder to be associated with the ViewModel property.</param>
        /// <returns>
        /// An interface for removing the binder from the ViewModel, or <c>null</c> if the binder could not be added
        /// or if the property is read-only.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRemoveBinderFromViewModel? AddBinder(this in BindParameters parameters, IBinder binder) =>
            parameters.ViewModel.AddBinder(binder, parameters.Id);
        
        /// <summary>
        /// Disposes of the ViewModel instance if it implements <see cref="IDisposable"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel instance to dispose of.</param>
        /// <typeparam name="T">The type of the ViewModel that implements <see cref="IViewModel"/> and <see cref="IDisposable"/>.</typeparam>
        public static void DisposeViewModel<T>(this T viewModel)
            where T : class, IViewModel, IDisposable
        {
            viewModel.Dispose();
        }
        
        /// <summary>
        /// Disposes of the ViewModel instance if it implements <see cref="IDisposable"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel instance to dispose of.</param>
        public static void DisposeViewModel(this IViewModel viewModel)
        {
            if (viewModel is IDisposable disposable)
                disposable.Dispose();
        }
    }
}