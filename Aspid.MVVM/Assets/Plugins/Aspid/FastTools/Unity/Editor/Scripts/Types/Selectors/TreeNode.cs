using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal class TreeNode
    {
        public string Caption { get; set; }

        public string Tooltip { get; set; }

        public List<TreeNode> Children { get; }

        public string DisplayName { get; set; }

        public string AssemblyQualifiedName { get; set; }

        public bool HasChildren => Children.Count > 0;

        public bool IsSelectable => AssemblyQualifiedName is not null || DisplayName == Constants.NoneOption;

        public TreeNode(string displayName, string assemblyQualifiedName = null, string caption = null)
        {
            DisplayName = displayName;
            AssemblyQualifiedName = assemblyQualifiedName;
            Caption = caption ?? displayName;
            Tooltip = string.Empty;
            Children = new List<TreeNode>();
        }

        public bool MatchesFilter(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return true;

            if (DisplayName?.ToLowerInvariant().Contains(filter) == true)
                return true;

            if (Caption?.ToLowerInvariant().Contains(filter) == true)
                return true;

            if (AssemblyQualifiedName?.ToLowerInvariant().Contains(filter) == true)
                return true;

            return false;
        }
    }
}
