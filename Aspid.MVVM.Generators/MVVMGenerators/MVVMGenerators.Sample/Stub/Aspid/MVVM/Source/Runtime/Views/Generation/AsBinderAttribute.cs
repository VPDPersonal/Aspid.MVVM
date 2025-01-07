using System;

namespace Aspid.MVVM.Generation
{
    /// <summary>
    /// Marker attribute for fields and properties within a class or structure marked with the <see cref="ViewAttribute"/>.
    /// Used by the Source Generator to generate binding code based on the provided <see cref="IBinder"/> type in the View.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class AsBinderAttribute : Attribute
    {
        public AsBinderAttribute(Type type) { }
        
        public AsBinderAttribute(Type type, params object[] arguments) { }
    }
} 