using System;

namespace AspidUI.MVVM.Views.Generation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class ViewAttribute : Attribute { }
}