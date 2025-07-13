using System;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;
using Aspid.MVVM;
using Aspid.MVVM.StarterKit;
using TriInspector;
using UnityEngine;

namespace Samples.Aspid.MVVM.VirtualizedList
{
    [Serializable]
    public class ElementFilterFactory : IViewModelFilterFactory
    {
        [OnValueChanged(nameof(OnValidate))]
        [SerializeField] private bool _isCompleted;

        private FilteredList<ElementViewModel> _filter;
        
        public IReadOnlyFilteredList<IViewModel> Create(IReadOnlyList<IViewModel> list)
        {
            if (list is not IReadOnlyList<ElementViewModel> specificList) return null;
            
            _filter ??= new FilteredList<ElementViewModel>(specificList, Check);
            return _filter;
        }

        private bool Check(ElementViewModel viewModel) =>
            viewModel.IsCompleted == _isCompleted;

        private void OnValidate() =>
            _filter?.Update();
    }
}