#nullable enable
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    /// <summary>
    /// Adapter over the two registry types' storage layouts.
    /// Core inspector code talks to this interface, never to the concrete registry.
    /// </summary>
    internal interface IRegistryAccessor
    {
        Object Target { get; }
        SerializedObject SerializedObject { get; }
        SerializedProperty TargetStructTypeProperty { get; }
        SerializedProperty NextIdProperty { get; }

        int Count { get; }
        int GetId(int index);
        string GetName(int index);

        /// <summary>
        /// Returns the new Id assigned to <paramref name="name"/> (and bumps NextId).
        /// Caller is responsible for Undo and dirty-marking via <see cref="Record"/>.
        /// </summary>
        int Add(string name);

        /// <summary>
        /// Renames the entry at <paramref name="index"/>.
        /// </summary>
        void SetName(int index, string name);

        /// <summary>
        /// Removes the entry at <paramref name="index"/>. For IdRegistry this removes
        /// from both _ids and _names atomically.
        /// </summary>
        void RemoveAt(int index);

        bool Contains(string name);

        /// <summary>Largest Id currently assigned, or 0 if the registry is empty.</summary>
        int MaxAssignedId { get; }

        /// <summary>Registers an Undo group covering the upcoming mutation.</summary>
        void Record(string operationName);

        /// <summary>Applies pending SerializedObject edits and dirties the asset.</summary>
        void Commit();

        /// <summary>True when storage is in an inconsistent state (e.g. parallel arrays diverged).</summary>
        bool HasStructuralDamage(out string reason);

        /// <summary>Enumerates duplicates and empty-name entries for the Clean-up flow.</summary>
        IEnumerable<int> EnumerateInvalidIndices();
    }
}
