using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public interface IBinder
    {
        public bool IsReverseEnabled => false;
        
        // TODO Delete
        public void Bind(IViewModel viewModel, string id);

        public void OnBound() { }
        
        // TODO Delete
        public void Unbind(IViewModel viewModel, string id);

        public void OnUnbound() { }
    }
    
    public interface IBinder<in T> : IBinder
    {
        public void SetValue(T value);
    }
}