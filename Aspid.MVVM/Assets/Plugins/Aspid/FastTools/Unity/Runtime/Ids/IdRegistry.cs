#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools
{
    /// <summary>
    /// A ScriptableObject that maps string names to stable integer IDs for a given struct type.
    /// Used by the <c>IdStruct</c> system to persist and resolve string/int ID pairs.
    /// </summary>
    [CreateAssetMenu(fileName = "StringIdRegistry", menuName = "Aspid/FastTools/String Id Registry")]
    public sealed class IdRegistry : ScriptableObject
    {
        /// <summary>
        /// A single name-to-id mapping entry.
        /// </summary>
        [Serializable]
        public struct IdEntry
        {
            public int Id;
            public string Name;
        }

#if UNITY_EDITOR
        [TypeSelector(typeof(IId))]
        [SerializeField] private string _targetStructType = string.Empty;
        
        [SerializeField] private int _nextId = 1;
#endif

        [SerializeField] private IdEntry[] _entries = Array.Empty<IdEntry>();

        private string[]? _idNames;

        /// <summary>Assembly-qualified name of the struct type this registry is bound to.</summary>
        public string TargetStructType => _targetStructType;

        /// <summary>All registered entries.</summary>
        public IReadOnlyList<IdEntry> Entries => _entries;

        /// <summary>All registered names in registration order.</summary>
        public IReadOnlyList<string> Ids => _idNames ??= BuildIdNames();

        private string[] BuildIdNames()
        {
            var names = new string[_entries.Length];
            for (int i = 0; i < _entries.Length; i++)
                names[i] = _entries[i].Name;
            return names;
        }

        private void OnValidate() => _idNames = null;
        
        public int GetId(string nameId)
        {
            foreach (var e in _entries)
                if (e.Name == nameId) return e.Id;

            return -1;
        }

        public string? GetNameId(int id)
        {
            foreach (var e in _entries)
                if (e.Id == id) return e.Name;

            return null;
        }

        public bool Contains(string nameId)
        {
            foreach (var e in _entries)
                if (e.Name == nameId) return true;

            return false;
        }
    }
}
