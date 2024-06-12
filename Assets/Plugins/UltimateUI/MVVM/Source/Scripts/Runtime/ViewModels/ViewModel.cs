using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    public abstract class ViewModel : MonoBehaviour, IViewModel
    {
        public IReadOnlyDictionary<string, Action<IReadOnlyCollection<IBinder>>> GetBinds() =>
            throw new Exception("This method must be implemented in the inheritor");
        
        public IReadOnlyDictionary<string, Action<IReadOnlyCollection<IBinder>>> GetUnbinds() =>
            throw new Exception("This method must be implemented in the inheritor");
    }
}