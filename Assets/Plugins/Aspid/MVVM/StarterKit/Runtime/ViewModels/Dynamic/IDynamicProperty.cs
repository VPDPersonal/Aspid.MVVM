namespace Aspid.MVVM.StarterKit.ViewModels
{
    public interface IDynamicProperty
    {
        public BindResult AddBinder(IBinder binder);
    }
}