using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    public abstract class MonoViewModel : MonoBehaviour, IViewModel
    {
        public virtual IReadOnlyBindsMethods GetBindMethods() =>
            throw new NotImplementedException("This method must be implemented in the inheritor");

        public virtual IReadOnlyBindsMethods GetUnbindsMethods() =>
            throw new NotImplementedException("This method must be implemented in the inheritor");
    }
}