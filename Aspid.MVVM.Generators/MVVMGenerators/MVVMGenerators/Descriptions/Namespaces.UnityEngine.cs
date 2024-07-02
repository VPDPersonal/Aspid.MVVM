using MVVMGenerators.Helpers;

namespace MVVMGenerators.Descriptions;

// ReSharper disable InconsistentNaming
public static partial class Namespaces
{
    public static readonly NamespaceText UnityEngine = new("UnityEngine");
    public static readonly NamespaceText UnityEngine_UI = new("UI", UnityEngine);
}