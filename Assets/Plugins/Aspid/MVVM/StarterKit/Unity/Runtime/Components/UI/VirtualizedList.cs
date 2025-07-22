using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;
using System.Collections;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;
using Aspid.Collections.Observable.Filtered;

namespace Aspid.MVVM.StarterKit.Unity
{
    // TODO Add support Layout
    // TODO Add support spacings
    // TODO Add support various views
    [AddComponentMenu("Aspid/MVVM/Components/UI/ScrollRect/VirtualizedList (Beta)")]
    public class VirtualizedList : ScrollRect
    {
        [SerializeField] private MonoView _viewPrefab;
        
        private Element[] _views;
        private Coroutine _initializing;
        private int _previousViewModelTopIndex = -1;
        
        private Length? _viewLength;
        private Length? _viewportLength;
        private ContentTransformData? _contentTransform;
        
        private IReadOnlyList<IViewModel> _itemsSource;
        
        public IReadOnlyList<IViewModel> ItemsSource
        {
            get => _itemsSource;
            set 
            {
                Deinitialize();
                _itemsSource = value;
                Initialize();
            }
        }

        private DirectionType Direction
        {
            get
            {
                if (vertical && horizontal) return DirectionType.VerticalAndHorizontal;
                if (vertical) return DirectionType.Vertical;
                if (horizontal) return DirectionType.Horizontal;
            
                return DirectionType.None;
            }
        }
        
        private Length ViewLength => _viewLength ??= new Length(_viewPrefab, Direction);
        
        private Length ViewportLength => _viewportLength ??= new Length(viewport, Direction);
        
        private ContentTransformData ContentTransform => _contentTransform ??= new ContentTransformData(content, ViewLength, Direction);
        
        protected override void OnValidate()
        {
            if (_viewPrefab)
                ContentTransform.Validate();
            
            base.OnValidate();
        }

        private void Initialize()
        {
            switch (ItemsSource)
            {
                case IReadOnlyFilteredList<IViewModel> filteredList: filteredList.CollectionChanged += OnCollectionChanged; break;
                case IReadOnlyObservableList<IViewModel> observableList: observableList.CollectionChanged += OnCollectionChanged; break;
            }
            
            _initializing = StartCoroutine(InitializeAsync());
        }

        private IEnumerator InitializeAsync()
        {
            if (ItemsSource is null) yield break;
            yield return new WaitForEndOfFrame();
            
            var visibleCount = CalculateVisibleCount();

            _views ??= new Element[visibleCount];
            
            for (var i = 0; i < visibleCount; i++)
            {
                var view = _views[i] is null
                    ? Instantiate(_viewPrefab, ContentTransform)
                    : _views[i].View;

                _views[i] = new Element(view, Direction);
            }
            
            onValueChanged.AddListener(OnScrollValueChanged);
            Refresh();
            yield break;

            int CalculateVisibleCount() =>
                Mathf.CeilToInt(ViewportLength.Value / ViewLength.Value) + 2;
        }

        private void Deinitialize()
        {
            if (ItemsSource is null) return;
            StopCoroutine(_initializing);
            _initializing = null;
            
            foreach (var view in _views)
                view.Deinitialize();
            
            onValueChanged.RemoveListener(OnScrollValueChanged);
            
            switch (ItemsSource)
            {
                case IReadOnlyFilteredList<IViewModel> filteredList: filteredList.CollectionChanged -= OnCollectionChanged; break;
                case IReadOnlyObservableList<IViewModel> observableList: observableList.CollectionChanged -= OnCollectionChanged; break;
            }

            _itemsSource = null;
            OnReset();
        }
        
        private void Refresh()
        {
            if (ItemsSource is null) return;
            if (_views is null) return;
            
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
            var hasViewModel = viewModelIndex >= 0 && viewModelIndex < ItemsSource.Count;
            
            if (!hasViewModel) _views[elementIndex].Reinitialize(null, -1, true);
            else _views[elementIndex].Reinitialize(ItemsSource[viewModelIndex], viewModelIndex, force);
        }
        
        private void ResizeContent() =>
            ContentTransform.Resize(ItemsSource.Count);

        private int GetCurrentViewModelTopIndex() =>
            Mathf.FloorToInt(ContentTransform.ScrollValue / ViewLength.Value);
        
        private void OnCollectionChanged()
        {
            OnReset();
            OnAdded(ItemsSource, 0);
        }

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<IViewModel> e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (e.IsSingleItem) OnAdded(e.NewItem, e.NewStartingIndex);
                        else OnAdded(e.NewItems, e.NewStartingIndex);
                    }
                    break;
                
