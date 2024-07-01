using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Commands
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class BindCommandAttribute : Attribute
    {
        public string? CanExecute { get; set; }
    }
}