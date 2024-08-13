using System;

namespace UltimateUI.MVVM.ViewModels
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class RelayCommandAttribute : Attribute
    {
        public string? CanExecute;
    }
}