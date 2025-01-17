namespace Aspid.MVVM.StarterKit.ViewModels
{
    public interface IDynamicProperty
    {
        public IRemoveBinderFromViewModel AddBinder(IBinder binder);
    }
}