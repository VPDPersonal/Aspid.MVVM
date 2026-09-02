using System.Collections.Generic;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="VisualElementMonoBinder{ListView}"/> that fills a <see cref="ListView"/> from a bound collection.
    /// </summary>
    /// <remarks>
    /// An observable collection is followed and the list is rebuilt via <see cref="ListView.RefreshItems"/> on every
    /// change, since a filtered list reports no index. The collection is always wrapped, even when it already
    /// implements <see cref="IList"/>, so the list view cannot write into the ViewModel's collection.
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

        /// <summary>
        /// Binds an observable list and follows it.
        /// </summary>
        /// <param name="value">The collection received from the ViewModel, or <see langword="null"/> to clear the list.</param>
        [BinderLog]
        public void SetValue(IReadOnlyObservableList<object> value) => SetList(value);

        /// <summary>
        /// Binds a filtered list and follows it.
        /// </summary>
        /// <param name="value">The collection received from the ViewModel, or <see langword="null"/> to clear the list.</param>
        [BinderLog]
        public void SetValue(IReadOnlyFilteredList<object> value) => SetList(value);

        /// <summary>
        /// Binds a plain list, which is read once.
        /// </summary>
        /// <param name="value">The collection received from the ViewModel, or <see langword="null"/> to clear the list.</param>
        [BinderLog]
        public void SetValue(IReadOnlyList<object> value) => SetList(value);

        /// <summary>
        /// Called when the binder is unbound. Clears the list's source so a recycled panel shows nothing from the
        /// previous binding.
        /// </summary>
        protected override void OnUnbound()
        {
            Unsubscribe();

            if (Element is not null)
            {
                Element.itemsSource = null;
                Element.RefreshItems();
            }

            _list = null;
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

            // Always wrapped, even when the collection already implements IList, so the list view cannot write into it.
            element.itemsSource = _list.ToListSource();
            element.RefreshItems();
        }

        private void Subscribe()
        {
            switch (_list)
            {
                case IReadOnlyFilteredList<object> filtered: filtered.CollectionChanged += Apply; break;
                case IReadOnlyObservableList<object> observable: observable.CollectionChanged += OnCollectionChanged; break;
            }
        }

        private void Unsubscribe()
        {
            switch (_list)
            {
                case IReadOnlyFilteredList<object> filtered: filtered.CollectionChanged -= Apply; break;
                case IReadOnlyObservableList<object> observable: observable.CollectionChanged -= OnCollectionChanged; break;
            }
        }

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<object> args) => Apply();
    }
}
