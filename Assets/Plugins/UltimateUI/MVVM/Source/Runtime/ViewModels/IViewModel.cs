// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    public interface IViewModel
    {
        public IReadOnlyBindsMethods GetBindMethods();
        
        public IReadOnlyBindsMethods GetUnbindsMethods();
    }
}