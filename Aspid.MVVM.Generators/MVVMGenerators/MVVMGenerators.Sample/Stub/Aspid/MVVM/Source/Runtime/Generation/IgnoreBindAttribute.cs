using System;

namespace Aspid.MVVM.Generation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class IgnoreBindAttribute : Attribute { }
}