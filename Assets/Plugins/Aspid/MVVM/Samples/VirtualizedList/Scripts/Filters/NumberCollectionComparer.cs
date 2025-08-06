using System;
using UnityEngine;
using System.Collections.Generic;
using Aspid.MVVM.StarterKit.Unity;

namespace Aspid.MVVM.Samples.VirtualizedList
{
    [Serializable]
    public sealed class NumberCollectionComparer : IViewModelCollectionComparer
    {
        [SerializeField] private bool _isInvert;

        public IComparer<IViewModel> Get() =>
            new Comparer(_isInvert);

        private class Comparer : IComparer<IViewModel>
        {
            private readonly bool _isInvert;
            
            public Comparer(bool isInvert)
            {
                _isInvert = isInvert;
            }

            public int Compare(IViewModel x, IViewModel y)
            {
                if (x is not ItemViewModel itemX || y is not ItemViewModel itemY) return 0;
                
                var result = itemX.Number.CompareTo(itemY.Number);
                return _isInvert ? -result : result;
            }
        }
    }
}