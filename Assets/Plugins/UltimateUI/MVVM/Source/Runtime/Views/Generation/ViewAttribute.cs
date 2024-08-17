using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views.Generation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class ViewAttribute : Attribute { }
}