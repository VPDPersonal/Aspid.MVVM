using System;
using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.VirtualizedList
{
    [Serializable]
    public sealed class CompletedCollectionFilter : IViewModelCollectionFilter
    {
        [SerializeField] private bool _isCompleted;
        
        public Predicate<IViewModel> Get() => viewModel =>
        {
            if (viewModel is ItemViewModel itemViewModel)
                return itemViewModel.IsCompleted == _isCompleted;

            return false;
        };
    }
}