using Aspid.MVVM;
using UnityEngine;
using Aspid.MVVM.Unity; 
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;
using Aspid.Collections.Observable.Extensions; 

namespace Samples.Aspid.MVVM.VirtualizedList
{
    [ViewModel]
    public sealed partial class ElementsListViewModel : MonoViewModel
    {
        [SerializeField] [Min(0)] private int _count = 100;

        [OneWayBind] private FilteredList<ElementViewModel> _isOnTrueItems;
        [OneWayBind] private FilteredList<ElementViewModel> _isOnFalseItems;
        [OneTimeBind] private readonly ObservableList<ElementViewModel> _items = new();

        private int _number;
        
        private int Number => _number++;
        
        private void Awake()
        {
            for (var i = 0; i < _count; i++)
                Items.Add(CreateElement());

            IsOnTrueItems = new FilteredList<ElementViewModel>(Items, vm => vm.IsCompleted);
            IsOnFalseItems = new FilteredList<ElementViewModel>(Items, vm => !vm.IsCompleted);
        }
        
        [RelayCommand]
        private void AddViewModel() =>
            Items.Add(CreateElement());

        [RelayCommand]
        private void InsertViewModel() =>
            Items.Insert(0, CreateElement());
        
        [RelayCommand]
        private void Move() =>
            Items.Move(0, Items.Count - 1);
        
        [RelayCommand]
        private void Swap() =>
            Items.Swap(0, Items.Count - 1);

        [RelayCommand]
        private void Remove() =>
            Items.RemoveAt(Items.Count - 1);

        [RelayCommand]
        private void Replace() =>
            Items[0] = CreateElement();

        private ElementViewModel CreateElement()
        {
            var isCompleted = Random.Range(0, 2) is 0;
            return new ElementViewModel(Number, isCompleted);
        }
    }
}