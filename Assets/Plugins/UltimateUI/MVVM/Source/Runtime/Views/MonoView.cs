using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    public abstract class MonoView : MonoBehaviour, IView
    {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _getBindersMarker = new($"{nameof(MonoView)}.{nameof(GetBinders)}");
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

        public abstract IEnumerable<(string id, IReadOnlyList<IBinder> binders)> GetBindersLazy();

        protected virtual IReadOnlyBindersCollectionById GetBinders() =>
            throw new Exception("This method must be implemented in the inheritor");
    }
}