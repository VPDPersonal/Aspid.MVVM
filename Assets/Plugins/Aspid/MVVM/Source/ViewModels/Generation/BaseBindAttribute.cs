using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Serves as the base class for all binding-related attributes.
    /// Derive from this class to implement custom binding attributes for use with ViewModels.
    /// This class itself does not contain any logic and is used primarily as a marker for attribute hierarchy.
    /// Classes that inherit from <see cref="BaseBindAttribute"/> must be manually added to the Source Generator 
    /// to generate the appropriate binding logic. This process does not happen automatically.
    /// </summary>
    public abstract class BaseBindAttribute : Attribute { }
}