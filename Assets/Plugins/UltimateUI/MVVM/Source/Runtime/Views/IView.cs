using UltimateUI.MVVM.ViewModels;

namespace UltimateUI.MVVM.Views
{
    public interface IView
    {
        public void Initialize(IViewModel viewModel);

        public void Deinitialize(IViewModel viewModel);
    }
} 