using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public sealed class BindAlsoAttribute : Attribute
    {
        public BindAlsoAttribute(string name) { }
    }
}