namespace Aspid.MVVM.ExampleScripts.Views.Handlers
{
    [View]
    public partial class Ex2InstantiatingHandlersView
    {
        // It will be cached
        private Binder PropertyBinder { get; }

        partial void OnInstantiatingBinders()
        {
            // Called before caching the required binders
        }

        partial void OnInstantiatedBinders()
        {
            // Called after caching the necessary binders
        }
    }
}