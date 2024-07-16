using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.ViewModels;
using UnityEngine.Profiling;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewBinders
{
    public static class ViewBinder
    {
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _bindMarker = new($"{nameof(ViewBinder)}.{nameof(Bind)}");
        private static readonly Unity.Profiling.ProfilerMarker _unbindMarker = new($"{nameof(ViewBinder)}.{nameof(Unbind)}");
        private static readonly Unity.Profiling.ProfilerMarker _rebindMarker = new($"{nameof(ViewBinder)}.{nameof(Rebind)}");
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
                Profiler.BeginSample("Foreach");
                foreach (var (key, binders) in view.GetBindersLazy())
                {
                    for (var i = 0; i < binders.Count; i++)
                        binders[i].Bind(viewModel, key);
                }
                Profiler.EndSample();
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