using System;

namespace Aspid.UI.MVVM.Generation
{
    // TODO Move To UnityFastTools
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public class CreateFromAttribute : Attribute
    {
        public CreateFromAttribute(Type type) { }
    }
}