using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids
{
    /// <summary>
    /// A strongly typed wrapper around <see cref="IdRegistry"/> that exposes <see cref="IId"/>-aware membership checks.
    /// </summary>
    /// <typeparam name="T">The id struct type bound to this registry.</typeparam>
    public class IdRegistry<T> : IdRegistry
        where T : struct, IId
    {
        public bool TryGetName(T id, out string nameId) =>
            base.TryGetName(id.Id, out nameId);

        public bool Contains(T id) =>
            base.Contains(id.Id);
    }

    /// <summary>
    /// A ScriptableObject that maps string names to stable integer IDs for a given struct type.
    /// </summary>
    [CreateAssetMenu(fileName = "IdRegistry", menuName = "Aspid/Id Registry")]
    public partial class IdRegistry : ScriptableObject, IEnumerable<KeyValuePair<int, string>>
    {
        [SerializeField] private int[] _ids = Array.Empty<int>();
        [SerializeField] private string[] _names = Array.Empty<string>();

        [NonSerialized] private Dictionary<string, int> _idByName = new();
        [NonSerialized] private Dictionary<int, string> _nameById = new();

        [field: NonSerialized]
        public bool IsCacheDirty { get; private set; }

        public int Count => _ids.Length;

        public IReadOnlyList<int> Ids => _ids;

        public IReadOnlyList<string> IdNames => _names;

        protected virtual void OnValidate() =>
            IsCacheDirty = true;

        protected virtual void OnEnable() =>
            IsCacheDirty = true;

        public bool TryGetId(string nameId, out int id)
        {
            EnsureCache();
            return _idByName.TryGetValue(nameId, out id);
        }

        public bool TryGetName(int id, out string nameId)
        {
            EnsureCache();
            if (_nameById.TryGetValue(id, out nameId)) return true;
            nameId = string.Empty;
            return false;
        }

        public bool Contains(int id)
        {
            EnsureCache();
            return _nameById.ContainsKey(id);
        }

        public bool Contains(string nameId)
        {
            EnsureCache();
            return _idByName.ContainsKey(nameId);
        }

        public void EnsureCache()
        {
            if (!IsCacheDirty) return;
            
            RebuildCache();
            IsCacheDirty = false;
        }
        
        public void InvalidateCache() =>
            IsCacheDirty = true;

        public IEnumerator<KeyValuePair<int, string>> GetEnumerator()
        {
            var count = Math.Min(_ids.Length, _names.Length);
            
            for (var i = 0; i < count; i++)
                yield return new KeyValuePair<int, string>(_ids[i], _names[i]);
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        private void RebuildCache()
        {
            using var _ = this.Marker();
            
            var count = Math.Min(_ids.Length, _names.Length);
            _idByName = new Dictionary<string, int>(count);
            _nameById = new Dictionary<int, string>(count);

            for (var i = 0; i < count; i++)
            {
                var id = _ids[i];
                var nameId = _names[i] ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(nameId))
                    _idByName[nameId] = id;

                _nameById[id] = nameId;
            }
        }
    }
}
