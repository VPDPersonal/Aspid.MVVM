

using System;
// ReSharper disable once CheckNamespace
using System.Collections.Generic;

namespace UltimateUI.MVVM.ViewModels
{
    public interface IViewModel
    {
        public void AddBinder(IBinder binder, string propertyName);

        // public void AddBinder(ReadOnlySpan<IBinder> binders, string propertyName) { }
        
        public void AddBinder(IReadOnlyList<IBinder> binders, string propertyName) { }
        
        public void RemoveBinder(IBinder binder, string propertyName);

        // public void RemoveBinder(ReadOnlySpan<IBinder> binder, string propertyName);
        //
        // public void RemoveBinder(IReadOnlyCollection<IBinder> binder, string propertyName);
    }
}