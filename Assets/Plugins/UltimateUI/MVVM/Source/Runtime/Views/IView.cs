// ReSharper disable once CheckNamespace

using System.Collections.Generic;

namespace UltimateUI.MVVM.Views
{
    public interface IView
    {
        public IReadOnlyBindersCollectionById GetBinders();
        
        public IEnumerable<(string id, IReadOnlyList<IBinder> binders)> GetBindersLazy() { yield break; }
    }
} 