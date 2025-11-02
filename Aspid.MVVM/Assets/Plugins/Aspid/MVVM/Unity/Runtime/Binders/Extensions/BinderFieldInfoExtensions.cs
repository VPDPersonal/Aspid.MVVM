using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM – Rewrite comments 
    public static class BinderFieldInfoExtensions
    {
        /// <summary>
        /// Generates an ID for a binder based on its field name.
        /// Removes common prefixes like "_" or "m_" and ensures the first character is uppercase.
        /// </summary>
        /// <param name="field">The field of the binder.</param>
        /// <returns>The processed ID string based on the field name.</returns>
        public static string GetBinderId(this FieldInfo field, object target)
        {
            var attributes = field.GetCustomAttributes<RequireBinderAttribute>(false);

            var fieldName = attributes
                .Select(attribute => attribute.Name)
                .FirstOrDefault(name => name is not null);

            if (fieldName is null)
            {
                return GetBinderId(field.Name);
            }
            
            var nameMemberInfos = target.GetType().GetMember(fieldName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (nameMemberInfos.Length > 1) return GetBinderId(field.Name);

            var memberValue = nameMemberInfos.First() switch
            {
                FieldInfo fieldInfo => fieldInfo.GetValue(target) as string,
                PropertyInfo propertyInfo => propertyInfo.GetValue(target) as string,
                _ => null
            };
                
            return memberValue ?? GetBinderId(field.Name);
        }

        /// <summary>
        /// Generates an ID for a binder based on its field name.
        /// Removes common prefixes like "_" or "m_" and ensures the first character is uppercase.
        /// </summary>
        /// <param name="fieldName">The original field name of the binder.</param>
        /// <returns>The processed ID string based on the field name.</returns>
        public static string GetBinderId(string fieldName)
        {
            var prefixCount = GetPrefixCount();
            fieldName = fieldName.Remove(0, prefixCount);
        
            var firstSymbol = fieldName[0];
            if (char.IsLower(firstSymbol))
            {
                fieldName = fieldName.Remove(0, 1);
                fieldName = char.ToUpper(firstSymbol) + fieldName;
            }
        
            return fieldName;
        
            int GetPrefixCount() => fieldName.StartsWith("_") ? 1 : fieldName.StartsWith("m_") ? 2 : 0;
        }
    }
}