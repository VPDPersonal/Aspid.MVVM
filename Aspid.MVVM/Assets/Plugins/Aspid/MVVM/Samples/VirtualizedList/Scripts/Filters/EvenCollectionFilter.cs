using System;
using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.VirtualizedList
{
    [Serializable]
    public sealed class EvenCollectionFilter : IViewModelCollectionFilter
    {
        [SerializeField] private bool _isInvert;

        public Predicate<IViewModel> Get() => viewModel =>
        {
            if (viewModel is ItemViewModel itemViewModel)
            {
                return (itemViewModel.Number % 2 is 0) switch
                {
                    true when !_isInvert => true,
                    false when _isInvert => true,
                    _ => false
                };
            }

            return false;
        };
    }
}