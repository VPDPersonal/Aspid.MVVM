using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.ViewModels
{
    /// <summary>
    /// An attribute indicating that the given class or structure represents a ViewModel and should be processed by the source code generator.
    /// The generator will work with other attributes within, such as the Bind.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class ViewModelAttribute : Attribute { }
}