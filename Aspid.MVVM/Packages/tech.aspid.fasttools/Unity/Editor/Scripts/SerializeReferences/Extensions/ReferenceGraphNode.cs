using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// A single managed-reference node in a document's graph: its <c>RefIds</c> id, the stored type identity and
    /// whether that type still resolves to a loadable <see cref="Type"/>. Built purely from the asset YAML, so it
    /// surfaces references at any nesting depth — including the orphaned ones Unity drops from the live object.
    /// </summary>
    internal readonly struct ReferenceGraphNode
    {
        public readonly long Rid;
        public readonly ManagedTypeName StoredType;
        public readonly bool Resolves;

        public ReferenceGraphNode(long rid, ManagedTypeName storedType, bool resolves)
        {
            Rid = rid;
            StoredType = storedType;
            Resolves = resolves;
        }

        /// <summary>
        /// Short type name (the class identifier without namespace/assembly), for the row label.
        /// </summary>
        public string ShortName =>
            string.IsNullOrEmpty(StoredType.Class) ? $"rid {Rid}" : StoredType.Class;

        /// <summary>
        /// Full <c>Namespace.Class, Assembly</c> identity, for the row tooltip.
        /// </summary>
        public string FullName => StoredType.FullName;
    }
}
