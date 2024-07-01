using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    public sealed class BindMethods : Dictionary<string, Action<IReadOnlyCollection<IBinder>>>, IReadOnlyBindsMethods { }
}