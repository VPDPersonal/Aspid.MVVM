using Aspid.MVVM;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;
using System.Collections.Generic;
using Aspid.MVVM.StarterKit.Unity;

namespace Samples.Aspid.MVVM.VirtualizedList
{
    public class VirtualizedList : ObservableListMonoBinder<IViewModel>
    {
        [SerializeField] private ScrollRect _scrollRect; 
        [SerializeField] private MonoView _viewPrefab;
        
        private int _previousViewModelTopIndex = -1;
        
        private readonly List<Element> _views = new();
        
        private RectTransform Content => _scrollRect.content;
        
        private RectTransform Viewport => _scrollRect.viewport;
        
        private float ElementHeight => ((RectTransform)_viewPrefab.transform).rect.height;

        protected override void OnBound()
        {
            var viewportHeight = Viewport.rect.height;
            var visibleCount = Mathf.CeilToInt(viewportHeight / ElementHeight) + 2;
            
            for (var i = 0; i < visibleCount; i++)
            {
                var view = Instantiate(_viewPrefab, Content);
                view.gameObject.SetActive(true);
                _views.Add(new Element(view));
            }

            _scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
            Refresh();
        }
        
        private void Refresh()
        {
            RefreshContentSize();
            _previousViewModelTopIndex = GetCurrentViewModelTopIndex();

            for (var i = 0; i < _views.Count; i++)
                RefreshElement(i, _previousViewModelTopIndex + i, true);
        }

        private void RefreshListOnScrollValueChanged()
        {
            var viewModelTopIndex = GetCurrentViewModelTopIndex();
            if (viewModelTopIndex == _previousViewModelTopIndex) return;
            
            var direction = viewModelTopIndex - _previousViewModelTopIndex;
            _previousViewModelTopIndex = viewModelTopIndex;

            switch (direction)
            {
                case > 0: RefreshListForward(viewModelTopIndex); break;
                case < 0: RefreshListFromBackward(viewModelTopIndex); break;
            }
        }
        
        private void RefreshListForward(int viewModelIndex)
        {
            var firstView = _views[0];
            
            for (var i = 1; i < _views.Count; i++)
            {
                _views[i - 1] = _views[i];
                RefreshElement(i, viewModelIndex + i - 1);
            }
            
            _views[^1] = firstView;
            RefreshElement(_views.Count - 1, viewModelIndex + _views.Count - 1);
        }
        
        private void RefreshListFromBackward(int viewModelIndex)
        {
            var lastView = _views[^1];
                
            for (var i = _views.Count - 1; i > 0; i--)
            {
                _views[i] = _views[i - 1];
                RefreshElement(i, viewModelIndex + i);
            }
                
            _views[0] = lastView;
            RefreshElement(0, viewModelIndex);
        }
        
        private void RefreshElement(int elementIndex, int viewModelIndex, bool force = false)
        {
            var hasViewModel = viewModelIndex >= 0 && viewModelIndex < List.Count;
            
            if (!hasViewModel) _views[elementIndex].Reinitialize(null, -1, true);
            else _views[elementIndex].Reinitialize(List[viewModelIndex], viewModelIndex, force);
        }
        
        private void RefreshContentSize() =>
            Content.sizeDelta = new Vector2(Content.sizeDelta.x, List.Count * ElementHeight);
        
        private int GetCurrentViewModelTopIndex()
        {
            var scrollY = Content.anchoredPosition.y;
            return Mathf.FloorToInt(scrollY / ElementHeight);
        }
        
        protected override void OnAdded(IViewModel newItem, int newStartingIndex)
        {
            var viewIndex = newStartingIndex - _previousViewModelTopIndex;

            if (viewIndex < 0 || viewIndex <= List.Count) Refresh();
            else RefreshContentSize();
        }

        protected override void OnAdded(IReadOnlyList<IViewModel> newItems, int newStartingIndex)
        {
            // TODO Optimize
            Refresh();
        }

        protected override void OnRemoved(IViewModel oldItem, int oldStartingIndex)
        {
            var viewIndex = oldStartingIndex - _previousViewModelTopIndex;

            if (viewIndex < 0 || viewIndex <= List.Count) Refresh();
            else RefreshContentSize();
        }

        protected override void OnRemoved(IReadOnlyList<IViewModel> oldItems, int oldStartingIndex)
        {
            // TODO Optimize
            Refresh();
        }

        protected override void OnReplace(IViewModel oldItem, IViewModel newItem, int newStartingIndex)
        {
            var viewIndex = newStartingIndex - _previousViewModelTopIndex;
            if (viewIndex >= 0 && viewIndex < List.Count) Refresh();
        }

        protected override void OnMove(IViewModel oldItem, IViewModel newItem, int oldStartingIndex, int newStartingIndex)
        {
            var oldViewIndex = oldStartingIndex - _previousViewModelTopIndex;
            var newViewIndex = newStartingIndex - _previousViewModelTopIndex;

            if (oldViewIndex < 0 || oldViewIndex <= _views.Count || newViewIndex < 0 || newViewIndex <= _views.Count)
                Refresh();
        }

        protected override void OnReset() =>
            Refresh();
        
        private void OnScrollValueChanged(Vector2 _) =>
            RefreshListOnScrollValueChanged();
        
        private class Element
        {
            private int _index;
            private readonly float _height;
            private readonly MonoView _view;
            
            public Element(MonoView view)
            {
                _index = -1;
                _view = view;
                _height = ((RectTransform)_view.transform).rect.height;
            }

            public void Reinitialize(IViewModel viewModel, int index, bool force = false)
            {
                if (!force && _index == index) return;
                
                _index = index;
                
                if (index >= 0)
                {
                    // TODO Remove reinitialize and add swap data for optimize
                    
                    _view.Reinitialize(viewModel);
                    _view.gameObject.SetActive(true);
                    _view.transform.localPosition = new Vector3(0, -index * _height, 0);
                }
                else
                {
                    _view.Deinitialize();
                    _view.gameObject.SetActive(false);
                }
            }
        }
    }
}