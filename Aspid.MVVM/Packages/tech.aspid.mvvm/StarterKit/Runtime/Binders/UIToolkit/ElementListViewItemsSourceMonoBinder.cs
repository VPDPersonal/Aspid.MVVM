using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{TElement}"/> that binds <see cref="ListView.itemsSource"/> to a read-only
    /// collection.
    /// </summary>
    /// <remarks>
    /// Observable and filtered lists are followed and refreshed on every change. The collection is wrapped so the
    /// list view cannot write into it.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/UIToolkit/Element Binder – ListView Items Source")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UIToolkit/Element Binder – ListView Items Source")]
    public sealed partial class ElementListViewItemsSourceMonoBinder : VisualElementMonoBinder<ListView>,
        IBinder<IReadOnlyObservableList<object>>,
        IBinder<IReadOnlyFilteredList<object>>,
        IBinder<IReadOnlyList<object>>
    {
        private IReadOnlyList<object> _list;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IReadOnlyObservableList<object> value) =>
            SetList(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IReadOnlyFilteredList<object> value) =>
            SetList(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IReadOnlyList<object> value) =>
            SetList(value);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Unsubscribe();
            _list = null;

            if (Element is not null)
            {
                Element.itemsSource = null;
                Element.RefreshItems();
            }

            base.OnUnbound();
        }

        private void SetList(IReadOnlyList<object> list)
        {
            Unsubscribe();
            _list = list;
            Subscribe();
            Apply();
        }

        private void Apply()
        {
            var element = Element;
            if (element is null) return;

            element.itemsSource = _list.ToListSource();
            element.RefreshItems();
        }

        private void Subscribe()
        {
            switch (_list)
            {
                case IReadOnlyFilteredList<object> filtered:
                    filtered.CollectionChanged += Apply;
                    break;
                case IReadOnlyObservableList<object> observable:
                    observable.CollectionChanged += OnCollectionChanged;
                    break;
            }
        }

        private void Unsubscribe()
        {
            switch (_list)
            {
                case IReadOnlyFilteredList<object> filtered:
                    filtered.CollectionChanged -= Apply;
                    break;
                case IReadOnlyObservableList<object> observable:
                    observable.CollectionChanged -= OnCollectionChanged;
                    break;
            }
        }

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<object> args) =>
            Apply();
    }
}
