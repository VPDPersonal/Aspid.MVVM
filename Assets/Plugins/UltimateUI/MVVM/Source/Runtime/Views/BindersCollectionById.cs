using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    public class BindersCollectionById : Dictionary<string, IReadOnlyList<IBinder>>, IReadOnlyBindersCollectionById { }

}