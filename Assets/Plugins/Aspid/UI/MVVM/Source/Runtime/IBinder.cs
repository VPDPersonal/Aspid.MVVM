using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM
{
    public interface IBinder
    {
        public bool IsReverseEnabled => false;
        
        public void Bind(IViewModel viewModel, string id);
        
        public void Unbind(IViewModel viewModel, string id);
    }
    
    public interface IBinder<in T> : IBinder
    {
        public void SetValue(T value);
    }
}