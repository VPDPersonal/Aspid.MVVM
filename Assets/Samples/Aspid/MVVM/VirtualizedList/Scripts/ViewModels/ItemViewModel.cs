namespace Aspid.MVVM.SamplesVirtualizedList
{
    [ViewModel]
    public partial class ItemViewModel
    {
        [Access(Get = Access.Public)]
        [OneWayBind] private int _number;
        
        [Access(Get = Access.Public)]
        [OneWayBind] private bool _isCompleted;

        public ItemViewModel(int number, bool isCompleted)
        {
            _number = number;
            _isCompleted = isCompleted;
        }
    }
}