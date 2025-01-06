using System;

namespace Aspid.MVVM.Generation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class BindIdAttribute : Attribute
    {
        public BindIdAttribute(string id) { }
    }
}