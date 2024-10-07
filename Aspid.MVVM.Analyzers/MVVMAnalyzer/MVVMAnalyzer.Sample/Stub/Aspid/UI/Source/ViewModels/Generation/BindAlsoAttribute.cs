using System;

namespace Aspid.UI.MVVM.ViewModels.Generation
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class BindAlsoAttribute : Attribute
    {
        public BindAlsoAttribute(string propertyName) { }
    }
}