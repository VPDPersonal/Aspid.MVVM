using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedParameter.Local
namespace Aspid.MVVM
{
    // TODO Move To UnityFastTools
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Class)]
    public class AddBinderContextMenuAttribute : Attribute
    {
        public Type Type { get; }
        
        public string Path { get; set; }
        
        public string[] SerializePropertyNames { get; }

        public AddBinderContextMenuAttribute(Type type, params string[] serializePropertyNames)
        {
            Type = type;
            SerializePropertyNames = serializePropertyNames;
        }
    }
}