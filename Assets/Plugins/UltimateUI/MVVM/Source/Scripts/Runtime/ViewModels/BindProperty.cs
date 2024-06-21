using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class BindProperty : Attribute
    {
        public BindProperty(string changedName, string id = "") { }
    }
}