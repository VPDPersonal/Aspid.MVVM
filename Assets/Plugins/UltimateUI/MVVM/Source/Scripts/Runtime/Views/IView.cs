using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    public interface IView
    {
        IReadOnlyDictionary<string, IReadOnlyList<IBinder>> GetBinders();
    }
}