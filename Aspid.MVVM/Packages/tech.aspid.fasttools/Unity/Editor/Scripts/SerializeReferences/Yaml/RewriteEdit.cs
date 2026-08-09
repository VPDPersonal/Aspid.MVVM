// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// A single computed line change for the bulk-fix diff preview: where it lands and the before/after text.
    /// </summary>
    internal readonly struct RewriteEdit
    {
        public readonly bool IsValid;
        public readonly int LineNumber;
        public readonly string OldLine;
        public readonly string NewLine;
        public readonly string AssetPath;

        public RewriteEdit(string assetPath, int lineNumber, string oldLine, string newLine)
        {
            OldLine = oldLine;
            NewLine = newLine;
            AssetPath = assetPath;
            LineNumber = lineNumber;
            IsValid = LineNumber >= 0 && !string.IsNullOrEmpty(AssetPath);
        }
    }
}
