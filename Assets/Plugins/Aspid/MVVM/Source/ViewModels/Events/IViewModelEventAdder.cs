namespace Aspid.MVVM
{
    public interface IViewModelEventAdder
    {
        public IViewModelEventRemover? AddBinder(IBinder binder);
    }
}