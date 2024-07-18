using UltimateUI.MVVM.ViewModels;

namespace UltimateUI.MVVM.Views
{
    public interface IView
    {
        public void Initialize(IViewModel viewModel);
    }

    public interface IView<in T> : IView
        where T : IViewModel
    {
        public void Initialize(T viewModel);
    }
} 