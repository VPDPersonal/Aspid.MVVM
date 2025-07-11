using System;
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
        [SerializeField] private Direction _direction;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private MonoView _viewPrefab;
        
        private Size? _viewSize;
        private Size? _viewportSize;
        private ContentTransform? _content;
        private int _previousViewModelTopIndex = -1;
        private Element[] _views = Array.Empty<Element>() ;


        private Size ViewSize => _viewSize ??= new Size(_viewPrefab, _direction);
        
        private Size ViewportSize => _viewportSize ??= new Size(_scrollRect.viewport, _direction);
        
        private ContentTransform Content => _content ??= new ContentTransform(_scrollRect.content, ViewSize, _direction);

        private void OnValidate()
        {
            if (!_scrollRect) return;
            
            Content.Validate();
            _scrollRect.vertical = _direction is Direction.Vertical;
            _scrollRect.horizontal = _direction is Direction.Horizontal;
        }

        protected override void OnBound() =>
            Initialize();
        
        protected override void OnUnbound() =>
            _scrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);

        private void Initialize()
        { 
            var visibleCount = CalculateVisibleCount();

            if (_views is not null)
            {
                foreach (var view in _views)
                    view.Dispose();
            }
            
            _views = new Element[visibleCount];
            
            for (var i = 0; i < visibleCount; i++)
            {
                var view = Instantiate(_viewPrefab, Content);
                _views[i] = new Element(view, _direction);
            }
            
            _scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
            Refresh();
            return;
            
            int CalculateVisibleCount() =>
                Mathf.CeilToInt(ViewportSize.Value / ViewSize.Value) + 2;
        }
        
        private void Refresh()
        {
            ResizeContent();
            _previousViewModelTopIndex = GetCurrentViewModelTopIndex();

            for (var i = 0; i < _views.Length; i++)
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
            
            for (var i = 1; i < _views.Length; i++)
            {
                _views[i - 1] = _views[i];
                RefreshElement(i, viewModelIndex + i - 1);
            }
            
            _views[^1] = firstView;
            RefreshElement(_views.Length - 1, viewModelIndex + _views.Length - 1);
        }
        
        private void RefreshListFromBackward(int viewModelIndex)
        {
            var lastView = _views[^1];
                
            for (var i = _views.Length - 1; i > 0; i--)
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
        
        private void ResizeContent() =>
            Content.Resize(List.Count);

        private int GetCurrentViewModelTopIndex() =>
            Mathf.FloorToInt(Content.ScrollValue / ViewSize.Value);
        
        protected override void OnAdded(IViewModel newItem, int newStartingIndex)
        {
            var viewIndex = newStartingIndex - _previousViewModelTopIndex;

            if (viewIndex < 0 || viewIndex <= List.Count) Refresh();
            else ResizeContent();
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
            else ResizeContent();
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

            if (oldViewIndex < 0 || oldViewIndex <= _views.Length || newViewIndex < 0 || newViewIndex <= _views.Length)
                Refresh();
        }

        protected override void OnReset() =>
            Refresh();
        
        private void OnScrollValueChanged(Vector2 _) =>
            RefreshListOnScrollValueChanged();
        
        private class Element : IDisposable
        {
            private int _index;
            private readonly float _size;
            private readonly MonoView _view;
            private readonly Direction _direction;
            
            public Element(MonoView view, Direction direction)
            {
                _index = -1;
                _view = view;
                _direction = direction;

                var rectTransform = (RectTransform)_view.transform;
                rectTransform.pivot = new Vector2(0, 1);

                _size = direction switch
                {
                    Direction.Vertical => rectTransform.rect.height,
                    Direction.Horizontal => rectTransform.rect.width,
                    _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                };
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
                    _view.transform.localPosition = GetPosition(index);
                }
                else
                {
                    _view.Deinitialize();
                    _view.gameObject.SetActive(false);
                }
            }

            private Vector3 GetPosition(int index) => _direction switch
            {
                Direction.Vertical => new Vector3(0, -index * _size, 0),
                Direction.Horizontal => new Vector3(index * _size, 0, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(_direction), _direction, null)
            };

            public void Dispose() =>
                _view.DestroyView();
        }
        
        private enum Direction
        {
            Vertical,
            Horizontal
        }

        private readonly struct Size
        {
            public readonly float Value;
            
            public Size(Component component, Direction direction)
            {
                var transform = (RectTransform)component.transform;
                
                Value = direction switch
                {
                    Direction.Vertical => transform.rect.size.y,
                    Direction.Horizontal => transform.rect.size.x,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        private readonly struct ContentTransform
        {
            private readonly Size _size;
            private readonly Direction _direction;
            private readonly RectTransform _content;

            public float ScrollValue => _direction switch
            {
                Direction.Vertical => _content.anchoredPosition.y,
                Direction.Horizontal => -_content.anchoredPosition.x,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            public ContentTransform(RectTransform content, Size size, Direction direction)
            {
                Validate(content, direction);

                _size = size;
                _content = content;
                _direction = direction;
            }

            public void Resize(int viewModelCount)
            {
                var size = viewModelCount * _size.Value;
                
                _content.sizeDelta = _direction switch
                {
                    Direction.Vertical => new Vector2(_content.sizeDelta.x, size),
                    Direction.Horizontal => new Vector2(size, _content.sizeDelta.y),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            public void Validate() =>
                Validate(_content, _direction);

            private static void Validate(RectTransform content, Direction direction)
            {
                if (content is null) return;
                
                content.offsetMin = Vector2.zero;
                content.offsetMax = Vector2.zero;
                content.pivot = new Vector2(0, 1);
                
                switch (direction)
                {
                    case Direction.Vertical:
                        content.anchorMin = new Vector2(0, 1);
                        content.anchorMax = new Vector2(1, 1);
                        break;
                    
                    case Direction.Horizontal:
                        content.anchorMin = new Vector2(0, 0);
                        content.anchorMax = new Vector2(0, 1);
                        break;
                    
                    default: throw new ArgumentOutOfRangeException();
                }
            }

            public static implicit operator RectTransform(ContentTransform content) => content._content;
        }
    }
}