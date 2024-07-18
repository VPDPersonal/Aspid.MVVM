using System;
using UnityEngine;
using UltimateUI.MVVM.Views;
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
using Unity.Profiling;
#endif

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Unity.Views
{
    public abstract class MonoView : MonoBehaviour, IView
    {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly ProfilerMarker _getBindersMarker = new($"{nameof(MonoView)}.{nameof(GetBinders)}");
#endif
        
        protected virtual void OnValidate() =>
            ViewUtility.ValidateBinders(this);

        IReadOnlyBindersCollectionById IView.GetBinders()
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_getBindersMarker.Auto())
#endif
            {
                return GetBinders();
            }
        }

        protected virtual IReadOnlyBindersCollectionById GetBinders() =>
            throw new Exception("This method must be implemented in the inheritor");
    }
}