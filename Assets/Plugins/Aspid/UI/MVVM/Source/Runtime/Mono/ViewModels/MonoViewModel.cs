using System;
using UnityEngine;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Mono.ViewModels
{
    // Is this work? 
    public abstract class MonoViewModel : MonoBehaviour, IViewModel
    {
        public void AddBinder(IBinder binder, string propertyName) =>
            throw new NotImplementedException("This method must be implemented in the inheritor");

        public void RemoveBinder(IBinder binder, string propertyName) =>
            throw new NotImplementedException("This method must be implemented in the inheritor");
    }
}