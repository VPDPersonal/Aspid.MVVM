namespace Aspid.MVVM.ExampleScripts.ViewModels.Bind
{
    [ViewModel]
    public partial class Ex1BindViewModel
    {
        [Bind] private int _twoWayValue;
        [Bind] private readonly int _oneTimeValue;
    }
}

//  Generated Code:
//  public partial class Ex1BindViewModel
//  {
//  	public event Action<int> TwoWayValueChanged;
//      
//  	private int TwoWayValue
//  	{
//  	    get => _twoWayValue;
//  	    set => SetTwoWayValue(value);
//  	}
//      
//  	private int OneTimeValue => _oneTimeValue;
//      
//  	private void SetTwoWayValue(int value)
//  	{
//  	    if (EqualityComparer<int>.Default.Equals(_twoWayValue, value)) return;
//  	    
//  	    OnTwoWayValueChanging(_twoWayValue, value);
//  	    _twoWayValue = value;
//  	    OnTwoWayValueChanged(value);
//  		TwoWayValueChanged?.Invoke(_twoWayValue);
//  	}
//      
//  	partial void OnTwoWayValueChanging(int oldValue, int newValue);
//  	
//  	partial void OnTwoWayValueChanged(int newValue);
//      
//  }
