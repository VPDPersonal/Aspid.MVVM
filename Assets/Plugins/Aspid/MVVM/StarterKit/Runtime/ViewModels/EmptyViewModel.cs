namespace Aspid.MVVM.StarterKit.ViewModels
{
    public sealed class EmptyViewModel : IViewModel
    {
        public BindResult AddBinder(IBinder binder, string propertyName) => default;
    }
}