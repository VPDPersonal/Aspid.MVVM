using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    public class TypeSelectorAttribute : PropertyAttribute
    {
        public readonly TypeSelectorMode Mode;
        
        public TypeSelectorAttribute(TypeSelectorMode typeSelectorMode = TypeSelectorMode.AssemblyQualifiedName)
        {
            Mode = typeSelectorMode;
        }
    }
}