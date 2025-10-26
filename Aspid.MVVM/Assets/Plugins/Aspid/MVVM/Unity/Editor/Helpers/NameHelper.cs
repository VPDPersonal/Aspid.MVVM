using System.Reflection;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static class NameHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetGeneratedPropertyName(this FieldInfo field)
            => GetGeneratedPropertyName(field.Name);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetGeneratedPropertyName(this string fieldName)
        {
            fieldName = fieldName.Remove(0, GetPrefixCount(fieldName));
            
            var firstSymbol = fieldName[0];
            if (char.IsLower(firstSymbol))
            {
                fieldName = fieldName.Remove(0, 1);
                fieldName = char.ToUpper(firstSymbol) + fieldName;
            }

            return fieldName;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetPrefixCount(this FieldInfo field)
            => GetPrefixCount(field.Name);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetPrefixCount(this string name)
        {
            if (name.StartsWith("__") || name.StartsWith("m_")) return 2;
            return name.StartsWith("_") ? 1 : 0;
        }
    }
}