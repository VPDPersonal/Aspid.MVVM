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
    /// The counterpart of the uGUI collection binders, and the one thing UI Toolkit does differently: a
    /// <see cref="ListView"/> owns the recycling itself, so nothing here creates or releases a view. The binder hands the
    /// list its source and tells it what changed; <c>makeItem</c> and <c>bindItem</c> stay where they belong, in the UXML
    /// and the code that authored the list.
    /// <para/>
    /// An observable collection is followed and the list is rebuilt on every change. <see cref="ListView.RefreshItems"/>
    /// is what a source of unknown shape requires — the granular calls need an index, and a filtered list reports none.
    /// <para/>
    /// The source is cleared when the binding is released, so a recycled panel does not show the previous ViewModel's
    /// items for a frame.
    /// <para/>
    /// The collection is wrapped rather than handed over, even when it already implements <see cref="IList"/>: the list
    /// view would otherwise be able to write into the ViewModel's collection. The wrapper does not copy, so the cost is
    /// one object per binding.
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

            // Обёртка ставится всегда, даже когда коллекция сама реализует IList: иначе ListView получил бы
            // прямую запись в коллекцию ViewModel — ровно ту связь, ради устранения которой существует
            // фреймворк. Обёртка не копирует, поэтому стоит один объект на привязку.
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
