#if UNITY_EDITOR
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable UnusedMember.Local
// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids
{
    public partial class IdRegistry
    {
        [TypeSelector(typeof(IId))]
        [SerializeField] private string _targetStructType = string.Empty;

        [SerializeField] private int _nextId = 1;
    }
}
#endif
