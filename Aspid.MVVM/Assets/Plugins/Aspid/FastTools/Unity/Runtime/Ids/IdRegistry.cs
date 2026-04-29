#nullable enable
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids
{
    public class IdRegistry<T> : IdRegistry
        where T : struct, IId
    {
        public bool Contains(T id) =>
            base.Contains(id.Id);
    }
    
    /// <summary>
    /// A ScriptableObject that holds a stable set of integer IDs for a given struct type.
    /// Names are stored and edited in the inspector but stripped from player builds.
    /// Use <see cref="Aspid.FastTools.StringIdRegistry"/> when name lookups are needed at runtime.
    /// </summary>
    [CreateAssetMenu(fileName = "IdRegistry", menuName = "Aspid/FastTools/Id Registry")]
    public partial class IdRegistry : ScriptableObject, IEnumerable<int>
    {
        [SerializeField] private int[] _ids = Array.Empty<int>();

        [NonSerialized] private HashSet<int>? _idSet;
        [NonSerialized] private bool _cacheDirty = true;

        public int Count => _ids.Length;

        public bool Contains(int id)
        {
            EnsureCache();
            return _idSet!.Contains(id);
        }

        public IEnumerator<int> GetEnumerator() =>
            ((IEnumerable<int>)_ids).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public void InvalidateCache() => _cacheDirty = true;

        private void EnsureCache()
        {
            if (!_cacheDirty && _idSet != null) return;

            _idSet = new HashSet<int>(_ids.Length);
            foreach (var id in _ids)
                _idSet.Add(id);

            _cacheDirty = false;
        }

#if UNITY_EDITOR
        private void OnValidate() => _cacheDirty = true;
#endif
    }
}
