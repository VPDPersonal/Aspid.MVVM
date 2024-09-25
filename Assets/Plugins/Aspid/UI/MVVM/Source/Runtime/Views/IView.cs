using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Views
{
    public interface IView
    {
        public IViewModel? ViewModel { get; }
        
        public void Initialize(IViewModel viewModel);

        public void Deinitialize();
    }
} 