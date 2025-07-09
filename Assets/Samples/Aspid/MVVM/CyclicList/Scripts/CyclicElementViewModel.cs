using Aspid.MVVM;

namespace Samples.Aspid.MVVM.CyclicList
{
    [ViewModel]
    public partial class CyclicElementViewModel
    {
        [OneWayBind] private string _name;
        [OneWayBind] private bool _isCompleted;

        public CyclicElementViewModel(string name, bool isCompleted)
        {
            _name = name;
            _isCompleted = isCompleted;
        }
    }
}