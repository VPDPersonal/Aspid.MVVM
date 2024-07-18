using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.ViewModels;
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
using Unity.Profiling;
#endif

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewBinders
{
    public static class ViewBinder
    {
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly ProfilerMarker _bindMarker = new("ViewBinder.Bind");
        private static readonly ProfilerMarker _unbindMarker = new("ViewBinder.Unbind");
        private static readonly ProfilerMarker _rebindMarker = new("ViewBinder.Rebind");
#endif
        
        public static void Rebind(IView view, IViewModel oldViewModel, IViewModel newViewModel)
        {
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_rebindMarker.Auto())
#endif
            {
                Unbind(view, oldViewModel);
                Bind(view, newViewModel);
            }
        }
        
        public static void Bind(IView view, IViewModel viewModel)
        {
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto())
#endif
            {
                var binders = view.GetBinders();
                var bindMethods = viewModel.GetBindMethods();

                foreach (var pair in binders)
                {
                    if (bindMethods.TryGetValue(pair.Key, out var bindMethod))
                        bindMethod(pair.Value);
                }
            }
        }

        public static void Unbind(IView view, IViewModel viewModel)
        {
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_unbindMarker.Auto())
#endif
            {
                var binders = view.GetBinders();
                var unbindMethods = viewModel.GetUnbindMethods();
            
                foreach (var pair in binders)
                {
                    if (unbindMethods.TryGetValue(pair.Key, out var unbindMethod))
                        unbindMethod(pair.Value);
                }
            }
        }
    }
}