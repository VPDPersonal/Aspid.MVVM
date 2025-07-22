using System;
using System.Diagnostics;

namespace Aspid.MVVM.Unity
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