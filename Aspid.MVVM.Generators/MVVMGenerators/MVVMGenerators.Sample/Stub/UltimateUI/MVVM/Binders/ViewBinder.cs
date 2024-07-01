using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewBinders
{
    public static class ViewBinder
    {
        public static void Bind(IView view, IViewModel viewModel)
        {
            var binders = view.GetBinders();
            var bindMethods = viewModel.GetBindMethods();

            foreach (var pair in binders)
            {
                if (bindMethods.TryGetValue(pair.Key, out var bindMethod))
                    bindMethod(pair.Value);
            }
        }

        public static void Unbind(IView view, IViewModel viewModel)
        {
            var binders = view.GetBinders();
            var unbindMethods = viewModel.GetUnbindsMethods();
            
            foreach (var pair in binders)
            {
                if (unbindMethods.TryGetValue(pair.Key, out var unbindMethod))
                    unbindMethod(pair.Value);
            }
        }
    }
}