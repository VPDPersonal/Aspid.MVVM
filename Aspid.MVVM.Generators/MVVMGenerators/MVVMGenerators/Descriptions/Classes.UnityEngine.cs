using MVVMGenerators.Helpers;

namespace MVVMGenerators.Descriptions;

public static partial class Classes
{
    public static readonly TypeText Object = new("Object", Namespaces.UnityEngine);
    public static readonly TypeText Component = new("Component", Namespaces.UnityEngine);
    public static readonly TypeText MonoBehaviour = new("MonoBehaviour", Namespaces.UnityEngine);
    public static readonly AttributeText SerializeFieldAttribute = new("SerializeField", Namespaces.UnityEngine);
    
    public static readonly TypeText Button = new("Button", Namespaces.UnityEngine_UI);
    public static readonly TypeText Toggle = new("Toggle", Namespaces.UnityEngine_UI);
    public static readonly TypeText Slider = new("Slider", Namespaces.UnityEngine_UI);
    public static readonly TypeText ScrollRect = new("ScrollRect", Namespaces.UnityEngine_UI);
}