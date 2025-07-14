using System;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;
using Aspid.MVVM;
using Aspid.MVVM.StarterKit; 
using UnityEngine;

namespace Samples.Aspid.MVVM.VirtualizedList
{
    [Serializable]
    public class ElementFilterFactory : IViewModelFilterFactory
    {
        [SerializeField] private bool _isCompleted;

        private FilteredList<ElementViewModel> _filter;
        
        public IReadOnlyFilteredList<IViewModel> Create(IReadOnlyList<IViewModel> list)
        {
            if (list is not IReadOnlyList<ElementViewModel> specificList) return null;
            
            _filter ??= new FilteredList<ElementViewModel>(specificList, Check);
            return _filter;
        }

        public void Release()
        {
            _filter.Dispose();
            _filter = null;
        }

        private bool Check(ElementViewModel viewModel) =>
            viewModel.IsCompleted == _isCompleted;
    }
    
    [Serializable]
    public class EvenFilterFactory : IViewModelFilterFactory
    {
        [SerializeField] private bool _isInvert;

        private FilteredList<ElementViewModel> _filter;
        
        public IReadOnlyFilteredList<IViewModel> Create(IReadOnlyList<IViewModel> list)
        {
            if (list is not IReadOnlyList<ElementViewModel> specificList) return null;
            
            _filter ??= new FilteredList<ElementViewModel>(specificList, Check);
            return _filter;
        }

        public void Release()
        {
            _filter.Dispose();
            _filter = null;
        }

        private bool Check(ElementViewModel viewModel) =>
            viewModel.Number % 2 is 0 && !_isInvert;
    }
}