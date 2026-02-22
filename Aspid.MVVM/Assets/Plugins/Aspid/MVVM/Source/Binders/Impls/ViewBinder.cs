// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Binds an <see cref="IView"/> to its <see cref="IViewModel"/>.
    /// Implements the <see cref="IBinder{T}"/> interface to work with <see cref="IViewModel"/> objects.
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
        /// Sets the ViewModel for the bound view.
        /// Deinitializes the current view before setting the new ViewModel.
        /// </summary>
        /// <param name="viewModel">The ViewModel to bind to the view.</param>
        public void SetValue(IViewModel? viewModel)
        {
            DeinitializeView();

            if (viewModel is not null)
                InitializeView(viewModel);
        }

        /// <inheritdoc />
        /// <summary>
        /// Deinitializes the view when the binder is unbound.
        /// </summary>
        protected override void OnUnbound() =>
            DeinitializeView();

        /// <summary>
        /// Initializes the <see cref="View"/> with the specified <see cref="IViewModel"/>.
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