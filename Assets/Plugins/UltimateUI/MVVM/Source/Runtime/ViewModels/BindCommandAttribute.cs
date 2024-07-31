using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class BindCommandAttribute : Attribute
    {
        public BindCommandAttribute(string canExecute = "") { }
    }
}