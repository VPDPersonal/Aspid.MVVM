using Aspid.UI.MVVM.ViewModels.Generation;

namespace MVVMAnalyzer.Sample;

[ViewModel]
public partial class ViewModel2
{
    [ReadOnlyBind] private float _value;
    
    public ViewModel2(float value)
    {
        _value = value;
        value = _value;
    }

    public void Execute()
    {
        // _value = 10;
        // var value = _value;
    }
}