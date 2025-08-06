namespace Aspid.MVVM.Samples.ExampleScripts.Views.Handlers
{
    [View]
    public partial class Ex1InitializeHandlersView
    {
        partial void OnInitializingInternal(IViewModel viewModel)
        {
            // Called before initialization
        }

        partial void OnInitializedInternal(IViewModel viewModel)
        {
            // Called after initialization
        }

        partial void OnDeinitializingInternal()
        {
            // Called before deinitialization
        }

        partial void OnDeinitializedInternal()
        {
            // Called after deinitialization
        }
    }
}