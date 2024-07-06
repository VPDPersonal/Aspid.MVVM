using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    public abstract class MonoView : MonoBehaviour, IView
    {
        protected virtual void OnValidate() =>
            ViewUtility.ValidateBinders(this);

        IReadOnlyBindersCollectionById IView.GetBinders() => GetBinders();

        protected virtual IReadOnlyBindersCollectionById GetBinders() =>
            throw new Exception("This method must be implemented in the inheritor");
    }
}