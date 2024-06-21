using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class ViewModelAttribute : Attribute { }
}