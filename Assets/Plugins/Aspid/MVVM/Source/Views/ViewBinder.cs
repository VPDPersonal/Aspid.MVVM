namespace Aspid.MVVM;

/// <summary>
/// Binds an <see cref="IView"/> to its <see cref="IViewModel"/>.
/// Implements the <see cref="IBinder{T}"/> interface to work with <see cref="IViewModel"/> objects.
/// </summary>
public sealed class ViewBinder : Binder, IBinder<IViewModel?>
{
    private readonly IView _view;
    private readonly bool _isDisposeViewModel;

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewBinder"/> class with the specified view and an option
    /// for automatically disposing of the ViewModel resources.
    /// </summary>
    /// <param name="view">The view that will be bound to the ViewModel.</param>
    /// <param name="isDisposeViewModel">Indicates whether the ViewModel's resources should be disposed upon deinitialization.</param>
    public ViewBinder(IView view, bool isDisposeViewModel = false)
    {
        _view = view;
        _isDisposeViewModel = isDisposeViewModel;
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
            _view.Initialize(viewModel);
    }
        
    protected override void OnUnbound() => 
        DeinitializeView();
        
    private void DeinitializeView()
    {
        var viewModel = _view.DeinitializeView();
        if (_isDisposeViewModel) viewModel?.DisposeViewModel();
    }
}