using System;

namespace Aspid.UI.MVVM.ViewModels.Generation
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class AccessAttribute : Attribute
    {
        public Access Get { get; set; }
        
        public Access Set { get; set; }
        
        public AccessAttribute(Access access = Access.Private) { }
    }
}