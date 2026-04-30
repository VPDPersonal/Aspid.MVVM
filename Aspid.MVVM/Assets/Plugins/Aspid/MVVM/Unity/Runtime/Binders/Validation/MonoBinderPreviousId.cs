using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Validation
{
    [Serializable]
    public struct MonoBinderPreviousId
    {
        [SerializeField] private string _id;
        
        public string Id => _id;

        public MonoBinderPreviousId(string id)
        {
            _id = id;
        }
    }
}