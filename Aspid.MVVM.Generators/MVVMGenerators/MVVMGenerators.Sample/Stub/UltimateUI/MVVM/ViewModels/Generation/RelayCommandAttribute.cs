using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels.Generation
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class RelayCommandAttribute : Attribute
    {
        public string? CanExecute;
    }
}