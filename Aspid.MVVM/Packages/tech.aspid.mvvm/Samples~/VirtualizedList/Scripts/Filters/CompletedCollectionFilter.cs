using System;
using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.VirtualizedList
{
    [Serializable]
    public sealed class CompletedCollectionFilter : ICollectionFilter<IViewModel>
    {
        [SerializeField] private bool _isCompleted;

        public bool Matches(IViewModel item) =>
            item is ItemViewModel viewModel && viewModel.IsCompleted == _isCompleted;
    }
}
