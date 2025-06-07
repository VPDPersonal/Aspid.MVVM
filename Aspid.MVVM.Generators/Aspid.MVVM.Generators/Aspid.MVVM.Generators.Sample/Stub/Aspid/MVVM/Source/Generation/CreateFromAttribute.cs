using System;

namespace Aspid.MVVM
{
    // TODO Move To UnityFastTools
    /// <summary>
    /// Marker attribute for constructors.
    /// Used by the Source Generator to generate extension methods for converting 
    /// from the type specified in the attribute to the type to which the attribute is attached.
    /// </summary>
    [Obsolete]
    [AttributeUsage(AttributeTargets.Constructor)]
    public class CreateFromAttribute : Attribute
    {
        public CreateFromAttribute(Type type) { }
    }
}