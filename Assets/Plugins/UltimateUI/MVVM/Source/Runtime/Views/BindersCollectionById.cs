using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    public sealed class BindersCollectionById : Dictionary<string, IReadOnlyList<IBinder>>, IReadOnlyBindersCollectionById { }

}