using Aspid.MVVM.Unity;

namespace Aspid.MVVM.ExampleScripts.Views
{
    [View]
    public partial class OnExecuteView : MonoView
    {
        public IBinder<int> SomeBinder => GetComponent<IBinder<int>>();
        
        // Partial method that is called before the ViewModel initialization process begins.
        partial void OnInitializingInternal(IViewModel viewModel) { }

        // Partial method that is called after the ViewModel has been initialized.
        partial void OnInitializedInternal(IViewModel viewModel) { }

        // Partial method that is called before the deinitialization process begins.
        partial void OnDeinitializingInternal() { }

        // Partial method that is called after the ViewModel has been deinitialized.
        partial void OnDeinitializedInternal() { }

        // Partial method that is called before the binders are instantiated.
        partial void OnInstantiatingBinders() { }

        // Partial method that is called after the binders have been instantiated.
        partial void OnInstantiatedBinders() { }
    }
}