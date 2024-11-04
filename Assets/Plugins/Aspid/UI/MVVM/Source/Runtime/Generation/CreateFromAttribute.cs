using System;

namespace Aspid.UI.MVVM.Generation
{
    // TODO Move To UnityFastTools
    /// <summary>
    /// Marker attribute for classes and structures.
    /// Used by the Source Generator to generate extension methods for converting 
    /// from the type specified in the attribute to the type to which the attribute is attached.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public class CreateFromAttribute : Attribute
    {
        public CreateFromAttribute(Type type) { }
    }
}