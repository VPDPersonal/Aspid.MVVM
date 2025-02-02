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
        /// A <see cref="BindResult"/> object that contains information about the binding operation.
        /// The <see cref="BindResult.IsBound"/> property indicates whether the binder was successfully bound.
        /// If the binding was successful, the <see cref="BindResult.BinderRemover"/> property provides an interface
        /// for removing the binder from the ViewModel. If the binding failed (e.g., the property is read-only),
        /// <see cref="BindResult.BinderRemover"/> will be null.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BindResult AddBinder(this in BindParameters parameters, IBinder binder) =>
            parameters.ViewModel.AddBinder(binder, parameters.Id);
        
        /// <summary>
        /// Disposes of the ViewModel instance if it implements <see cref="IDisposable"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel instance to dispose of.</param>
        /// <typeparam name="T">The type of the ViewModel that implements <see cref="IViewModel"/> and <see cref="IDisposable"/>.</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisposeViewModel<T>(this T viewModel)
            where T : class, IViewModel, IDisposable
        {
            viewModel.Dispose();
        }
        
        /// <summary>
        /// Disposes of the ViewModel instance if it implements <see cref="IDisposable"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel instance to dispose of.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisposeViewModel(this IViewModel viewModel)
        {
            if (viewModel is IDisposable disposable)
                disposable.Dispose();
        }
    }
}