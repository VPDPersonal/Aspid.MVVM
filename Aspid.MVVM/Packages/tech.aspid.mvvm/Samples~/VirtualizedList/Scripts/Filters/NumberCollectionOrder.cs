using System;
using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.VirtualizedList
{
    [Serializable]
    public sealed class NumberCollectionOrder : ICollectionOrder<IViewModel>
    {
        [SerializeField] private bool _isInvert;

        public int Compare(IViewModel x, IViewModel y)
        {
            if (x is not ItemViewModel itemX || y is not ItemViewModel itemY) return 0;

            var result = itemX.Number.CompareTo(itemY.Number);
            return _isInvert ? -result : result;
        }
    }
}
