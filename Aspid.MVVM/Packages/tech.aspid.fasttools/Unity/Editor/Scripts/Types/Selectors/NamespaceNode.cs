using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal class NamespaceNode
    {
        internal string Segment { get; set; }

        internal bool IsTerminal { get; set; }

        internal Dictionary<string, NamespaceNode> Children { get; }

        internal NamespaceNode(string segment)
        {
            Segment = segment;
            Children = new Dictionary<string, NamespaceNode>(StringComparer.Ordinal);
        }

        internal NamespaceNode GetOrCreateChild(string segment)
        {
            if (!Children.TryGetValue(segment, out var child))
            {
                child = new NamespaceNode(segment);
                Children[segment] = child;
            }

            return child;
        }
    }
}
