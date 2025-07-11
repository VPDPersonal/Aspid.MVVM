using Aspid.MVVM;

namespace Samples.Aspid.MVVM.VirtualizedList
{
    [ViewModel]
    public partial class ElementViewModel
    {
        [OneWayBind] private string _name;
        
        [Access(Get = Access.Public)]
        [OneWayBind] private bool _isCompleted;

        public ElementViewModel(string name, bool isCompleted)
        {
            _name = name;
            _isCompleted = isCompleted;
        }
    }
}