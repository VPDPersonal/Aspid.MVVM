#if UNITY_EDITOR
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable UnusedMember.Local
// ReSharper disable once CheckNamespace
// ReSharper disable NotAccessedField.Local
namespace Aspid.FastTools.Ids
{
    public partial class IdRegistry
    {
        [TypeSelector(typeof(IId))]
        [SerializeField] private string _targetStructType = string.Empty;
        
        [SerializeField] private int _nextId = 1;
        [SerializeField] private string[] _names = Array.Empty<string>();
    }
}
#endif
