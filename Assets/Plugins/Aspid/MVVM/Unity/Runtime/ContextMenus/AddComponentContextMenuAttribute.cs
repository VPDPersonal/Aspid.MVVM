using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedParameter.Local
namespace Aspid.MVVM
{
    // TODO Move To UnityFastTools
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Class)]
    public class AddComponentContextMenuAttribute : Attribute
    {
        public int Priority { get; set; }

        public AddComponentContextMenuAttribute(Type componentType, string path = null) { }
    }
}