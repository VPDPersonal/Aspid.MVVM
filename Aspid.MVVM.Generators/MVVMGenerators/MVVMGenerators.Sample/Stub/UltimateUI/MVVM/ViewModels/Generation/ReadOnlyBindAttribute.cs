using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels.Generation
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ReadOnlyBindAttribute : Attribute { }
}