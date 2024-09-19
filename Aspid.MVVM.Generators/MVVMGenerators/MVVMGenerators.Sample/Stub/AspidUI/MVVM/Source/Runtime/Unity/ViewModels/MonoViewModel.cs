using System;
using UnityEngine;
using AspidUI.MVVM.ViewModels;

namespace AspidUI.MVVM.Unity.ViewModels
{
    public abstract class MonoViewModel : MonoBehaviour, IViewModel
    {
        public void AddBinder(IBinder binder, string propertyName) =>
            throw new NotImplementedException("This method must be implemented in the inheritor");

        public void RemoveBinder(IBinder binder, string propertyName) =>
            throw new NotImplementedException("This method must be implemented in the inheritor");
    }
}