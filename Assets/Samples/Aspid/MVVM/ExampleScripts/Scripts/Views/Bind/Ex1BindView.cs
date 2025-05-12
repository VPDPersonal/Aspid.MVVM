namespace Aspid.MVVM.ExampleScripts.Views.Bind
{
    [View]
    public partial class Ex1BindView
    {
        private Binder _binder;
        private Binder[] _binders;
    }
}

//  Generated Code:
//  public partial class Ex1BindView : IView
//  {
//  	public IViewModel ViewModel { get; private set; }
//  	
//  	public void Initialize(IViewModel viewModel)
//  	{
//  	    if (viewModel is null) throw new ArgumentNullException(nameof(viewModel));
//  	    if (ViewModel is not null) throw new InvalidOperationException("View is already initialized.");
//  	    
//  	    ViewModel = viewModel;
//  	    InitializeInternal(viewModel);
//  	}
//  	
//  	protected virtual void InitializeInternal(IViewModel viewModel)
//      {
//          OnInitializingInternal(viewModel);
//              
//          _binder.BindSafely(viewModel.FindBindableMember(new(Ids.Binder)));
//          _binders.BindSafely(viewModel.FindBindableMember(new(Ids.Binders)));
//              
//          OnInitializedInternal(viewModel);
//      }
//  	
//  	partial void OnInitializingInternal(IViewModel viewModel);
//  	
//  	partial void OnInitializedInternal(IViewModel viewModel);
//      
//  	public void Deinitialize()
//  	{
//  	    if (ViewModel is null) return;
//  	
//  	    DeinitializeInternal();
//  	    ViewModel = null;
//  	}
//  	
//  	protected virtual void DeinitializeInternal()
//      {
//          OnDeinitializingInternal();
//              
//          _binder.UnbindSafely();
//          _binders.UnbindSafely();
//              
//          OnDeinitializedInternal();
//      }
//  	
//  	partial void OnDeinitializingInternal();
//  	
//  	partial void OnDeinitializedInternal();
//  }