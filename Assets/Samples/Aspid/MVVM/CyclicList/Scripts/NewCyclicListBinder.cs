using System;
using Aspid.MVVM;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;
using System.Collections.Generic;
using Aspid.MVVM.StarterKit.Unity;

namespace Samples.Aspid.MVVM.CyclicList
{
    public class NewCyclicListBinder : ObservableListMonoBinder<IViewModel>
    {
        [SerializeField] private ScrollRect _scrollRect; 
        [SerializeField] private MonoView _viewPrefab;
        
        private readonly List<Element> _views = new();
        private int _previousViewModelTopIndex = -1;
        
        private RectTransform Content => _scrollRect.content;
        
        private RectTransform Viewport => _scrollRect.viewport;
        
        private float ElementHeight => ((RectTransform)_viewPrefab.transform).rect.height;

        protected override void OnBound()
        {
            var viewportHeight = Viewport.rect.height;
            var visibleCount = Mathf.CeilToInt(viewportHeight / ElementHeight) + 2;
            Content.sizeDelta = new Vector2(Content.sizeDelta.x, List.Count * ElementHeight);
            
            for (var i = 0; i < visibleCount; i++)
            {
                var view = Instantiate(_viewPrefab, Content);
                view.gameObject.SetActive(true);
                _views.Add(new Element(view));
            }

            _scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
            Refresh();
        }

        private void OnScrollValueChanged(Vector2 _) =>
            OnScrollValueChangedRefresh();

        private void OnScrollValueChangedRefresh()
        {
            var viewModelTopIndex = GetCurrentViewModelTopIndex();
            if (viewModelTopIndex == _previousViewModelTopIndex) return;
            
            var direction = viewModelTopIndex - _previousViewModelTopIndex;
            
            if (direction < 0)
            {
                if (viewModelTopIndex < 0) return;
                
                var lastView = _views[^1];
                
                for (var i = _views.Count - 1; i > 0; i--)
                {
                    _views[i] = _views[i - 1];
                    ReinitializeElement(_views[i], viewModelTopIndex + i);
                }
                
                _views[0] = lastView;
                ReinitializeElement(lastView, viewModelTopIndex);
            }
            else if (direction > 0)
            {
                if (viewModelTopIndex + _views.Count - 1 >= List.Count) return;
                
                var firstView = _views[0];
            
                for (var i = 1; i < _views.Count; i++)
                {
                    _views[i - 1] = _views[i];
                    ReinitializeElement(_views[i], viewModelTopIndex + i - 1);
                }
            
                _views[^1] = firstView;
                ReinitializeElement(firstView, viewModelTopIndex + _views.Count - 1);
            }
            
            _previousViewModelTopIndex = viewModelTopIndex;
        }
        
        private void Refresh()
        {
            var viewModelTopIndex = GetCurrentViewModelTopIndex();
            if (viewModelTopIndex == _previousViewModelTopIndex) return;
            _previousViewModelTopIndex = viewModelTopIndex;

            foreach (var view in _views)
                ReinitializeElement(view, viewModelTopIndex++);
        }
        
        private int GetCurrentViewModelTopIndex()
        {
            var scrollY = Content.anchoredPosition.y;
            return Mathf.FloorToInt(scrollY / ElementHeight);
        }
        
        private void ReinitializeElement(Element element, int viewModelIndex)
        {
            var hasViewModel = viewModelIndex >= 0 && viewModelIndex < List.Count;

            if (hasViewModel)
            {
                if (element.Index != viewModelIndex)
                {
                    element.SetActive(true);
                    element.Reinitialize(List[viewModelIndex], viewModelIndex);
                    element.SetPosition(0, -viewModelIndex * ElementHeight, 0);
                }
            }
            else
            {
                element.Reinitialize(null, -1);
                element.SetActive(false);
            }
        }

        #region Handlers
        protected override void OnAdded(IViewModel newItem, int newStartingIndex)
        {
            // throw new System.NotImplementedException();
        }

        protected override void OnAdded(IReadOnlyList<IViewModel> newItems, int newStartingIndex)
        {
            // throw new System.NotImplementedException();
        }

        protected override void OnRemoved(IViewModel oldItem, int oldStartingIndex)
        {
            // throw new System.NotImplementedException();
        }

        protected override void OnRemoved(IReadOnlyList<IViewModel> oldItems, int oldStartingIndex)
        {
            // throw new System.NotImplementedException();
        }

        protected override void OnReplace(IViewModel oldItem, IViewModel newItem, int newStartingIndex)
        {
            // throw new System.NotImplementedException();
        }

        protected override void OnMove(IViewModel oldItem, IViewModel newItem, int oldStartingIndex,
            int newStartingIndex)
        {
            // throw new System.NotImplementedException();
        }

        protected override void OnReset()
        {
            // throw new System.NotImplementedException();
        }
        #endregion
        
        private sealed class Element
        {
            private readonly MonoView _view;
            
            public int Index { get; private set; }

            public Element(MonoView view)
            {
                Index = -1;
                _view = view;
            }

            public void Reinitialize(IViewModel viewModel, int index)
            {
                Index = index;
                _view.Reinitialize(viewModel);
            }

            public void SetActive(bool isActive) =>
                _view.gameObject.SetActive(isActive);

            public void SetPosition(float x, float y, float z) =>
                SetPosition(new Vector3(x, y, z));
            
            public void SetPosition(Vector3 position) =>
                _view.transform.localPosition = position;
        }
    }
}