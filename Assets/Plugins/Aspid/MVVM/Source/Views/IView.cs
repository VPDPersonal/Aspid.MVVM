// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
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
    
    /// <summary>
    /// Generic interface for initializing a View with a strongly-typed ViewModel.
    /// </summary>
    /// <typeparam name="T">The specific type of the ViewModel to be used for initialization. Must implement <see cref="IViewModel"/>.</typeparam>
    public interface IView<in T> : IView
        where T : IViewModel
    {
        /// <summary>
        /// Initializes the view with a strongly-typed <typeparamref name="T"/> ViewModel.
        /// </summary>
        /// <param name="viewModel">The <typeparamref name="T"/> ViewModel instance to initialize the View.</param>
        public void Initialize(T viewModel);
    }
}