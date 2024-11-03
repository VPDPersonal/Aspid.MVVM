using System;

namespace Aspid.UI.MVVM.Generation
{
    // TODO Move To UnityFastTools
    /// <summary>
    /// Атрибут-маркер для классов и структур.
    /// Используется Source Generator для генерации методов расширения преобразования 
    /// из типа, указанного в атрибуте, в тип, к которому присоединен атрибут.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public class CreateFromAttribute : Attribute
    {
        public CreateFromAttribute(Type type) { }
    }
}