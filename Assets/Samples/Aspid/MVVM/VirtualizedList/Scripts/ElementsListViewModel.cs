using Aspid.MVVM;
using UnityEngine;
using Aspid.MVVM.Unity; 
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Extensions;
using Aspid.Collections.Observable.Filtered;
using Random = UnityEngine.Random;

namespace Samples.Aspid.MVVM.VirtualizedList
{
    [ViewModel]
    public sealed partial class ElementsListViewModel : MonoViewModel
    {
        [SerializeField] [Min(0)] private int _count = 100;

        [OneWayBind] 
        private FilteredList<ElementViewModel> _items;
        
        [OneTimeBind] 
        private readonly ObservableList<ElementViewModel> _itemsSource = new();

        private void Awake()
        {
            for (var i = 0; i < _count; i++)
            {
                var itemName = $"{i}";
                var isCompleted = Random.Range(0, 2) is 0;
                ItemsSource.Add(new ElementViewModel(itemName, isCompleted));
            }

            
            Items = new FilteredList<ElementViewModel>(ItemsSource, filter: vm => vm.IsCompleted);
        }

        [RelayCommand]
        private void AddViewModel()
        {
            var isCompleted = Random.Range(0, 2) is 0;
            ItemsSource.Add(new ElementViewModel("New" + (ItemsSource.Count), isCompleted));
        }

        [RelayCommand]
        private void InsertViewModel()
        {
            var isCompleted = Random.Range(0, 2) is 0;
            ItemsSource.Insert(0, new ElementViewModel("New" + (ItemsSource.Count - 1), isCompleted));
        }
        
        [RelayCommand]
        private void Move()
        {
            ItemsSource.Move(0, ItemsSource.Count - 1);
        }
        
        [RelayCommand]
        private void Swap()
        {
            ItemsSource.Swap(0, ItemsSource.Count - 1);
        }

        [RelayCommand]
        private void Remove()
        {
            ItemsSource.RemoveAt(ItemsSource.Count - 1);
        }

        [RelayCommand]
        private void Replace()
        {
            var isCompleted = Random.Range(0, 2) is 0;
            ItemsSource[0] = new ElementViewModel($"Replace {ItemsSource.Count}", isCompleted);
        }
    }
}