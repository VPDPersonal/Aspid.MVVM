#if ASPID_MVVM_HAS_ASPID_COLLECTIONS && ASPID_MVVM_HAS_UGUI && ASPID_MVVM_HAS_UI
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
        private int _operationVersion;
        private bool _destroyed;
        private IReadOnlyList<IViewModel> _subscribedSource;
        private int _previousTopIndex = -1;

        private Length? _viewLength;
        private Length? _viewportLength;
        private ContentTransformData? _contentTransform;

        private IReadOnlyList<IViewModel> _itemsSource;

        /// <summary>
        /// Gets or sets the ViewModels shown by the list. A <see langword="null"/> source clears the displayed items.
        /// </summary>
        /// <remarks>
        /// Source changes are tracked while enabled. Re-enabling refreshes the retained source; clearing it does not dispose its ViewModels.
        /// </remarks>
        public IReadOnlyList<IViewModel> ItemsSource
        {
            get => _itemsSource;
            set
            {
                if (_destroyed)
                {
                    if (value is null) return;
                    throw new InvalidOperationException("The virtualized list has been destroyed.");
                }

                var version = ++_operationVersion;
                StopTracking();
                _itemsSource = null;

                try
                {
                    DeinitializeViews();
                }
                finally
                {
                    if (version == _operationVersion) ClearContent();
                }

                if (version != _operationVersion || _destroyed) return;
                _itemsSource = value;
                if (value is not null && isActiveAndEnabled) Initialize();
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

            Initialize();
        }

        protected override void OnDisable()
        {
            _operationVersion++;
            try
            {
                StopTracking();
            }
            finally
            {
                base.OnDisable();
            }
        }

        protected override void OnDestroy()
        {
            _destroyed = true;
            _operationVersion++;
            _itemsSource = null;
            var failures = new CleanupExceptions();

            try
            {
                StopTracking();
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            var views = _views;
            _views = null;
            if (views is not null)
            {
                foreach (var element in views)
                {
                    try
                    {
                        element.Destroy();
                    }
                    catch (Exception exception)
                    {
                        failures.Add(exception);
                    }
                }
            }

            try
            {
                base.OnDestroy();
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }
            failures.ThrowIfAny();
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
            var first = Math.Min(oldStartingIndex, newStartingIndex);
            var last = Math.Max(oldStartingIndex, newStartingIndex);
            if (last >= _previousTopIndex && first < _previousTopIndex + _views.Length)
                Refresh();
        }

        /// <summary>
        /// Called when the source is reset or filtered anew.
        /// </summary>
        protected virtual void OnReset() =>
            Refresh();

        private void Initialize()
        {
            if (_destroyed || _itemsSource is null || !isActiveAndEnabled) return;
            StopTracking();
            var version = ++_operationVersion;
            _subscribedSource = _itemsSource;

            switch (_subscribedSource)
            {
                case IReadOnlyFilteredList<IViewModel> filteredList:
                    filteredList.CollectionChanged += OnCollectionChanged;
                    break;
                case IReadOnlyObservableList<IViewModel> observableList:
                    observableList.CollectionChanged += OnCollectionChanged;
                    break;
            }

            _initializing = StartCoroutine(InitializeAsync(version));
        }

        private IEnumerator InitializeAsync(int version)
        {
            yield return null;
            if (!IsCurrent(version)) yield break;
            _initializing = null;

            try
            {
                Canvas.ForceUpdateCanvases();
                if (!IsCurrent(version)) yield break;
                if (ViewLength.Value <= 0f)
                    throw new InvalidOperationException("The view prefab must have a positive size along the scroll direction.");

                _viewportLength = new Length(viewport, Direction);
                var visibleCount = Mathf.CeilToInt(ViewportLength.Value / ViewLength.Value) + 2;
                if (!EnsureViews(visibleCount, version)) yield break;

                onValueChanged.AddListener(OnScrollValueChanged);
                Refresh();
            }
            catch (Exception exception)
            {
                if (version == _operationVersion)
                {
                    Rollback.Run(
                        exception,
                        this,
                        static list => list.ItemsSource = null,
                        "VirtualizedList rollback");
                }
                throw;
            }
        }

        private bool EnsureViews(int count, int version)
        {
            if (_views is not null && _views.Length == count) return IsCurrent(version);
            var previous = _views ?? Array.Empty<Element>();
            var views = new Element[count];
            var reused = Math.Min(previous.Length, count);
            Array.Copy(previous, views, reused);
            var created = reused;
            var installed = false;

            try
            {
                for (; created < count; created++)
                {
                    views[created] = new Element(Instantiate(_viewPrefab, ContentTransform), Direction);
                    if (!IsCurrent(version))
                    {
                        created++;
                        return false;
                    }
                }

                _views = views;
                installed = true;
                var failures = new CleanupExceptions();
                for (var i = reused; i < previous.Length; i++)
                {
                    try
                    {
                        previous[i].Destroy();
                    }
                    catch (Exception exception)
                    {
                        failures.Add(exception);
                    }
                }
                failures.ThrowIfAny();
                return IsCurrent(version);
            }
            finally
            {
                if (!installed)
                {
                    for (var i = reused; i < created; i++)
                    {
                        try
                        {
                            views[i].Destroy();
                        }
                        catch (Exception exception)
                        {
                            Debug.LogException(exception, this);
                        }
                    }
                }
            }
        }

        private bool IsCurrent(int version) =>
            version == _operationVersion && !_destroyed && isActiveAndEnabled && _itemsSource is not null;

        private void StopTracking()
        {
            StopInitializing();
            onValueChanged.RemoveListener(OnScrollValueChanged);
            var source = _subscribedSource;
            _subscribedSource = null;

            switch (source)
            {
                case IReadOnlyFilteredList<IViewModel> filteredList:
                    filteredList.CollectionChanged -= OnCollectionChanged;
                    break;
                case IReadOnlyObservableList<IViewModel> observableList:
                    observableList.CollectionChanged -= OnCollectionChanged;
                    break;
            }
        }

        private void DeinitializeViews()
        {
            _previousTopIndex = -1;
            if (_views is null) return;
            var failures = new CleanupExceptions();
            foreach (var view in _views)
            {
                try
                {
                    view.Deinitialize();
                }
                catch (Exception exception)
                {
                    failures.Add(exception);
                }
            }
            failures.ThrowIfAny();
        }

        private void ClearContent()
        {
            if (!content) return;
            var size = content.sizeDelta;
            if (vertical) size.y = 0f;
            else if (horizontal) size.x = 0f;
            content.sizeDelta = size;
        }

        private void StopInitializing()
        {
            if (_initializing is null) return;

            StopCoroutine(_initializing);
            _initializing = null;
        }

        private void Refresh()
        {
            var version = _operationVersion;
            if (!IsCurrent(version) || _views is null) return;

            ResizeContent();
            _previousTopIndex = GetTopIndex();

            for (var i = 0; i < _views.Length; i++)
            {
                if (!IsCurrent(version)) return;
                RefreshElement(i, _previousTopIndex + i, version, true);
            }
        }

        private void RefreshIfVisibleOrResize(int index)
        {
            if (!IsCurrent(_operationVersion) || _views is null) return;

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
            var version = _operationVersion;
            if (!IsCurrent(version) || _views is null || _views.Length is 0) return;
            var topIndex = GetTopIndex();
            if (topIndex == _previousTopIndex) return;

            var direction = topIndex - _previousTopIndex;
            _previousTopIndex = topIndex;

            switch (direction)
            {
                case > 0: ShiftForward(topIndex, version); break;
                case < 0: ShiftBackward(topIndex, version); break;
            }
        }

        private void ShiftForward(int topIndex, int version)
        {
            var firstView = _views[0];

            for (var i = 1; i < _views.Length; i++)
            {
                _views[i - 1] = _views[i];
            }

            _views[^1] = firstView;
            for (var i = 0; i < _views.Length; i++)
            {
                if (!IsCurrent(version)) return;
                RefreshElement(i, topIndex + i, version);
            }
        }

        private void ShiftBackward(int topIndex, int version)
        {
            var lastView = _views[^1];

            for (var i = _views.Length - 1; i > 0; i--)
            {
                _views[i] = _views[i - 1];
            }

            _views[0] = lastView;
            for (var i = 0; i < _views.Length; i++)
            {
                if (!IsCurrent(version)) return;
                RefreshElement(i, topIndex + i, version);
            }
        }

        private void RefreshElement(int elementIndex, int itemIndex, int version, bool force = false)
        {
            var hasItem = itemIndex >= 0 && itemIndex < _itemsSource.Count;

            if (!hasItem) _views[elementIndex].Reinitialize(null, -1, this, version, true);
            else _views[elementIndex].Reinitialize(_itemsSource[itemIndex], itemIndex, this, version, force);
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
            private bool _isUpdating;
            private bool _clearPending;
            private bool _destroyPending;

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

            public void Reinitialize(IViewModel viewModel, int index, VirtualizedList owner, int version, bool force)
            {
                if (!force && _index == index) return;
                Deinitialize();
                if (!owner.IsCurrent(version)) return;
                if (index < 0) return;
                if (viewModel is null)
                {
                    _index = index;
                    View.transform.localPosition = GetPosition(index);
                    return;
                }

                _isUpdating = true;
                try
                {
                    View.Initialize(viewModel);
                }
                catch (Exception exception)
                {
                    _isUpdating = false;
                    Rollback.Run(exception, Deinitialize,
                        $"{nameof(VirtualizedList)}: initialize View {View.GetType().FullName} at index {index}");
                    throw;
                }
                finally
                {
                    _isUpdating = false;
                }

                if (_clearPending || !owner.IsCurrent(version))
                    Deinitialize();

                if (!owner.IsCurrent(version)) return;
                _index = index;
                View.transform.localPosition = GetPosition(index);
                View.gameObject.SetActive(true);
            }

            public void Deinitialize()
            {
                _index = -1;
                if (_isUpdating)
                {
                    _clearPending = true;
                    return;
                }
                if (!View) return;

                _isUpdating = true;
                try
                {
                    View.Deinitialize();
                }
                finally
                {
                    _isUpdating = false;
                    _clearPending = false;
                    if (View) View.gameObject.SetActive(false);
                    if (_destroyPending) Destroy();
                }
            }

            public void Destroy()
            {
                if (_isUpdating)
                {
                    _clearPending = true;
                    _destroyPending = true;
                    return;
                }

                _destroyPending = false;
                View.DestroyViewAndGameObject();
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
#endif
