using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;
using Aspid.Collections.Observable.Filtered;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ScrollRect"/> that shows a list of ViewModels through a fixed set of recycled views,
    /// instantiating only as many as fit the viewport.
    /// </summary>
    /// <remarks>
    /// Beta. Items share the prefab size, scroll in one direction only, without spacing or layout groups.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Components/UI/ScrollRect/VirtualizedList (Beta)")]
    public class VirtualizedList : ScrollRect
    {
        [Tooltip("View prefab recycled for the items. Its RectTransform size sets the item size.")]
        [SerializeField] private MonoView _viewPrefab;

        private Element[] _views;
        private Coroutine _initializing;
        private int _previousTopIndex = -1;

        private Length? _viewLength;
        private Length? _viewportLength;
        private ContentTransformData? _contentTransform;

        private IReadOnlyList<IViewModel> _itemsSource;

        /// <summary>
        /// Gets or sets the ViewModels shown by the list. Observable and filtered lists are tracked for changes.
        /// </summary>
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

        private Length ViewLength =>
            _viewLength ??= new Length(_viewPrefab, Direction);

        private Length ViewportLength =>
            _viewportLength ??= new Length(viewport, Direction);

        private ContentTransformData ContentTransform =>
            _contentTransform ??= new ContentTransformData(content, ViewLength, Direction);

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (_viewPrefab && Direction is DirectionType.Vertical or DirectionType.Horizontal)
            {
                _viewLength = new Length(_viewPrefab, Direction);
                ContentTransform.Validate();
            }

            base.OnValidate();
        }
