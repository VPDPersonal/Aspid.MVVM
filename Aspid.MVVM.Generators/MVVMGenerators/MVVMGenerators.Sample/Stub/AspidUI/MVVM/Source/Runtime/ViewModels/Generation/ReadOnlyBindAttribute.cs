using System;

namespace AspidUI.MVVM.ViewModels.Generation
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ReadOnlyBindAttribute : Attribute { }
}