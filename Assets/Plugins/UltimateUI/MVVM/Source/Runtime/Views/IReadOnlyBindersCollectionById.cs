using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    public interface IReadOnlyBindersCollectionById : IReadOnlyDictionary<string, IReadOnlyList<IBinder>> { }
}