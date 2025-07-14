using Aspid.MVVM;

namespace Samples.Aspid.MVVM.VirtualizedList
{
    [ViewModel]
    public partial class ElementViewModel
    {
        [Access(Get = Access.Public)]
        [OneWayBind] private int _number;
        
        [Access(Get = Access.Public)]
        [OneWayBind] private bool _isCompleted;

        public ElementViewModel(int number, bool isCompleted)
        {
            _number = number;
            _isCompleted = isCompleted;
        }
    }
}