using Aspid.MVVM;
using UnityEngine;
using Aspid.MVVM.Unity; 
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Extensions;

namespace Samples.Aspid.MVVM.VirtualizedList
{
    [ViewModel]
    public sealed partial class ElementsListViewModel : MonoViewModel
    {
        [SerializeField] [Min(0)] private int _count = 100;
        
        [OneTimeBind] private readonly ObservableList<IViewModel> _items = new();

        private void Awake()
        {
            for (var i = 0; i < _count; i++)
            {
                var itemName = $"{i}";
                var isCompleted = Random.Range(0, 2) is 0;
                Items.Add(new ElementViewModel(itemName, isCompleted));
            }
        }

        [RelayCommand]
        private void AddViewModel()
        {
            var isCompleted = Random.Range(0, 2) is 0;
            Items.Add(new ElementViewModel("New" + (Items.Count), isCompleted));
        }

        [RelayCommand]
        private void InsertViewModel()
        {
            var isCompleted = Random.Range(0, 2) is 0;
            Items.Insert(0, new ElementViewModel("New" + (Items.Count - 1), isCompleted));
        }
        
        [RelayCommand]
        private void Move()
        {
            Items.Move(0, Items.Count - 1);
        }
        
        [RelayCommand]
        private void Swap()
        {
            Items.Swap(0, Items.Count - 1);
        }

        [RelayCommand]
        private void Remove()
        {
            Items.RemoveAt(Items.Count - 1);
        }

        [RelayCommand]
        private void Replace()
        {
            var isCompleted = Random.Range(0, 2) is 0;
            Items[0] = new ElementViewModel($"Replace {Items.Count}", isCompleted);
        }
    }
}