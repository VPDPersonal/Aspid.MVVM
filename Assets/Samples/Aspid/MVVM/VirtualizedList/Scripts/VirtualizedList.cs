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
            Initialize();
        }
        
        private void Initialize()
        {
            Content.sizeDelta = new Vector2(Content.sizeDelta.x, List.Count * ElementHeight);
            var viewModelTopIndex = GetCurrentViewModelTopIndex();
            _previousViewModelTopIndex = viewModelTopIndex;

            foreach (var view in _views)
                InitializeElement(view, viewModelTopIndex++, true);
        }

        private void RefreshListOnScrollValueChanged()
        {
            var viewModelTopIndex = GetCurrentViewModelTopIndex();
            if (viewModelTopIndex == _previousViewModelTopIndex) return;
            
            var direction = viewModelTopIndex - _previousViewModelTopIndex;
            _previousViewModelTopIndex = viewModelTopIndex;

            switch (direction)
            {
                case > 0: RefreshListFromTopToBottom(viewModelTopIndex); break;
                case < 0: RefreshListFromBottomToTop(viewModelTopIndex); break;
            }
        }
        
        private void RefreshListFromTopToBottom(int viewModelIndex)
        {
            var firstView = _views[0];
            
            for (var i = 1; i < _views.Count; i++)
            {
                _views[i - 1] = _views[i];
                InitializeElement(_views[i], viewModelIndex + i - 1);
            }
            
            _views[^1] = firstView;
            InitializeElement(firstView, viewModelIndex + _views.Count - 1);
        }
        
        private void RefreshListFromBottomToTop(int viewModelIndex)
        {
            var lastView = _views[^1];
                
            for (var i = _views.Count - 1; i > 0; i--)
            {
                _views[i] = _views[i - 1];
                InitializeElement(_views[i], viewModelIndex + i);
            }
                
            _views[0] = lastView;
            InitializeElement(lastView, viewModelIndex);
        }
        
        private void InitializeElement(Element element, int viewModelIndex, bool force = false)
        {
            var hasViewModel = viewModelIndex >= 0 && viewModelIndex < List.Count;

            if (!hasViewModel)
            {
                element.Reinitialize(null, -1);
            }
            else if (force || element.Index != viewModelIndex)
            {
                element.Reinitialize(List[viewModelIndex], viewModelIndex);
            }
        }
        
        private int GetCurrentViewModelTopIndex()
        {
            var scrollY = Content.anchoredPosition.y;
            return Mathf.FloorToInt(scrollY / ElementHeight);
        }

        private void OnScrollValueChanged(Vector2 _) =>
            RefreshListOnScrollValueChanged();

        #region Handlers
        protected override void OnAdded(IViewModel newItem, int newStartingIndex) =>
            Initialize();

        protected override void OnAdded(IReadOnlyList<IViewModel> newItems, int newStartingIndex) =>
            Initialize();

        protected override void OnRemoved(IViewModel oldItem, int oldStartingIndex) =>
            Initialize();

        protected override void OnRemoved(IReadOnlyList<IViewModel> oldItems, int oldStartingIndex) =>
            Initialize();

        protected override void OnReplace(IViewModel oldItem, IViewModel newItem, int newStartingIndex) =>
            Initialize();

        protected override void OnMove(IViewModel oldItem, IViewModel newItem, int oldStartingIndex, int newStartingIndex) =>
            Initialize();

        protected override void OnReset() =>
            Initialize();
        #endregion
        
        private sealed class Element
        {
            private readonly MonoView _view;
            
            private float Height => ((RectTransform)_view.transform).rect.height;
            
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

                if (index >= 0)
                {
                    _view.gameObject.SetActive(true);
                    _view.transform.localPosition = new Vector3(0, -index * Height, 0);
                }
                else
                {
                    _view.gameObject.SetActive(false);
                }
            }
        }
    }
}