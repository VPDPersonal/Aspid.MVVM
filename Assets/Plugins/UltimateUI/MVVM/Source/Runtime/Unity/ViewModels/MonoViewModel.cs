using System;
using UnityEngine;
using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Unity.ViewModels
{
    public abstract class MonoViewModel : MonoBehaviour, IViewModel
    {
        public virtual IReadOnlyBindsMethods GetBindMethods() =>
            throw new NotImplementedException("This method must be implemented in the inheritor");

        public virtual IReadOnlyBindsMethods GetUnbindMethods() =>
            throw new NotImplementedException("This method must be implemented in the inheritor");
    }
}