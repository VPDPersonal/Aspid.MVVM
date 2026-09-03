using System;
using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.VirtualizedList
{
    [Serializable]
    public sealed class EvenCollectionFilter : ICollectionFilter<IViewModel>
    {
        [SerializeField] private bool _isInvert;

        public bool Matches(IViewModel item) =>
            item is ItemViewModel viewModel && (viewModel.Number % 2 is 0) != _isInvert;
    }
}
