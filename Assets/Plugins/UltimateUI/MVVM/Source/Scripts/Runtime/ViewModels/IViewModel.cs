using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    public interface IViewModel
    {
        public IReadOnlyDictionary<string, Action<IReadOnlyCollection<IBinder>>> GetBinds();
        
        public IReadOnlyDictionary<string, Action<IReadOnlyCollection<IBinder>>> GetUnbinds();
    }
}