// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Binds an <see cref="IView"/> to its <see cref="IViewModel"/>.
    /// Implements the <see cref="IBinder{T}"/> interface to work with <see cref="IViewModel"/> objects.
    /// </summary>
    public class ViewBinder : Binder, IBinder<IViewModel?>
    {
        protected readonly IView View;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewBinder"/> class with the specified view
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
        
        protected override void OnUnbound() => 
            DeinitializeView();
        
        protected void InitializeView(IViewModel viewModel) => 
            View.Initialize(viewModel);
        
        protected void DeinitializeView() =>
            View.Deinitialize();
    }
}