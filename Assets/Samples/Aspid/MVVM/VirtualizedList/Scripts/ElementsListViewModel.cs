using Aspid.MVVM;
using UnityEngine;
using Aspid.MVVM.Unity; 
using Aspid.Collections.Observable;

namespace Samples.Aspid.MVVM.VirtualizedList
{
    [ViewModel]
    public partial class ElementsListViewModel : MonoViewModel
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
    }
}