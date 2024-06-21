using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class AccessorsAttribute : Attribute
    {
        public string Get { get; set; }
        
        public string Set { get; set; }
        
        public AccessorsAttribute(Access access = Access.Private) { }
    }
}