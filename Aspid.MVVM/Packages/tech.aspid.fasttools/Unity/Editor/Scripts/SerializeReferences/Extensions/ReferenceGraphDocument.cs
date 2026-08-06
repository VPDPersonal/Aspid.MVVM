using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// The managed-reference graph of one serialized object document: its <c>fileId</c> anchor, an optional
    /// best-effort component/type name for the header, the <c>RefIds</c> nodes, the parent → child edges between
    /// them, the field-pointer roots and the derived shared / orphaned sets.
    /// </summary>
    internal sealed class ReferenceGraphDocument
    {
        public long FileId;
        public string TypeName;

        public readonly List<ReferenceGraphNode> Nodes = new();

        // One entry per field pointer in the document body (the tree's entry points). The same rid may appear under
        // two fields — both are kept, so the window renders each subtree and the shared set flags the alias.
        public readonly List<ReferenceGraphRoot> Roots = new();

        // Parent rid → ordered, de-duplicated child edges of the nested graph (data-block pointers only; roots are
        // tracked separately in Roots). Empty (null-sentinel) child slots are kept so a cleared nested field surfaces.
        public readonly Dictionary<long, List<ReferenceGraphEdge>> Edges = new();

        // rids referenced by two or more parents in total (root pointers + nested edges) — aliased managed references.
        public readonly HashSet<long> Shared = new();

        // rids reachable from no root — leftover payloads no field points at.
        public readonly HashSet<long> Orphans = new();

        public ReferenceGraphNode? FindNode(long rid)
        {
            foreach (var node in Nodes.Where(node => node.Rid == rid))
                return node;

            return null;
        }

        public IReadOnlyList<ReferenceGraphEdge> ChildrenOf(long rid) =>
            Edges.TryGetValue(rid, out var children) ? children : Array.Empty<ReferenceGraphEdge>();
    }
}
