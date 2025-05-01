namespace Aspid.MVVM.StarterKit
{
    public interface IDynamicProperty
    {
        public BindResult AddBinder(IBinder binder);
    }
}