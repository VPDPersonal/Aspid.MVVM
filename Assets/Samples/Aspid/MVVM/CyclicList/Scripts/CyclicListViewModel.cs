using Aspid.MVVM;
using UnityEngine;
using Aspid.MVVM.Unity;
using Aspid.Collections.Observable;
using Random = UnityEngine.Random;

namespace Samples.Aspid.MVVM.CyclicList
{
    [ViewModel]
    public partial class CyclicListViewModel : MonoViewModel
    {
        [SerializeField] [Min(0)] private int _count = 100;
        
        [OneTimeBind] private readonly ObservableList<IViewModel> _items = new();

        private void Awake()
        {
            for (var i = 0; i < _count; i++)
            {
                var itemName = $"{i}";
                var isCompleted = Random.Range(0, 2) is 0;
                Items.Add(new CyclicElementViewModel(itemName, isCompleted));
            }
        }
    }
}