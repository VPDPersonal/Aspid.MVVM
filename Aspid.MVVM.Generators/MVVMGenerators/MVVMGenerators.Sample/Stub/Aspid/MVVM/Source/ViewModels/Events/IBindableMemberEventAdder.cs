namespace Aspid.MVVM
{
    public interface IBindableMemberEventAdder
    {
        public IBindableMemberEventRemover? Add(IBinder binder);
    }
}