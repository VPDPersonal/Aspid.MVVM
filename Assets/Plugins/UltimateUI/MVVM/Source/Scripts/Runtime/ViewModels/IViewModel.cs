using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    public interface IViewModel
    {
        public IReadOnlyDictionary<string, Action<IReadOnlyCollection<IBinder>>> GetBindMethods();
        
        public IReadOnlyDictionary<string, Action<IReadOnlyCollection<IBinder>>> GetUnbindsMethods();
    }
}