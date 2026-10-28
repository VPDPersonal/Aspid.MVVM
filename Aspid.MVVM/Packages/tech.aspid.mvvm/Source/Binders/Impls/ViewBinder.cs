// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="Binder"/> that initializes an <see cref="IView"/> with each ViewModel it receives.
    /// </summary>
    public class ViewBinder : Binder, IBinder<IViewModel?>
    {
        /// <summary>
        /// The view the binder initializes.
        /// </summary>
        protected readonly IView View;

        /// <summary>
        /// Creates a binder for <paramref name="view"/>.
        /// </summary>
        /// <param name="view">The view to bind.</param>
        public ViewBinder(IView view)
        {
            View = view;
        }

        /// <summary>
        /// Deinitializes <see cref="View"/> and initializes it with <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel received from the bindable member; <see langword="null"/> leaves the view deinitialized.</param>
        public void SetValue(IViewModel? viewModel)
        {
            DeinitializeView();

            if (viewModel is not null)
                InitializeView(viewModel);
        }

        /// <summary>
        /// Called after the binder detaches to deinitialize <see cref="View"/>.
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
        /// Deinitializes <see cref="View"/>.
        /// </summary>
        protected void DeinitializeView() =>
            View.Deinitialize();
    }
}
