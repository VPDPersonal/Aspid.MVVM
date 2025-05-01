namespace Aspid.MVVM.StarterKit
{
    public sealed class EmptyViewModel : IViewModel
    {
        public BindResult AddBinder(IBinder binder, string propertyName) => default;
    }
}