using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal class NamespaceNode
    {
        public string Segment { get; set; }

        public bool IsTerminal { get; set; }

        public Dictionary<string, NamespaceNode> Children { get; }

        public NamespaceNode(string segment)
        {
            Segment = segment;
            Children = new Dictionary<string, NamespaceNode>(StringComparer.Ordinal);
        }

        public NamespaceNode GetOrCreateChild(string segment)
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
