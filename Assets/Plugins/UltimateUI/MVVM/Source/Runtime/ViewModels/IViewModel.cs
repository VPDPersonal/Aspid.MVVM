// ReSharper disable once CheckNamespace

namespace UltimateUI.MVVM.ViewModels
{
    public interface IViewModel
    {
        public void AddBinder(IBinder binder, string propertyName);

        public void RemoveBinder(IBinder binder, string propertyName);
    }
}