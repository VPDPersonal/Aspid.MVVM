using Aspid.MVVM;

namespace Samples.Aspid.MVVM.VirtualizedList
{
    [ViewModel]
    public partial class ElementViewModel
    {
        [OneTimeBind] private readonly string _name;
        [OneTimeBind] private readonly bool _isCompleted;

        public ElementViewModel(string name, bool isCompleted)
        {
            _name = name;
            _isCompleted = isCompleted;
        }
    }
}