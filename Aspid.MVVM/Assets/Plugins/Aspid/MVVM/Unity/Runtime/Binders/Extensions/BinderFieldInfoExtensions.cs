// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides utility methods for deriving binder IDs from field names.
    /// </summary>
    public static class BinderFieldInfoExtensions
    {
        /// <summary>
        /// Derives a binder ID from a field name by stripping the <c>_</c> or <c>m_</c> prefix
        /// and capitalizing the first character.
        /// </summary>
        /// <param name="fieldName">The field name to convert.</param>
        /// <returns>The field name with the prefix removed and the first letter capitalized.</returns>
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