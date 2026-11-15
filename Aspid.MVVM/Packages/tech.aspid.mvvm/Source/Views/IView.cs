// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Defines a View that binds its binders to a ViewModel.
    /// </summary>
    /// <remarks>Binder instances and collection contents stay unchanged between <see cref="Initialize"/> and <see cref="Deinitialize"/>.</remarks>
    public interface IView
    {
        /// <summary>
        /// Gets the ViewModel the view is initialized with; <see langword="null"/> when it is not initialized.
        /// </summary>
        public IViewModel? ViewModel { get; }
        
        /// <summary>
        /// Initializes the view with <paramref name="viewModel"/>, binding its binders to the ViewModel's members.
        /// </summary>
        /// <param name="viewModel">The ViewModel to bind.</param>
        public void Initialize(IViewModel viewModel);
        
        /// <summary>
        /// Unbinds the binders and clears <see cref="ViewModel"/>.
        /// </summary>
        /// <remarks>Does not dispose the ViewModel.</remarks>
        public void Deinitialize();
    }
    
    /// <summary>
    /// <see cref="IView"/> that accepts a ViewModel of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The ViewModel type the view binds to.</typeparam>
    public interface IView<in T> : IView
        where T : IViewModel
    {
        /// <inheritdoc cref="IView.Initialize"/>
        public void Initialize(T viewModel);
    }
}
