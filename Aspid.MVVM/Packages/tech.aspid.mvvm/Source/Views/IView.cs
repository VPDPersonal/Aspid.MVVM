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
        /// Gets the bound ViewModel, or <see langword="null"/> before initialization and after deinitialization.
        /// </summary>
        public IViewModel? ViewModel { get; }
        
        /// <summary>
        /// Binds the View to the members of <paramref name="viewModel"/>.
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