                case NotifyCollectionChangedAction.Remove:
                    {
                        if (e.IsSingleItem) OnRemoved(e.OldItem, e.OldStartingIndex);
                        else OnRemoved(e.OldItems, e.OldStartingIndex);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        if (e.IsSingleItem) OnReplace(e.OldItem, e.NewItem, e.OldStartingIndex);
                        else throw new NotImplementedException();
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    {
                        OnReset();
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    {
                        OnMove(e.OldItem, e.NewItem, e.OldStartingIndex, e.NewStartingIndex);
                    }
                    break;
                
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        protected virtual void OnAdded(IViewModel newItem, int newStartingIndex)
        {
            var viewIndex = newStartingIndex - _previousViewModelTopIndex;

            if (viewIndex < 0 || viewIndex <= ItemsSource.Count) Refresh();
            else ResizeContent();
        }

        protected virtual void OnAdded(IReadOnlyList<IViewModel> newItems, int newStartingIndex)
        {
            // TODO Optimize
            Refresh();
        }

        protected virtual void OnRemoved(IViewModel oldItem, int oldStartingIndex)
        {
            var viewIndex = oldStartingIndex - _previousViewModelTopIndex;

            if (viewIndex < 0 || viewIndex <= ItemsSource.Count) Refresh();
            else ResizeContent();
        }

        protected virtual void OnRemoved(IReadOnlyList<IViewModel> oldItems, int oldStartingIndex)
        {
            // TODO Optimize
            Refresh();
        }

        protected virtual void OnReplace(IViewModel oldItem, IViewModel newItem, int newStartingIndex)
        {
            var viewIndex = newStartingIndex - _previousViewModelTopIndex;
            if (viewIndex >= 0 && viewIndex < ItemsSource.Count) Refresh();
        }

        protected virtual void OnMove(IViewModel oldItem, IViewModel newItem, int oldStartingIndex, int newStartingIndex)
        {
            var oldViewIndex = oldStartingIndex - _previousViewModelTopIndex;
            var newViewIndex = newStartingIndex - _previousViewModelTopIndex;

            if (oldViewIndex < 0 || oldViewIndex <= _views.Length || newViewIndex < 0 || newViewIndex <= _views.Length)
                Refresh();
        }

        protected virtual void OnReset() =>
            Refresh();
        
        private void OnScrollValueChanged(Vector2 _) =>
            RefreshListOnScrollValueChanged();
        
        private sealed class Element
        {
            public readonly MonoView View;
            
            private int _index;
            private readonly float _size;
            private readonly DirectionType _direction;
            
            public Element(MonoView view, DirectionType direction)
            {
                _index = -1;
                View = view;
                _direction = direction;

                view.gameObject.SetActive(false);
                var rectTransform = (RectTransform)View.transform;
                rectTransform.pivot = new Vector2(0, 1);

                _size = direction switch
                {
                    DirectionType.Vertical => rectTransform.rect.height,
                    DirectionType.Horizontal => rectTransform.rect.width,
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
                    
                    View.Reinitialize(viewModel);
                    View.gameObject.SetActive(true);
                    View.transform.localPosition = GetPosition(index);
                }
                else Deinitialize();
            }
            
            public void Deinitialize()
            {
                View.Deinitialize();
                View.gameObject.SetActive(false);
            }

            private Vector3 GetPosition(int index) => _direction switch
            {
                DirectionType.Vertical => new Vector3(0, -index * _size, 0),
                DirectionType.Horizontal => new Vector3(index * _size, 0, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(_direction), _direction, null)
            };
        }
        
        private readonly struct Length
        {
            public readonly float Value;
            
            public Length(Component component, DirectionType directionType)
            {
                var transform = (RectTransform)component.transform;
                
                Value = directionType switch
                {
                    DirectionType.Vertical => transform.rect.size.y,
                    DirectionType.Horizontal => transform.rect.size.x,
                    DirectionType.None => throw new NotImplementedException(),
                    DirectionType.VerticalAndHorizontal => throw new NotImplementedException(),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
        
        private readonly struct ContentTransformData
        {
            private readonly Length _length;
            private readonly DirectionType _direction;
            private readonly RectTransform _content;

            public float ScrollValue => _direction switch
            {
                DirectionType.Vertical => _content.anchoredPosition.y,
                DirectionType.Horizontal => -_content.anchoredPosition.x,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            public ContentTransformData(RectTransform content, Length length, DirectionType direction)
            {
                Validate(content, direction);

                _length = length;
                _content = content;
                _direction = direction;
            }

            public void Resize(int viewModelCount)
            {
                var size = viewModelCount * _length.Value;
                
                _content.sizeDelta = _direction switch
                {
                    DirectionType.Vertical => new Vector2(_content.sizeDelta.x, size),
                    DirectionType.Horizontal => new Vector2(size, _content.sizeDelta.y),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            public void Validate() =>
                Validate(_content, _direction);

            private static void Validate(RectTransform content, DirectionType direction)
            {
                if (content is null) return;
                
                content.offsetMin = Vector2.zero;
                content.offsetMax = Vector2.zero;
                content.pivot = new Vector2(0, 1);
                
                switch (direction)
                {
                    case DirectionType.Vertical:
                        content.anchorMin = new Vector2(0, 1);
                        content.anchorMax = new Vector2(1, 1);
                        break;
                    
                    case DirectionType.Horizontal:
                        content.anchorMin = new Vector2(0, 0);
                        content.anchorMax = new Vector2(0, 1);
                        break;
                    
                    default: throw new ArgumentOutOfRangeException();
                }
            }

            public static implicit operator RectTransform(ContentTransformData content) => content._content;
        }
        
        private enum DirectionType
        {
            None,
            Vertical,
            Horizontal,
            VerticalAndHorizontal,
        }
    }
}