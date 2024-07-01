namespace MVVMGenerators.Descriptions.UnityEngine;

public static class ClassesDescription
{
    public static readonly TypeDescription Object = new("Object", NamespacesDescription.UnityEngine);
    public static readonly TypeDescription Component = new("Component", NamespacesDescription.UnityEngine);
    public static readonly TypeDescription MonoBehaviour = new("MonoBehaviour", NamespacesDescription.UnityEngine);
    
    public static readonly TypeDescription Button = new("Button", NamespacesDescription.UnityEngine_UI);
    public static readonly TypeDescription Toggle = new("Toggle", NamespacesDescription.UnityEngine_UI);
    public static readonly TypeDescription Slider = new("Slider", NamespacesDescription.UnityEngine_UI);
    public static readonly TypeDescription ScrollRect = new("ScrollRect", NamespacesDescription.UnityEngine_UI);
}