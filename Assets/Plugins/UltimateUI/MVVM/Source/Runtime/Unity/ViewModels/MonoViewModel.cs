using System;
using UnityEngine;
using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Unity.ViewModels
{
    public abstract class MonoViewModel : MonoBehaviour, IViewModel
    {
        public void AddBinder(IBinder binder, string propertyName) =>
            throw new NotImplementedException("This method must be implemented in the inheritor");

        public void RemoveBinder(IBinder binder, string propertyName) =>
            throw new NotImplementedException("This method must be implemented in the inheritor");
    }
}