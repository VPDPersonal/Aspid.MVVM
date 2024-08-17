using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels.Generation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class ViewModelAttribute : Attribute { }
}