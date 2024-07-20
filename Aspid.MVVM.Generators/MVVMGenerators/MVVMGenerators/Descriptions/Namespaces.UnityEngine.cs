using MVVMGenerators.Helpers;

namespace MVVMGenerators.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Namespaces
{
    public static readonly NamespaceText Unity = new("Unity");
    public static readonly NamespaceText Unity_Profiling = new("Profiling", Unity);
    
    public static readonly NamespaceText UnityEngine = new("UnityEngine");
    public static readonly NamespaceText UnityEngine_UI = new("UI", UnityEngine);
}