#endif

        protected override void OnEnable()
        {
            base.OnEnable();
            if (_itemsSource is null) return;

            StopInitializing();
            _initializing = StartCoroutine(InitializeAsync());
        }

        protected override void OnDisable()
        {
            StopInitializing();
            onValueChanged.RemoveListener(OnScrollValueChanged);
            base.OnDisable();
        }

        /// <summary>
        /// Called when one item is inserted.
        /// </summary>
        /// <param name="newItem">The inserted ViewModel.</param>
        /// <param name="newStartingIndex">The index it was inserted at.</param>
        protected virtual void OnAdded(IViewModel newItem, int newStartingIndex) =>
            RefreshIfVisibleOrResize(newStartingIndex);

        /// <summary>
        /// Called when several items are inserted.
        /// </summary>
        /// <param name="newItems">The inserted ViewModels.</param>
        /// <param name="newStartingIndex">The index of the first inserted item.</param>
        protected virtual void OnAdded(IReadOnlyList<IViewModel> newItems, int newStartingIndex) =>
            RefreshIfVisibleOrResize(newStartingIndex);

        /// <summary>
        /// Called when one item is removed.
        /// </summary>
        /// <param name="oldItem">The removed ViewModel.</param>
        /// <param name="oldStartingIndex">The index it was removed from.</param>
        protected virtual void OnRemoved(IViewModel oldItem, int oldStartingIndex) =>
            RefreshIfVisibleOrResize(oldStartingIndex);

        /// <summary>
        /// Called when several items are removed.
        /// </summary>
        /// <param name="oldItems">The removed ViewModels.</param>
        /// <param name="oldStartingIndex">The index of the first removed item.</param>
        protected virtual void OnRemoved(IReadOnlyList<IViewModel> oldItems, int oldStartingIndex) =>
            RefreshIfVisibleOrResize(oldStartingIndex);

        /// <summary>
        /// Called when one item is replaced in place.
        /// </summary>
        /// <param name="oldItem">The replaced ViewModel.</param>
        /// <param name="newItem">The new ViewModel.</param>
        /// <param name="index">The index of the replaced item.</param>
        protected virtual void OnReplace(IViewModel oldItem, IViewModel newItem, int index)
        {
            if (_views is null) return;
            if (IsVisible(index)) Refresh();
        }

        /// <summary>
        /// Called when one item is moved.
        /// </summary>
        /// <param name="item">The moved ViewModel.</param>
        /// <param name="oldStartingIndex">The index it was moved from.</param>
        /// <param name="newStartingIndex">The index it was moved to.</param>
        protected virtual void OnMove(IViewModel item, int oldStartingIndex, int newStartingIndex)
        {
            if (_views is null) return;
            if (IsVisible(oldStartingIndex) || IsVisible(newStartingIndex)) Refresh();
        }

        /// <summary>
        /// Called when the source is reset or filtered anew.
        /// </summary>
        protected virtual void OnReset() =>
            Refresh();

        private void Initialize()
        {
            switch (_itemsSource)
            {
                case IReadOnlyFilteredList<IViewModel> filteredList:
                    filteredList.CollectionChanged += OnCollectionChanged;
                    break;

                case IReadOnlyObservableList<IViewModel> observableList:
                    observableList.CollectionChanged += OnCollectionChanged;
                    break;
            }

            if (isActiveAndEnabled)
                _initializing = StartCoroutine(InitializeAsync());
        }

        private IEnumerator InitializeAsync()
        {
            yield return new WaitForEndOfFrame();
            _initializing = null;

            if (ViewLength.Value <= 0f)
            {
                Debug.LogError($"{name}: view prefab has zero size along the scroll direction", this);
                yield break;
            }

            var visibleCount = Mathf.CeilToInt(ViewportLength.Value / ViewLength.Value) + 2;
            EnsureViews(visibleCount);

            onValueChanged.RemoveListener(OnScrollValueChanged);
            onValueChanged.AddListener(OnScrollValueChanged);
            Refresh();
        }

        private void EnsureViews(int count)
        {
            if (_views is not null && _views.Length == count) return;

            var views = new Element[count];
            var reused = 0;

            if (_views is not null)
            {
                reused = Math.Min(_views.Length, count);
                Array.Copy(_views, views, reused);

                for (var i = reused; i < _views.Length; i++)
                    _views[i].View.DestroyViewAndGameObject();
            }

            for (var i = reused; i < count; i++)
                views[i] = new Element(Instantiate(_viewPrefab, ContentTransform), Direction);

            _views = views;
        }

        private void Deinitialize()
        {
            if (_itemsSource is null) return;

            StopInitializing();

            if (_views is not null)
            {
                foreach (var view in _views)
                    view.Deinitialize();
            }

            onValueChanged.RemoveListener(OnScrollValueChanged);

            switch (_itemsSource)
            {
                case IReadOnlyFilteredList<IViewModel> filteredList:
                    filteredList.CollectionChanged -= OnCollectionChanged;
                    break;

                case IReadOnlyObservableList<IViewModel> observableList:
                    observableList.CollectionChanged -= OnCollectionChanged;
                    break;
            }

            _itemsSource = null;
            OnReset();
        }

        private void StopInitializing()
        {
            if (_initializing is null) return;

            StopCoroutine(_initializing);
            _initializing = null;
        }

        private void Refresh()
        {
            if (_itemsSource is null || _views is null) return;

            ResizeContent();
            _previousTopIndex = GetTopIndex();

            for (var i = 0; i < _views.Length; i++)
                RefreshElement(i, _previousTopIndex + i, true);
        }

        private void RefreshIfVisibleOrResize(int index)
        {
            if (_views is null) return;

            if (index - _previousTopIndex < _views.Length) Refresh();
            else ResizeContent();
        }

        private bool IsVisible(int index)
        {
            var viewIndex = index - _previousTopIndex;
            return viewIndex >= 0 && viewIndex < _views.Length;
        }

        private void OnScrollValueChanged(Vector2 _)
        {
            var topIndex = GetTopIndex();
            if (topIndex == _previousTopIndex) return;

            var direction = topIndex - _previousTopIndex;
            _previousTopIndex = topIndex;

            switch (direction)
            {
                case > 0: ShiftForward(topIndex); break;
                case < 0: ShiftBackward(topIndex); break;
            }
        }

        private void ShiftForward(int topIndex)
        {
            var firstView = _views[0];

            for (var i = 1; i < _views.Length; i++)
            {
                _views[i - 1] = _views[i];
                RefreshElement(i, topIndex + i - 1);
            }

            _views[^1] = firstView;
            RefreshElement(_views.Length - 1, topIndex + _views.Length - 1);
        }

        private void ShiftBackward(int topIndex)
        {
            var lastView = _views[^1];

            for (var i = _views.Length - 1; i > 0; i--)
            {
                _views[i] = _views[i - 1];
                RefreshElement(i, topIndex + i);
            }

            _views[0] = lastView;
            RefreshElement(0, topIndex);
        }

        private void RefreshElement(int elementIndex, int itemIndex, bool force = false)
        {
            var hasItem = itemIndex >= 0 && itemIndex < _itemsSource.Count;

            if (!hasItem) _views[elementIndex].Reinitialize(null, -1, true);
            else _views[elementIndex].Reinitialize(_itemsSource[itemIndex], itemIndex, force);
        }

        private void ResizeContent() =>
            ContentTransform.Resize(_itemsSource.Count);

        private int GetTopIndex() =>
            Mathf.FloorToInt(ContentTransform.ScrollValue / ViewLength.Value);

        private void OnCollectionChanged() =>
            OnReset();

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<IViewModel> e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.IsSingleItem) OnAdded(e.NewItem, e.NewStartingIndex);
                    else OnAdded(e.NewItems, e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (e.IsSingleItem) OnRemoved(e.OldItem, e.OldStartingIndex);
                    else OnRemoved(e.OldItems, e.OldStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    if (e.IsSingleItem) OnReplace(e.OldItem, e.NewItem, e.OldStartingIndex);
                    else Refresh();
                    break;

                case NotifyCollectionChangedAction.Move:
                    OnMove(e.NewItem, e.OldStartingIndex, e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    OnReset();
                    break;

                default: throw new ArgumentOutOfRangeException(nameof(e.Action), e.Action, null);
            }
        }

        private sealed class Element
        {
            public readonly MonoView View;

            private readonly float _size;
            private readonly DirectionType _direction;

            private int _index;

            public Element(MonoView view, DirectionType direction)
            {
                _index = -1;
                View = view;
                _direction = direction;

                view.gameObject.SetActive(false);
                var rectTransform = (RectTransform)view.transform;
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

                if (index < 0)
                {
                    Deinitialize();
                    return;
                }

                View.Reinitialize(viewModel);
                View.gameObject.SetActive(true);
                View.transform.localPosition = GetPosition(index);
            }

            public void Deinitialize()
            {
                View.Deinitialize();
                View.gameObject.SetActive(false);
            }

            private Vector3 GetPosition(int index) =>
                _direction switch
                {
                    DirectionType.Vertical => new Vector3(0, -index * _size, 0),
                    DirectionType.Horizontal => new Vector3(index * _size, 0, 0),
                    _ => throw new ArgumentOutOfRangeException(nameof(_direction), _direction, null)
                };
        }

        private readonly struct Length
        {
            public readonly float Value;

            public Length(Component component, DirectionType direction)
            {
                var transform = (RectTransform)component.transform;

                Value = direction switch
                {
                    DirectionType.Vertical => transform.rect.size.y,
                    DirectionType.Horizontal => transform.rect.size.x,
                    _ => throw new InvalidOperationException("VirtualizedList scrolls in exactly one direction")
                };
            }
        }

        private readonly struct ContentTransformData
        {
            private readonly Length _length;
            private readonly DirectionType _direction;
            private readonly RectTransform _content;

            public ContentTransformData(RectTransform content, Length length, DirectionType direction)
            {
                Validate(content, direction);

                _length = length;
                _content = content;
                _direction = direction;
            }

            public float ScrollValue =>
                _direction switch
                {
                    DirectionType.Vertical => _content.anchoredPosition.y,
                    DirectionType.Horizontal => -_content.anchoredPosition.x,
                    _ => throw new ArgumentOutOfRangeException(nameof(_direction), _direction, null)
                };

            public void Resize(int itemCount)
            {
                var size = itemCount * _length.Value;

                _content.sizeDelta = _direction switch
                {
                    DirectionType.Vertical => new Vector2(_content.sizeDelta.x, size),
                    DirectionType.Horizontal => new Vector2(size, _content.sizeDelta.y),
                    _ => throw new ArgumentOutOfRangeException(nameof(_direction), _direction, null)
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

                    default: throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }
            }

            public static implicit operator RectTransform(ContentTransformData content) =>
                content._content;
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
