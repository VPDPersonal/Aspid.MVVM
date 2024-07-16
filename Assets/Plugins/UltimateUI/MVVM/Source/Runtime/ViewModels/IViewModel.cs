// ReSharper disable once CheckNamespace

namespace UltimateUI.MVVM.ViewModels
{
    /// <summary>
    /// Interface representing a ViewModel that provides methods to retrieve bind and unbind methods.
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Retrieves the methods that handle binding.
        /// </summary>
        /// <returns>A read-only collection of methods responsible for binding.</returns>
        public IReadOnlyBindsMethods GetBindMethods();
        
        /// <summary>
        /// Retrieves the methods that handle unbinding.
        /// </summary>
        /// <returns>A read-only collection of methods responsible for unbinding.</returns>
        public IReadOnlyBindsMethods GetUnbindMethods();
        
        public void AddBinder(IBinder binder, string propertyName) { }

        public void RemoveBinder(IBinder binder, string propertyName) { }
    }
}