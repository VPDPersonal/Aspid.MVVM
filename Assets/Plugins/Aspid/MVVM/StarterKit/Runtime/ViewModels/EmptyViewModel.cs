namespace Aspid.MVVM.StarterKit.ViewModels
{
    public sealed class EmptyViewModel : IViewModel
    {
        public IRemoveBinderFromViewModel AddBinder(IBinder binder, string propertyName) => default;
    }
}