using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    public interface IView
    {
        public void Initialize(IViewModel viewModel);

        public void Deinitialize(IViewModel viewModel);
    }
} 