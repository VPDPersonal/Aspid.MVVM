using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views.Generation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class AsBinderAttribute : Attribute
    {
        public AsBinderAttribute(Type type, params object[] args) { }
    }
}