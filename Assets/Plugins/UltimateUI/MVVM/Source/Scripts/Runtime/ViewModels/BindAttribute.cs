using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class BindAttribute : Attribute
    {
        public BindAttribute(Access access = Access.Private) { }
    }
}