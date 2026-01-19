using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Move To UnityFastTools
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AddBinderContextMenuByTypeAttribute : Attribute
    {
        public Type Type { get; }

        public AddBinderContextMenuByTypeAttribute(Type componentType)
        {
            Type = componentType;
        }
    }
}
