#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides utility methods for binder IDs.
    /// </summary>
    public static class BinderIdUtility
    {
        /// <summary>
        /// Derives a binder ID from a field name: strips the <c>_</c> or <c>m_</c> prefix and capitalizes the first character.
        /// </summary>
        /// <param name="fieldName">The field name.</param>
        /// <returns>The derived binder ID.</returns>
        public static string FromFieldName(string fieldName)
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
