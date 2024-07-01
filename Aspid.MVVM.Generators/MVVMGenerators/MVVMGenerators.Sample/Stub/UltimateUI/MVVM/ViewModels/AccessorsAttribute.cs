using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class AccessorsAttribute : Attribute
    {
        public Access Get { get; set; }
        
        public Access Set { get; set; }
        
        public AccessorsAttribute(Access access = Access.Private) { }
    }
}