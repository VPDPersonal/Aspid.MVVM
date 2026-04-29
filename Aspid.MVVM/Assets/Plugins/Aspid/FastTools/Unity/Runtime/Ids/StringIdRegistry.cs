#nullable enable
using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools
{
    public class StringIdRegistry<T> : StringIdRegistry
        where T : struct, IId
    {
        public bool Contains(T id) =>
            base.Contains(id.Id);
    }
    
    /// <summary>
    /// A ScriptableObject that maps string names to stable integer IDs for a given struct type.
    /// Used by the <c>IdStruct</c> system to persist and resolve string/int ID pairs.
    /// </summary>
    [CreateAssetMenu(fileName = "StringIdRegistry", menuName = "Aspid/FastTools/String Id Registry")]
    public partial class StringIdRegistry : ScriptableObject, IEnumerable<KeyValuePair<int, string>>
    {
        [SerializeField] private IdEntry[] _entries = Array.Empty<IdEntry>();

        [NonSerialized] private bool _cacheDirty = true;
        [NonSerialized] private Dictionary<string, int>? _idByName;
        [NonSerialized] private Dictionary<int, string>? _nameById;

        public IEnumerable<int> Ids =>
            this.Select(entry => entry.Key);

        public IEnumerable<string> IdNames =>
            this.Select(entry => entry.Value);

        public int GetId(string nameId)
        {
            EnsureCache();
            return _idByName!.TryGetValue(nameId, out var id) ? id : -1;
        }

        public string? GetNameId(int id)
        {
            EnsureCache();
            return _nameById.GetValueOrDefault(id);
        }
        
        public bool Contains(int id)
        {
            EnsureCache();
            return _nameById!.ContainsKey(id);
        }

        public bool Contains(string nameId)
        {
            EnsureCache();
            return _idByName!.ContainsKey(nameId);
        }

        public IEnumerator<KeyValuePair<int, string>> GetEnumerator() =>
            _entries.Select(entry => new KeyValuePair<int, string>(entry.Id, entry.Name)).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        private void EnsureCache()
        {
            if (!_cacheDirty && _idByName != null && _nameById != null) return;

            _idByName = new Dictionary<string, int>(_entries.Length);
            _nameById = new Dictionary<int, string>(_entries.Length);
            
            foreach (var entry in _entries)
            {
                if (!string.IsNullOrEmpty(entry.Name))
                    _idByName[entry.Name] = entry.Id;
                _nameById[entry.Id] = entry.Name ?? string.Empty;
            }

            _cacheDirty = false;
        }

        public void InvalidateCache() => _cacheDirty = true;

#if UNITY_EDITOR
        private void OnValidate() => _cacheDirty = true;
#endif

        /// <summary>
        /// A single name-to-id mapping entry.
        /// </summary>
        [Serializable]
        private struct IdEntry
        {
            public int Id;
            public string Name;
        }
    }
}
