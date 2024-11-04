using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Views
{
    /// <summary>
    /// Interface for initializing a View using a specified ViewModel.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Gets the associated ViewModel.
        /// If the view is not initialized, it may return <c>null</c>.
        /// </summary>
        public IViewModel? ViewModel { get; }
        
        /// <summary>
        /// Initializes the view with the specified <see cref="IViewModel"/> for binding.
        /// </summary>
        /// <param name="viewModel">The <see cref="IViewModel"/> object used to initialize the View.</param>
        public void Initialize(IViewModel viewModel);
        
        /// <summary>
        /// Deinitializes the view, resetting the ViewModel property to null.
        /// </summary>
        public void Deinitialize();
    }
} 