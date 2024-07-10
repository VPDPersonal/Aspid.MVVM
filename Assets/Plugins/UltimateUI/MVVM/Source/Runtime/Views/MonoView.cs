using System;
using UnityEngine;
using Unity.Profiling;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    public abstract class MonoView : MonoBehaviour, IView
    {
        private static readonly ProfilerMarker _getBindersMarker = new($"{nameof(MonoView)}.{nameof(GetBinders)}");
        
        protected virtual void OnValidate() =>
            ViewUtility.ValidateBinders(this);

        IReadOnlyBindersCollectionById IView.GetBinders()
        {
            using (_getBindersMarker.Auto())
                return GetBinders();
        }

        protected virtual IReadOnlyBindersCollectionById GetBinders() =>
            throw new Exception("This method must be implemented in the inheritor");
    }
}