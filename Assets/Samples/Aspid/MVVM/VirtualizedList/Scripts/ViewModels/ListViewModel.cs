using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Unity;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;
using Aspid.Collections.Observable.Extensions;
using Random = UnityEngine.Random;

namespace Aspid.MVVM.SamplesVirtualizedList
{
    [ViewModel]
    [Serializable]
    public sealed partial class ListViewModel : IComponentInitializable
    {
        [SerializeField] [Min(0)] private int _count = 100;

        [OneTimeBind] private readonly FilteredList<ItemViewModel> _isOnTrueItems;
        [OneTimeBind] private readonly ObservableList<ItemViewModel> _items = new();

        private int _number;
        
        private int Number => _number++;
        
        public ListViewModel()
        {
            _items = new ObservableList<ItemViewModel>(_items);
            _isOnTrueItems = new FilteredList<ItemViewModel>(Items, vm => vm.IsCompleted);
        }
        
        void IComponentInitializable.Initialize()
        {
            for (var i = 0; i < _count; i++)
                Items.Add(CreateElement());
        }
        
        [RelayCommand]
        private void AddViewModel() =>
            Items.Add(CreateElement());

        [RelayCommand]
        private void InsertViewModel(int index) =>
            Items.Insert(index, CreateElement());
        
        [RelayCommand]
        private void Move(int oldIndex, int newIndex) =>
            Items.Move(oldIndex, newIndex);
        
        [RelayCommand]
        private void Swap(int index1, int index2) =>
            Items.Swap(index1, index2);

        [RelayCommand]
        private void Remove(int index) =>
            Items.RemoveAt(index);

        [RelayCommand]
        private void Replace(int index) =>
            Items[index] = CreateElement();

        private ItemViewModel CreateElement()
        {
            var isCompleted = Random.Range(0, 2) is 0;
            return new ItemViewModel(Number, isCompleted);
        }
    }
}