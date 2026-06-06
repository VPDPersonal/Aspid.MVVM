// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="Binder"/> that initializes an <see cref="IView"/> when a bound <see cref="IViewModel"/> is received,
    /// and deinitializes it on unbind.
    /// </summary>
    public class ViewBinder : Binder, IBinder<IViewModel?>
    {
        /// <summary>
        /// The <see cref="IView"/> instance that this binder manages.
        /// </summary>
        protected readonly IView View;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewBinder"/> class with the specified view.
        /// </summary>
        /// <param name="view">The view that will be bound to the ViewModel.</param>
        public ViewBinder(IView view)
        {
            View = view;
        }

        /// <summary>
        /// Deinitializes the current view and reinitializes it with the received ViewModel value.
        /// If <paramref name="viewModel"/> is <see langword="null"/>, only deinitialization is performed.
        /// </summary>
        /// <param name="viewModel">The new ViewModel, or <see langword="null"/> to deinitialize without reinitializing.</param>
        public void SetValue(IViewModel? viewModel)
        {
            DeinitializeView();

            if (viewModel is not null)
                InitializeView(viewModel);
        }

        /// <summary>
        /// Called after unbinding. Deinitializes the view.
        /// </summary>
        protected override void OnUnbound() =>
            DeinitializeView();

        /// <summary>
        /// Initializes <see cref="View"/> with <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel to initialize the view with.</param>
        protected void InitializeView(IViewModel viewModel) =>
            View.Initialize(viewModel);

        /// <summary>
        /// Deinitializes the <see cref="View"/>, clearing its current ViewModel.
        /// </summary>
        protected void DeinitializeView() =>
            View.Deinitialize();
    }
}
