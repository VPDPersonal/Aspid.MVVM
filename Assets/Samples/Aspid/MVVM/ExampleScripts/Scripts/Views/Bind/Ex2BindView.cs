namespace Aspid.MVVM.ExampleScripts.Views.Bind
{
    [View]
    public partial class Ex2BindView
    {
        private Binder PropertyBinder { get; }
        
        private Binder[] PropertyBinders { get; }
    }
}

//  public partial class Ex2BindView : IView
//  {
//      private Binder __propertyBinderCachedPropertyBinder;
//      private Binder[] __propertyBindersCachedPropertyBinder;
//
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
//          InstantiateBinders();
//              
//          __propertyBinderCachedPropertyBinder.BindSafely(viewModel.FindBindableMember(new(Ids.PropertyBinder)));
//          __propertyBindersCachedPropertyBinder.BindSafely(viewModel.FindBindableMember(new(Ids.PropertyBinders)));
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
//          __propertyBinderCachedPropertyBinder.UnbindSafely();
//          __propertyBindersCachedPropertyBinder.UnbindSafely();
//              
//          OnDeinitializedInternal();
//      }
//  	
//  	partial void OnDeinitializingInternal();
//  	
//  	partial void OnDeinitializedInternal();
//      
//  	private void InstantiateBinders()
//      {
//          OnInstantiatingBinders();

//          __propertyBinderCachedPropertyBinder ??= PropertyBinder;
//          __propertyBindersCachedPropertyBinder ??= PropertyBinders;
//          
//          OnInstantiatedBinders();
//      }
//  	
//  	partial void OnInstantiatingBinders();
//  	
//  	partial void OnInstantiatedBinders();
//  }