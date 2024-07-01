using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    public interface IReadOnlyBindsMethods : IReadOnlyDictionary<string, Action<IReadOnlyCollection<IBinder>>> { }
}