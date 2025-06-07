using System;
using System.Diagnostics;

namespace Aspid.MVVM.Unity
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Class)]
    public class AddComponentContextMenuAttribute : Attribute
    {
        public int Priority { get; set; }

        public AddComponentContextMenuAttribute(Type componentType, string path = null) { }
    }
}